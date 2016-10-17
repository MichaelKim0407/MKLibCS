using System;

namespace MKLibCS.File
{
    /// <summary>
    /// 
    /// </summary>
    public enum FileSLCustomMethod
    {
        /// <summary>
        /// 
        /// </summary>
        Single,

        /// <summary>
        /// 
        /// </summary>
        Struct,

        /// <summary>
        /// 
        /// </summary>
        Complex
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = false
        )]
    public class FileSLCustomAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        public FileSLCustomAttribute(FileSLCustomMethod method)
        {
            this.method = method;
        }

        /// <summary>
        /// 
        /// </summary>
        public FileSLCustomMethod method;

        /// <summary>
        /// 
        /// </summary>
        public string SaveMethod = "Save";

        /// <summary>
        /// 
        /// </summary>
        public string LoadMethod = "Load";

        /// <summary>
        /// 
        /// </summary>
        public bool LoadDefault = false;

        /// <summary>
        /// 
        /// </summary>
        public string LoadDefaultMethod = "LoadDefault";
    }

    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true
        )]
    public class FileSLItemAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public FileSLItemAttribute()
        {
            this.saveName = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="saveName"></param>
        public FileSLItemAttribute(string saveName)
        {
            this.saveName = saveName;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly string saveName;

        /// <summary>
        /// <para>Gets or sets whether the item will be skipped when saving if it is null,</para>
        /// <para>and will be assigned the null value when reading if the item cannot be found.</para>
        /// <para>If false, an exception will be thrown upon saving an null object.</para>
        /// <para>Default is false.</para>
        /// </summary>
        public bool skipNull = false;

        /// <summary>
        /// <para>Gets or sets whether the item will be skipped when saving if it is an empty string,</para>
        /// <para>and will be assigned an empty string when reading if the item cannot be found and skipNull is false.</para>
        /// <para>If false, an exception will be thrown upon saving an empty string.</para>
        /// <para>Default is false. It is considered false when the item is not of type string.</para>
        /// </summary>
        public bool skipEmptyString = false;

        /// <summary>
        /// <para>Gets or sets whether the item will be skipped when reading if the node cannot be found and both skipNull and skipEmptyString are false.</para>
        /// <para>If false, an exception will be thrown when the item cannot be found, and both skipNull and skipEmptyString are false.</para>
        /// <para>Default is true.</para>
        /// </summary>
        public bool useDefault = true;

        /// <summary>
        /// 
        /// </summary>
        public bool isTesting = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SkipItem(object value)
        {
            if (isTesting)
                return true;
            else if (value == null)
                return skipNull;
            else if (value is string && (string) value == "")
                return skipEmptyString;
            else
                return false;
        }
    }
}