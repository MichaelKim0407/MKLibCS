using System;
using System.Reflection;
using MKLibCS.Reflection;

namespace MKLibCS.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public struct ExceptionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public static ExceptionInfo Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        public ExceptionInfo(MemberInfo memberInfo)
        {
            DeclaringType = memberInfo.DeclaringType;
            MemberType = memberInfo.GetMemberType();
            Name = memberInfo.Name;
            ValueType = memberInfo.GetValueType();
        }

        /// <summary>
        /// 
        /// </summary>
        public Type DeclaringType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public MemberTypes MemberType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type ValueType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static implicit operator ExceptionInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
                return Empty;
            return new ExceptionInfo(memberInfo);
        }
    }
}