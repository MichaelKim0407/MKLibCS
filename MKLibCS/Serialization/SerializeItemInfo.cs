using System;
using System.Reflection;
using MKLibCS.Reflection;

namespace MKLibCS.Serialization
{
    /// <summary>
    /// 
    /// </summary>
    public struct SerializeItemInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public static SerializeItemInfo Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        public SerializeItemInfo(MemberInfo memberInfo)
        {
            Type = memberInfo.GetValueType();
            Attr = memberInfo.GetSerializeItemAttribute();
        }

        /// <summary>
        /// 
        /// </summary>
        public SerializeItemAttribute Attr { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static implicit operator SerializeItemInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
                return Empty;
            return new SerializeItemInfo(memberInfo);
        }
    }
}