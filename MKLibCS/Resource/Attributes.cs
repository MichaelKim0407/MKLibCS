using System;
using MKLibCS.System;

namespace MKLibCS.Resource
{
    /// <summary>
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// </summary>
        SingleFile,

        /// <summary>
        /// </summary>
        Dir
    }

    /// <summary>
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Struct,
        AllowMultiple = false,
        Inherited = false)]
    public class ResourceAttribute : Attribute
    {
        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        public ResourceAttribute(ResourceType type, string path)
        {
            Type = type;
            Path = new Path(path);
        }

        /// <summary>
        /// </summary>
        public readonly ResourceType Type;

        /// <summary>
        /// </summary>
        public readonly Path Path;
    }

    /// <summary>
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Field | AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = true)]
    public class RequiredResourceAttribute : Attribute
    {
    }
}