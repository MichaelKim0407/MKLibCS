using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MKLibCS.Reflection
{
    /// <summary>
    /// </summary>
    public static partial class ReflectionUtil
    {
        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type[] GetGenericArguments(this TypeInfo type)
        {
            return type.GenericTypeArguments;
        }

        /// <summary>
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetTypes(this Assembly assembly)
        {
            return assembly.DefinedTypes;
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static PropertyInfo GetProperty(this TypeInfo type, string name)
        {
            return type.GetDeclaredProperty(name);
        }

        /// <summary>
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static MethodInfo GetGetMethod(this PropertyInfo property)
        {
            return property.GetMethod;
        }

        #region SelfAndInherited

        /// <summary>
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
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<FieldInfo> GetFields(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredFields);
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<MemberInfo> GetMembers(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredMembers);
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<MethodInfo> GetMethods(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredMethods);
        }

        /// <summary>
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
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodInfo GetMethod(this TypeInfo type, string name)
        {
            return type.GetMethods(name).First();
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<TypeInfo> GetNestedTypes(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredNestedTypes);
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetProperties(this TypeInfo type)
        {
            return type.GetSelfAndInherited(t => t.DeclaredProperties);
        }

        #endregion
    }
}