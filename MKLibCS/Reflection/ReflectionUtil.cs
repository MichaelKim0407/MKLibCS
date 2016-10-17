using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using MKLibCS.Collections;
using MKLibCS.TargetSpecific;
using MKLibCS.Generic;

namespace MKLibCS.Reflection
{
    /// <summary>
    /// Utilities and extensions for System.Reflection types.
    /// </summary>
    public static class ReflectionUtil
    {
        #region Create

        /// <summary>
        /// Gets the delegate from a method.
        /// </summary>
        /// <typeparam name="T">The delegate type.</typeparam>
        /// <param name="method">The System.Reflection.MethodInfo object representing the method.</param>
        public static T CreateDelegate<T>(this MethodInfo method)
        {
            return (T)(object)method.CreateDelegate(typeof(T));
        }

        #endregion

        #region Find

        /// <summary>
        /// Gets all public methods with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="System.ArgumentNullException">type is null.</exception>
        public static IEnumerable<MethodInfo> FindAllMethodsWithAttribute(this TypeInfo type, Type attributeType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            foreach (MethodInfo method in type.GetMethods())
                if (method.GetCustomAttribute(attributeType) != null)
                    yield return method;
        }

        /// <summary>
        /// Gets all public methods with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="System.ArgumentNullException">type is null.</exception>
        public static IEnumerable<MethodInfo> FindAllMethodsWithAttribute<T>(this TypeInfo type)
            where T : Attribute
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return type.FindAllMethodsWithAttribute(typeof(T));
        }

        /// <summary>
        /// Gets all public types with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="System.ArgumentNullException">type is null.</exception>
        public static IEnumerable<TypeInfo> FindAllNestedTypesWithAttribute(this TypeInfo type, Type attributeType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            foreach (var t in type.GetNestedTypes())
                if (t.GetCustomAttribute(attributeType) != null)
                    yield return t;
        }

