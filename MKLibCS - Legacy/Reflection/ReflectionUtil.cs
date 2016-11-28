using System;
using System.Reflection;

namespace MKLibCS.Reflection
{
    using TypeInfo = Type;

    /// <summary>
    /// </summary>
    public static partial class ReflectionUtil
    {
        #region GetCustomAttribute

        /// <summary>
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static Attribute GetCustomAttribute(this Assembly assembly, Type attributeType)
        {
            return Attribute.GetCustomAttribute(assembly, attributeType);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assembly"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">assembly is null.</exception>
        public static T GetCustomAttribute<T>(this Assembly assembly)
            where T : Attribute
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            return (T) assembly.GetCustomAttribute(typeof(T));
        }

        /// <summary>
        ///     Gets the attribute of type T applied the a member.
        /// </summary>
        /// <param name="m">The member.</param>
        /// <param name="attributeType">The type of the attribute.</param>
        public static Attribute GetCustomAttribute(this MemberInfo m, Type attributeType)
        {
            return Attribute.GetCustomAttribute(m, attributeType);
        }

        /// <summary>
        ///     Gets the attribute of type T applied the a member.
        /// </summary>
        /// <typeparam name="T">The type of the attribute.</typeparam>
        /// <param name="m">The member.</param>
        /// <exception cref="System.ArgumentNullException">m is null.</exception>
        public static T GetCustomAttribute<T>(this MemberInfo m)
            where T : Attribute
        {
            if (m == null)
                throw new ArgumentNullException(nameof(m));
            return (T) m.GetCustomAttribute(typeof(T));
        }

        #endregion

        #region TypeInfo

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TypeInfo GetTypeInfo(this Type type)
        {
            return type;
        }

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Delegate CreateDelegate(this MethodInfo method, Type type)
        {
            return Delegate.CreateDelegate(type, method);
        }
    }
}