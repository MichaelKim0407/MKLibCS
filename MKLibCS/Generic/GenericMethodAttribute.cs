using System;

namespace MKLibCS.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public enum GenericMethodType
    {
        /// <summary>
        /// 
        /// </summary>
        Method,

        /// <summary>
        /// 
        /// </summary>
        Creator,

        /// <summary>
        /// 
        /// </summary>
        Parser
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false
        )]
    public class GenericMethodAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="methodType"></param>
        /// <param name="types"></param>
        public GenericMethodAttribute(string name, GenericMethodType methodType, params Type[] types)
        {
            this.name = name;
            this.methodType = methodType;
            this.types = types;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        public GenericMethodAttribute(string name, params Type[] types)
            : this(name, GenericMethodType.Method, types)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodType"></param>
        /// <param name="types"></param>
        public GenericMethodAttribute(GenericMethodType methodType, params Type[] types)
            : this(null, methodType, types)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="types"></param>
        public GenericMethodAttribute(params Type[] types)
            : this(null, GenericMethodType.Method, types)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly string name;

        /// <summary>
        /// 
        /// </summary>
        public readonly GenericMethodType methodType;

        /// <summary>
        /// 
        /// </summary>
        public readonly Type[] types;
    }
}