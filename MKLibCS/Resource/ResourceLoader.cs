using System;
using MKLibCS.System;
#if LEGACY
using MKLibCS.Reflection;

#else
using System.Reflection;

#endif

namespace MKLibCS.Resource
{
    /// <summary>
    /// </summary>
    public abstract class ResourceLoader
    {
        private Path rootDir;

        /// <summary>
        /// </summary>
        /// <param name="rootDir"></param>
        protected ResourceLoader(Path rootDir)
        {
            this.rootDir = rootDir.AbsPath;
        }

        /// <summary>
        /// </summary>
        /// <param name="rootDir"></param>
        protected ResourceLoader(string rootDir)
            : this(new Path(rootDir))
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="absPath"></param>
        /// <returns></returns>
        protected abstract object LoadFromResourceFileAbs(Type type, Path absPath);

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        protected object LoadFromResourceFile(Type type, Path path)
        {
            return LoadFromResourceFileAbs(type, rootDir + path);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public T LoadFromResourceFile<T>(Path path)
        {
            return (T) LoadFromResourceFile(typeof(T), path);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadFromResourceFile<T>()
        {
            var attr = typeof(T).GetTypeInfo().GetCustomAttribute<ResourceAttribute>();
            if (attr == null || attr.Type != ResourceType.SingleFile)
                throw new ArgumentException(nameof(T));
            return LoadFromResourceFile<T>(attr.Path);
        }
    }
}