using System;
using System.Reflection;
using MKLibCS.Reflection;

namespace MKLibCS.File
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
            this.DeclaringType = memberInfo.DeclaringType;
            this.MemberType = memberInfo.GetMemberType();
            this.Name = memberInfo.Name;
            this.ValueType = memberInfo.GetValueType();
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
            else
                return new ExceptionInfo(memberInfo);
        }
    }
}