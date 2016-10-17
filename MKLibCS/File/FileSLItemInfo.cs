using System;
using System.Reflection;
using MKLibCS.Reflection;

namespace MKLibCS.File
{
    /// <summary>
    /// 
    /// </summary>
    public struct FileSLItemInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public static FileSLItemInfo Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        public FileSLItemInfo(MemberInfo memberInfo)
        {
            this.Type = memberInfo.GetValueType();
            this.Attr = memberInfo.GetFileSLItemAttribute();
        }

        /// <summary>
        /// 
        /// </summary>
        public FileSLItemAttribute Attr { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static implicit operator FileSLItemInfo(MemberInfo memberInfo)
        {
            if (memberInfo == null)
                return Empty;
            else
                return new FileSLItemInfo(memberInfo);
        }
    }
}