using System;

namespace MKLibCS.Serialization
{
    /// <summary>
    /// </summary>
    public enum SerializeObjectMethod
    {
        /// <summary>
        ///     When serialized, the object will be treated as a single value.
        /// </summary>
        Single,

        /// <summary>
        ///     When serialized, the object will be turned into a node.
        /// </summary>
        Struct,

        /// <summary>
        ///     Custom behaviour is defined for serialization.
        /// </summary>
        Custom
    }

    /// <summary>
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = false
        )]
    public class SerializeObjectAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        public SerializeObjectAttribute(SerializeObjectMethod method)
        {
            this.Method = method;
        }

        /// <summary>
        /// </summary>
        public SerializeObjectMethod Method;

        /// <summary>
        /// </summary>
        public string SaveMethod = "Save";

        /// <summary>
        /// </summary>
        public string LoadMethod = "Load";

        /// <summary>
        /// </summary>
        public bool LoadDefault = false;

        /// <summary>
        /// </summary>
        public string LoadDefaultMethod = "LoadDefault";
    }

    /// <summary>
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true
        )]
    public class SerializeItemAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        public SerializeItemAttribute()
        {
            this.SerializeName = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serializeName"></param>
        public SerializeItemAttribute(string serializeName)
        {
            this.SerializeName = serializeName;
        }

        /// <summary>
        /// </summary>
        public readonly string SerializeName;

        /// <summary>
        ///     <para>
        ///         Gets or sets whether the item will be skipped when serialized
        ///         if it is null,
        ///     </para>
        ///     <para>
        ///         and will be assigned the null value when de-serialized
        ///         if the item cannot be found.
        ///     </para>
        ///     <para>If false, an exception will be thrown upon serializing an null object.</para>
        ///     <para>Default is false.</para>
        /// </summary>
        public bool SkipNull = false;

        /// <summary>
        ///     <para>
        ///         Gets or sets whether the item will be skipped when serialized
        ///         if it is an empty string,
        ///     </para>
        ///     <para>
        ///         and will be assigned an empty string when de-serializing
        ///         if the item cannot be found and skipNull is false.
        ///     </para>
        ///     <para>If false, an exception will be thrown upon serializing an empty string.</para>
        ///     <para>Default is false. It is considered false when the item is not of type string.</para>
        /// </summary>
        public bool SkipEmptyString = false;

        /// <summary>
        ///     <para>
        ///         Gets or sets whether the item will be skipped when de-serializing
        ///         if the node cannot be found and both skipNull and skipEmptyString are false.
        ///     </para>
        ///     <para>
        ///         If false, an exception will be thrown when the item cannot be found,
        ///         and both skipNull and skipEmptyString are false.
        ///     </para>
        ///     <para>Default is true.</para>
        /// </summary>
        public bool UseDefault = true;

        /// <summary>
        /// </summary>
        public bool Ignore = false;

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SkipItem(object value)
        {
            if (Ignore)
                return true;
            else if (value == null)
                return SkipNull;
            else if (value is string && (string) value == "")
                return SkipEmptyString;
            else
                return false;
        }
    }
}