        /// <summary>
        /// Gets all public types with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="System.ArgumentNullException">type is null.</exception>
        public static IEnumerable<TypeInfo> FindAllNestedTypesWithAttribute<T>(this TypeInfo type)
            where T : Attribute
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return type.FindAllNestedTypesWithAttribute(typeof(T));
        }

        /// <summary>
        /// Gets all public properties with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        /// <exception cref="System.ArgumentNullException">type is null.</exception>
        public static IEnumerable<PropertyInfo> FindAllPropertiesWithAttribute(this TypeInfo type, Type attributeType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            foreach (var property in type.GetProperties())
                if (property.GetCustomAttribute(attributeType) != null)
                    yield return property;
        }

        /// <summary>
        /// Gets all public properties with a specific type of attribute declared in a class/struct.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="System.ArgumentNullException">type is null.</exception>
        public static IEnumerable<PropertyInfo> FindAllPropertiesWithAttribute<T>(this TypeInfo type)
            where T : Attribute
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return type.FindAllPropertiesWithAttribute(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">assembly is null.</exception>
        public static IEnumerable<TypeInfo> FindAllTypesWithAttribute(this Assembly assembly, Type attributeType)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            foreach (var t in assembly.DefinedTypes)
                if (t.GetCustomAttribute(attributeType) != null)
                    yield return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">assembly is null.</exception>
        public static IEnumerable<TypeInfo> FindAllTypesWithAttribute<T>(this Assembly assembly)
            where T : Attribute
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            return assembly.FindAllTypesWithAttribute(typeof(T));
        }

        #endregion

        #region Get

        #region GetCustomAttribute
        /* NOTE: New in .NET Framework 4.5: System.Reflection.CustomAttributeExtensions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static Attribute GetCustomAttribute(this Assembly assembly, Type attributeType)
        {
            return Attribute.GetCustomAttribute(assembly, attributeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">assembly is null.</exception>
        public static T GetCustomAttribute<T>(this Assembly assembly)
            where T : Attribute
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");
            return (T)assembly.GetCustomAttribute(typeof(T));
        }

        /// <summary>
        /// Gets the attribute of type T applied the a member.
        /// </summary>
        /// <param name="m">The member.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        public static Attribute GetCustomAttribute(this MemberInfo m, Type attributeType)
        {
            return Attribute.GetCustomAttribute(m, attributeType);
        }

        /// <summary>
        /// Gets the attribute of type T applied the a member.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="m">The member.</param>
        /// <exception cref="System.ArgumentNullException">m is null.</exception>
        public static T GetCustomAttribute<T>(this MemberInfo m)
            where T : Attribute
        {
            if (m == null)
                throw new ArgumentNullException("m");
            return (T)m.GetCustomAttribute(typeof(T));
        }
        */
        #endregion

        #region SelfAndInherited

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetSelfAndInheritedTypes(this TypeInfo type)
        {
            var t = type;
            while (t != null)
            {
                yield return t;
                t = t.BaseType == null ? t.BaseType.GetTypeInfo() : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetSelfAndInherited<T>(this TypeInfo type, Func<TypeInfo, T> func)
        {
            foreach (var t in type.GetSelfAndInheritedTypes())
                yield return func(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetSelfAndInherited<T>(this TypeInfo type, Func<TypeInfo, IEnumerable<T>> func)
        {
            foreach (var t in type.GetSelfAndInheritedTypes())
                foreach (var item in func(t))
                    yield return item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetFields(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredFields);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetMembers(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredMembers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetMethods(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredMethods);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetMethods(this TypeInfo type, string name)
        {
            foreach (var method in type.GetMethods())
                if (method.Name == name)
                    yield return method;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodInfo GetMethod(this TypeInfo type, string name)
        {
            return type.GetMethods(name).First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetNestedTypes(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredNestedTypes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetProperties(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredProperties);
        }

        #endregion

        /// <summary>
        /// Gets all public fields and properties declared in a class/struct.
        /// </summary>
        /// <param name="type">The type of the class/struct.</param>
        /// <exception cref="System.ArgumentNullException">type is null.</exception>
        public static IEnumerable<MemberInfo> GetFieldsAndProperties(this TypeInfo type)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            return CollectionsUtil.Combine<MemberInfo>(type.GetFields(), type.GetProperties());
        }

        private static MemberTypes GetMemberTypeDefault(this MemberInfo member)
        {
            if (member is ConstructorInfo)
                return MemberTypes.Constructor;
            else if (member is EventInfo)
                return MemberTypes.Event;
            else if (member is FieldInfo)
                return MemberTypes.Field;
            else if (member is MethodInfo)
                return MemberTypes.Method;
            else if (member is PropertyInfo)
                return MemberTypes.Property;
            else if (member is TypeInfo)
                return MemberTypes.TypeInfo;
            else
                return MemberTypes.All;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static MemberTypes GetMemberType(this MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            try
            {
                return (MemberTypes)TargetSpecificUtil.GetMemberType.Do(member);
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
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TypeInfo GetObjTypeInfo(this object obj)
        {
            return obj.GetType().GetTypeInfo();
        }

        /// <summary>
        /// Gets the value of the field/property member of an object.
        /// </summary>
        /// <param name="member">The System.Reflection.MemberInfo object that provides information for the member.</param>
        /// <param name="obj">The object that contains the member.</param>
        /// <returns>The value of the member.</returns>
        /// <exception cref="System.ArgumentNullException">member is null.</exception>
        /// <exception cref="System.ArgumentException">member is not System.Reflection.FieldInfo or System.Reflection.PropertyInfo.</exception>
        public static object GetValue(this MemberInfo member, object obj)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            if (member is FieldInfo)
                return (member as FieldInfo).GetValue(obj);
            else if (member is PropertyInfo)
                return (member as PropertyInfo).GetValue(obj, null);
            else
                throw new ArgumentException("member is not FieldInfo or PropertyInfo");
        }

        /// <summary>
        /// Gets the type of the member.
        /// </summary>
        /// <param name="member">The System.Reflection.MemberInfo object that provides information for the member.</param>
        /// <exception cref="System.ArgumentNullException">member is null.</exception>
        /// <exception cref="System.ArgumentException">member is not System.Reflection.FieldInfo or System.Reflection.PropertyInfo.</exception>
        public static Type GetValueType(this MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            if (member is FieldInfo)
                return (member as FieldInfo).FieldType;
            else if (member is PropertyInfo)
                return (member as PropertyInfo).PropertyType;
            else
                throw new ArgumentException("member is not FieldInfo or PropertyInfo");
        }

        #endregion

        #region Set

        /// <summary>
        /// Sets the value of the field/property member of an object.
        /// </summary>
        /// <param name="member">The System.Reflection.MemberInfo object that provides information for the member.</param>
        /// <param name="obj">The object that contains the member.</param>
        /// <param name="value">The value to set.</param>
        /// <exception cref="System.ArgumentNullException">member is null.</exception>
        /// <exception cref="System.ArgumentException">member is not System.Reflection.FieldInfo or System.Reflection.PropertyInfo.</exception>
        public static void SetValue(this MemberInfo member, object obj, object value)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            if (member is FieldInfo)
                (member as FieldInfo).SetValue(obj, value);
            else if (member is PropertyInfo)
                (member as PropertyInfo).SetValue(obj, value, null);
            else
                throw new ArgumentException("member is not FieldInfo or PropertyInfo");
        }

        #endregion
    }
}
