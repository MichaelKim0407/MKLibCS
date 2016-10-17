using System;

namespace MKLibCS.Generic
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = false
        )]
    public class GenericUsageAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prerequisites"></param>
        public GenericUsageAttribute(params Type[] prerequisites)
        {
            this.prerequisites = prerequisites;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly Type[] prerequisites;
    }
}