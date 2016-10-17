using System;
using MKLibCS.Collections;

namespace MKLibCS.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public class MissingGenericMethodException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="types"></param>
        public MissingGenericMethodException(string name, params Type[] types)
        {
            this.name = name;
            this.types = types;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly string name;

        /// <summary>
        /// 
        /// </summary>
        public readonly Type[] types;

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get { return "Method " + name + " does not support types [" + types.ToString(", ") + "]"; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class CreateInstanceFailureException : MissingGenericMethodException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public CreateInstanceFailureException(Type type)
            : base("Create", type)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Type type
        {
            get { return types[0]; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return "Type \"" + type.Name + "\" does not have a default constructor. "
                       + "Neither is it supported for a default creation.";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class BindingNotFoundException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        public BindingNotFoundException(string methodName)
        {
            this.methodName = methodName;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly string methodName;

        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                return "Method " + methodName +
                       " does not have a default binding. Please specify its generic method name.";
            }
        }
    }
}