using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MKLibCS.Collections;
using MKLibCS.Generic;
using MKLibCS.Logging;
using MKLibCS.TargetSpecific;

namespace MKLibCS.Reflection
{
#if LEGACY
    using TypeInfo = Type;

#endif

    /// <summary>
    ///     Utilities and extensions for System.Reflection types.
    /// </summary>
    public static partial class ReflectionUtil
    {
        private static readonly Logger logger = new Logger(typeof(ReflectionUtil));

        #region Delegate

        /// <summary>
        /// </summary>
        /// <param name="typeArgs"></param>
        /// <returns></returns>
        public static Type GetDelegateType(Type[] typeArgs)
        {
#if V3
            // TODO: typeArgs.Length > 7
            if (typeArgs.Last() != typeof(void))
                return Expression.GetFuncType(typeArgs);
            var actionTypeArgs = typeArgs.ToList();
            actionTypeArgs.RemoveAt(actionTypeArgs.Count - 1);
            return Expression.GetActionType(actionTypeArgs.ToArray());
#else
            return Expression.GetDelegateType(typeArgs);
#endif
        }

        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        /// <param name="declaringType"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public static Delegate CreateDelegateSmart(this MethodInfo method, Type declaringType, out Type[] paramTypes)
        {
            var typeInfo = declaringType.GetTypeInfo();
            var types = method.GetParameters().Select(p => p.ParameterType).ToList();
            if (!method.IsStatic)
                types.Insert(0, declaringType);
            paramTypes = types.ToArray();
            Type delegType;
            if (!method.IsStatic && typeInfo.IsValueType)
            {
#if V3
                // Make it a lambda
                var count = 0;
                var expressionParams = types.Select(t => Expression.Parameter(t, "arg" + (count++))).ToList();
                var obj = expressionParams[0];
                var methodParams = expressionParams.Take(1, expressionParams.Count - 1).ToArray();
                types.Add(method.ReturnType);
                delegType = GetDelegateType(types.ToArray());
                var call = Expression.Call(obj, method, methodParams.ToArray());
                var lambda = Expression.Lambda(
                    delegType,
                    call,
                    expressionParams
                    ).Compile();
                logger.InternalDebug("Converted struct <{0}> member method \"{1}\" to lambda", typeInfo.FullName,
                    method.Name);
                return lambda;
#else
                types[0] = types[0].MakeByRefType();
#endif
            }
            types.Add(method.ReturnType);
            delegType = GetDelegateType(types.ToArray());
            return method.CreateDelegate(delegType);
        }

        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public static Delegate CreateDelegateSmart(this MethodInfo method, out Type[] paramTypes)
        {
            return method.CreateDelegateSmart(method.DeclaringType, out paramTypes);
        }

        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Delegate CreateDelegateSmart(this MethodInfo method)
        {
            Type[] paramTypes;
            return method.CreateDelegateSmart(out paramTypes);
        }

        /// <summary>
        /// </summary>
        /// <param name="deleg"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object DynamicInvokeSafe(this Delegate deleg, params object[] parameters)
        {
#if Unity
            try
            {
                return deleg.DynamicInvoke(parameters);
            }
            catch (TargetParameterCountException)
            {
                // There is something wrong with Mono's DynamicInvoke
                var method = deleg.Method;
                var obj = parameters[0];
                parameters = parameters.Take(1, parameters.Length - 1).ToArray();
                return method.Invoke(obj, parameters);
            }
#else
            return deleg.DynamicInvoke(parameters);
#endif
        }

        #endregion

        #region Get Members

        /// <summary>
        ///     Get all public fields and properties declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static IEnumerable<MemberInfo> GetFieldsAndProperties(this TypeInfo type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            return CollectionsUtil.Combine<MemberInfo>(type.GetFields(), type.GetProperties());
        }

        #region Attribute

        #region Private

        private static IEnumerable<KeyValuePair<M, Attribute[]>> GetMembersWithAttributeIter<M>(
            this TypeInfo type,
            Type attributeType,
            Func<TypeInfo, IEnumerable<M>> getMembers
            )
            where M : MemberInfo
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (!attributeType.GetTypeInfo().IsSubclassOf(typeof(Attribute)))
                throw new ArgumentException(nameof(attributeType));
            foreach (var member in getMembers(type))
            {
                var attrs = member.GetCustomAttributes(attributeType, true);
                if (attrs.IsEmpty())
                    continue;
                yield return new KeyValuePair<M, Attribute[]>(
                    member,
                    attrs.Cast<Attribute>().ToArray()
                    );
            }
        }

        private static IEnumerable<KeyValuePair<M, T[]>> GetMembersWithAttributeIter<M, T>(
            this TypeInfo type,
            Func<TypeInfo, IEnumerable<M>> getMembers
            )
            where M : MemberInfo
            where T : Attribute
        {
            return type.GetMembersWithAttributeIter(typeof(T), getMembers)
                .Select(kvp => new KeyValuePair<M, T[]>(kvp.Key, kvp.Value.Cast<T>().ToArray()));
        }

        private static Dictionary<M, Attribute[]> GetMembersWithAttributeDict<M>(
            this TypeInfo type,
            Type attributeType,
            Func<TypeInfo, IEnumerable<M>> getMembers
            )
            where M : MemberInfo
        {
            return type.GetMembersWithAttributeIter(attributeType, getMembers).ToDictionary();
        }

        private static Dictionary<M, T[]> GetMembersWithAttributeDict<M, T>(
            this TypeInfo type,
            Func<TypeInfo, IEnumerable<M>> getMembers
            )
            where M : MemberInfo
            where T : Attribute
        {
            return type.GetMembersWithAttributeIter<M, T>(getMembers).ToDictionary();
        }

        private static IEnumerable<M> GetMembersWithAttribute<M>(
            this TypeInfo type,
            Type attributeType,
            Func<TypeInfo, IEnumerable<M>> getMembers
            )
            where M : MemberInfo
        {
            return type.GetMembersWithAttributeIter(attributeType, getMembers).Select(kvp => kvp.Key);
        }

        private static IEnumerable<M> GetMembersWithAttribute<M, T>(
            this TypeInfo type,
            Func<TypeInfo, IEnumerable<M>> getMembers
            )
            where M : MemberInfo
            where T : Attribute
        {
            return type.GetMembersWithAttributeIter<M, T>(getMembers).Select(kvp => kvp.Key);
        }

        #endregion

        #region Member

        /// <summary>
        ///     Get all public members with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static Dictionary<MemberInfo, Attribute[]> GetMembersWithAttributeDict(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttributeDict(attributeType, t => t.GetMembers());
        }

        /// <summary>
        ///     Get all public members with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Dictionary<MemberInfo, T[]> GetMembersWithAttributeDict<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttributeDict<MemberInfo, T>(t => t.GetMembers());
        }

        /// <summary>
        ///     Get all public members with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static IEnumerable<MemberInfo> GetMembersWithAttribute(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttribute(attributeType, t => t.GetMembers());
        }

        /// <summary>
        ///     Get all public members with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static IEnumerable<MemberInfo> GetMembersWithAttribute<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttribute<MemberInfo, T>(t => t.GetMembers());
        }

        #endregion

        #region Field

        /// <summary>
        ///     Get all public fields with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static Dictionary<FieldInfo, Attribute[]> GetFieldsWithAttributeDict(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttributeDict(attributeType, t => t.GetFields());
        }

        /// <summary>
        ///     Get all public fields with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Dictionary<FieldInfo, T[]> GetFieldsWithAttributeDict<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttributeDict<FieldInfo, T>(t => t.GetFields());
        }

        /// <summary>
        ///     Get all public fields with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static IEnumerable<FieldInfo> GetFieldsWithAttribute(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttribute(attributeType, t => t.GetFields());
        }

        /// <summary>
        ///     Get all public fields with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static IEnumerable<FieldInfo> GetFieldsWithAttribute<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttribute<FieldInfo, T>(t => t.GetFields());
        }

        #endregion

        #region Property

        /// <summary>
        ///     Get all public properties with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static Dictionary<PropertyInfo, Attribute[]> GetPropertiesWithAttributeDict(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttributeDict(attributeType, t => t.GetProperties());
        }

        /// <summary>
        ///     Get all public properties with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Dictionary<PropertyInfo, T[]> GetPropertiesWithAttributeDict<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttributeDict<PropertyInfo, T>(t => t.GetProperties());
        }

        /// <summary>
        ///     Get all public properties with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttribute(attributeType, t => t.GetProperties());
        }

        /// <summary>
        ///     Get all public properties with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttribute<PropertyInfo, T>(t => t.GetProperties());
        }

        #endregion

        #region Field & Property

        /// <summary>
        ///     Get all public fields and properties with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static Dictionary<MemberInfo, Attribute[]> GetFieldsAndPropertiesWithAttributeDict(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttributeDict(attributeType, t => t.GetFieldsAndProperties());
        }

        /// <summary>
        ///     Get all public fields and properties with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Dictionary<MemberInfo, T[]> GetFieldsAndPropertiesWithAttributeDict<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttributeDict<MemberInfo, T>(t => t.GetFieldsAndProperties());
        }

        /// <summary>
        ///     Get all public fields and properties with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static IEnumerable<MemberInfo> GetFieldsAndPropertiesWithAttribute(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttribute(attributeType, t => t.GetFieldsAndProperties());
        }

        /// <summary>
        ///     Get all public fields and properties with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static IEnumerable<MemberInfo> GetFieldsAndPropertiesWithAttribute<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttribute<MemberInfo, T>(t => t.GetFieldsAndProperties());
        }

        #endregion

        #region Method

        /// <summary>
        ///     Get all public methods with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static Dictionary<MethodInfo, Attribute[]> GetMethodsWithAttributeDict(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttributeDict(attributeType, t => t.GetMethods());
        }

        /// <summary>
        ///     Get all public methods with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Dictionary<MethodInfo, T[]> GetMethodsWithAttributeDict<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttributeDict<MethodInfo, T>(t => t.GetMethods());
        }

        /// <summary>
        ///     Get all public methods with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static IEnumerable<MethodInfo> GetMethodsWithAttribute(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttribute(attributeType, t => t.GetMethods());
        }

        /// <summary>
        ///     Get all public methods with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttribute<MethodInfo, T>(t => t.GetMethods());
        }

        #endregion

        #region NestedType

        /// <summary>
        ///     Get all public nested types with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static Dictionary<TypeInfo, Attribute[]> GetNestedTypesWithAttributeDict(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttributeDict(attributeType, t => t.GetNestedTypes());
        }

        /// <summary>
        ///     Get all public nested types with a specific type of attribute declared in a class/struct,
        ///     together with attributes, in a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static Dictionary<TypeInfo, T[]> GetNestedTypesWithAttributeDict<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttributeDict<TypeInfo, T>(t => t.GetNestedTypes());
        }

        /// <summary>
        ///     Get all public nested types with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        /// <exception cref="ArgumentException">attributeType is not subclass of Attribute.</exception>
        public static IEnumerable<TypeInfo> GetNestedTypesWithAttribute(
            this TypeInfo type,
            Type attributeType
            )
        {
            return type.GetMembersWithAttribute(attributeType, t => t.GetNestedTypes());
        }

        /// <summary>
        ///     Get all public nested types with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="ArgumentNullException">type is null.</exception>
        public static IEnumerable<TypeInfo> GetNestedTypesWithAttribute<T>(
            this TypeInfo type
            )
            where T : Attribute
        {
            return type.GetMembersWithAttribute<TypeInfo, T>(t => t.GetNestedTypes());
        }

        #endregion

        #region Assembly -> Type

        /// <summary>
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">assembly is null.</exception>
        public static IEnumerable<TypeInfo> GetTypesWithAttribute(this Assembly assembly, Type attributeType)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            foreach (var t in assembly.GetTypes())
                if (t.GetCustomAttribute(attributeType) != null)
                    yield return t;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">assembly is null.</exception>
        public static IEnumerable<TypeInfo> GetTypesWithAttribute<T>(this Assembly assembly)
            where T : Attribute
        {
            return assembly.GetTypesWithAttribute(typeof(T));
        }

        #endregion

        #endregion

        private static MemberTypes GetMemberTypeDefault(this MemberInfo member)
        {
            if (member is ConstructorInfo)
                return MemberTypes.Constructor;
            if (member is EventInfo)
                return MemberTypes.Event;
            if (member is FieldInfo)
                return MemberTypes.Field;
            if (member is MethodInfo)
                return MemberTypes.Method;
            if (member is PropertyInfo)
                return MemberTypes.Property;
            if (member is TypeInfo)
                return MemberTypes.TypeInfo;
            return MemberTypes.All;
        }

        /// <summary>
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static MemberTypes GetMemberType(this MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            try
            {
                return (MemberTypes) TargetSpecificUtil.GetMemberType.Do(member);
            }
            catch (NullReferenceException)
            {
                return member.GetMemberTypeDefault();
            }
            catch (MissingGenericMethodException)
            {
                return member.GetMemberTypeDefault();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TypeInfo GetObjTypeInfo(this object obj)
        {
            return obj.GetType().GetTypeInfo();
        }

        /// <summary>
        ///     Get the value of the field/property member of an object.
        /// </summary>
        /// <param name="member">The System.Reflection.MemberInfo object that provides information for the member.</param>
        /// <param name="obj">The object that contains the member.</param>
        /// <returns>The value of the member.</returns>
        /// <exception cref="ArgumentNullException">member is null.</exception>
        /// <exception cref="ArgumentException">member is not System.Reflection.FieldInfo or System.Reflection.PropertyInfo.</exception>
        public static object GetValue(this MemberInfo member, object obj)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            if (member is FieldInfo)
                return (member as FieldInfo).GetValue(obj);
            if (member is PropertyInfo)
                return (member as PropertyInfo).GetValue(obj, null);
            throw new ArgumentException(nameof(member) + " is not FieldInfo or PropertyInfo");
        }

        /// <summary>
        ///     Get the type of the member.
        /// </summary>
        /// <param name="member">The System.Reflection.MemberInfo object that provides information for the member.</param>
        /// <exception cref="ArgumentNullException">member is null.</exception>
        /// <exception cref="ArgumentException">member is not System.Reflection.FieldInfo or System.Reflection.PropertyInfo.</exception>
        public static Type GetValueType(this MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            if (member is FieldInfo)
                return (member as FieldInfo).FieldType;
            if (member is PropertyInfo)
                return (member as PropertyInfo).PropertyType;
            throw new ArgumentException(nameof(member) + " is not FieldInfo or PropertyInfo");
        }

        #endregion

        #region Set Members

        /// <summary>
        ///     Sets the value of the field/property member of an object.
        /// </summary>
        /// <param name="member">The System.Reflection.MemberInfo object that provides information for the member.</param>
        /// <param name="obj">The object that contains the member.</param>
        /// <param name="value">The value to set.</param>
        /// <exception cref="ArgumentNullException">member is null.</exception>
        /// <exception cref="ArgumentException">member is not System.Reflection.FieldInfo or System.Reflection.PropertyInfo.</exception>
        public static void SetValue(this MemberInfo member, object obj, object value)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));
            if (member is FieldInfo)
                (member as FieldInfo).SetValue(obj, value);
            else if (member is PropertyInfo)
                (member as PropertyInfo).SetValue(obj, value, null);
            else
                throw new ArgumentException(nameof(member) + " is not FieldInfo or PropertyInfo");
        }

        #endregion

        #region Type

        /// <summary>
        ///     Check if a type is an implementation of the class List&lt;&gt;,
        ///     and output the type parameters.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public static bool IsList(this TypeInfo type, out Type[] paramTypes)
        {
            paramTypes = null;
            if (!type.IsGenericType)
                return false;
            if (type.GetGenericTypeDefinition() != typeof(List<>))
                return false;
            paramTypes = type.GetGenericArguments();
            return true;
        }

        /// <summary>
        ///     Check if a type is an implementation of the class List&lt;&gt;.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsList(this TypeInfo type)
        {
            Type[] paramTypes;
            return type.IsList(out paramTypes);
        }

        /// <summary>
        ///     Check if a type is an implementation of the class Dictionary&lt;&gt;,
        ///     and output the type parameters.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public static bool IsDict(this TypeInfo type, out Type[] paramTypes)
        {
            paramTypes = null;
            if (!type.IsGenericType)
                return false;
            if (type.GetGenericTypeDefinition() != typeof(Dictionary<,>))
                return false;
            paramTypes = type.GetGenericArguments();
            return true;
        }

        /// <summary>
        ///     Check if a type is an implementation of the class Dictionary&lt;&gt;.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDict(this TypeInfo type)
        {
            Type[] paramTypes;
            return type.IsDict(out paramTypes);
        }

        /// <summary>
        ///     Check if a type is an implementation of the class KeyValuePair&lt;&gt;,
        ///     and output the type parameters.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paramTypes"></param>
        /// <returns></returns>
        public static bool IsKeyValuePair(this TypeInfo type, out Type[] paramTypes)
        {
            paramTypes = null;
            if (!type.IsGenericType)
                return false;
            if (type.GetGenericTypeDefinition() != typeof(KeyValuePair<,>))
                return false;
            paramTypes = type.GetGenericArguments();
            return true;
        }

        /// <summary>
        ///     Check if a type is an implementation of the class KeyValuePair&lt;&gt;.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsKeyValuePair(this TypeInfo type)
        {
            Type[] paramTypes;
            return type.IsKeyValuePair(out paramTypes);
        }

        #endregion
    }
}