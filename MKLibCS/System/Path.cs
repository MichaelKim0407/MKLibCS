using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MKLibCS.Collections;
using MKLibCS.TargetSpecific;

namespace MKLibCS.System
{
    /// <summary>
    /// </summary>
    public sealed class Path
    {
        /// <summary>
        /// </summary>
        public const char WindowsDiscSeperator = ':';

        /// <summary>
        /// </summary>
        public const char WindowsPathSeperator = '\\';

        /// <summary>
        /// </summary>
        public const char UnixPathSeperator = '/';

        /// <summary>
        /// </summary>
        public static readonly char[] PathSeperators =
        {
            WindowsPathSeperator,
            UnixPathSeperator
        };

        private const string WindowsDiscSeperatorStr = ":";
        private const string WindowsDiscPathSeperatorStr = "\\\\";
        private const string WindowsPathSeperatorStr = "\\";
        private const string UnixPathSeperatorStr = "/";

        private readonly string disc;
        private readonly List<string> directories;

        private readonly string path;

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        public Path(string path)
        {
            if (path.Contains(WindowsDiscSeperatorStr))
            {
                var i = path.IndexOf(WindowsDiscSeperator);
                disc = path.Substring(0, i + 1);
                path = path.Substring(i + 1);
            }
            else if (path.StartsWith(UnixPathSeperatorStr))
            {
                disc = "";
            }
            else
            {
                disc = null;
            }
            directories = Split(path);
            this.path = GetPathString();
        }

        /// <summary>
        /// </summary>
        public Path()
            : this(".")
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="directories"></param>
        public Path(string path, params string[] directories)
            : this(path)
        {
            Append(directories);
            this.path = GetPathString();
        }

        private Path(Path child, int upperDirectory)
        {
            disc = child.disc;
            directories = child.directories.Take(0, child.directories.Count - upperDirectory).ToList();
            path = GetPathString();
        }

        private static List<string> Split(string dir)
        {
            var result = dir.Split(PathSeperators).ToList();
            result.RemoveAll(s => s == "");
            return result;
        }

        private string GetPathString()
        {
            switch (disc)
            {
                case null:
                    return directories.ToString(UnixPathSeperatorStr);
                case "":
                    return UnixPathSeperatorStr + directories.ToString(UnixPathSeperatorStr);
                default:
                    return disc + WindowsDiscPathSeperatorStr + directories.ToString(WindowsPathSeperatorStr);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return path;
        }

        private void Append(params string[] directories)
        {
            foreach (var dir in directories)
                this.directories.AddRange(Split(dir));
        }

        /// <summary>
        /// </summary>
        public bool IsAbsPath => disc != null;

        /// <summary>
        /// </summary>
        public Path AbsPath
        {
            get
            {
                if (IsAbsPath)
                    return this;
                var absPath = (string) TargetSpecificUtil.GetFullPath.Do(path);
                return new Path(absPath);
            }
        }

        /// <summary>
        /// </summary>
        public static Path CurPath => new Path().AbsPath;

        /// <summary>
        /// </summary>
        public Path Parent => new Path(this, 1);

        /// <summary>
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public Path GetParent(int level)
        {
            return new Path(this, level);
        }

        /// <summary>
        /// </summary>
        public string Name => directories.Last();

        /// <summary>
        /// </summary>
        public bool IsFile => File.Exists(path);

        /// <summary>
        /// </summary>
        public bool IsDirectory => Directory.Exists(path);

        /// <summary>
        /// </summary>
        public bool Exists => IsFile || IsDirectory;

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Path> ListDir()
        {
#if V3
            return Directory.GetFileSystemEntries(path)
#else
            return Directory.EnumerateFileSystemEntries(path)
#endif
                .Select(name => new Path(name));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Path> ListFiles()
        {
#if V3
            return Directory.GetFiles(path)
#else
            return Directory.EnumerateFiles(path)
#endif
                .Select(name => new Path(name));
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Path> ListSubdirectories()
        {
#if V3
            return Directory.GetDirectories(path)
#else
            return Directory.EnumerateDirectories(path)
#endif
                .Select(name => new Path(name));
        }

        private Path(Path parent, params string[] directories)
        {
            disc = parent.disc;
            this.directories = new List<string>();
            this.directories.AddRange(parent.directories);
            Append(directories);
            path = GetPathString();
        }

        /// <summary>
        /// </summary>
        /// <param name="directories"></param>
        /// <returns></returns>
        public Path Join(params string[] directories)
        {
            return new Path(this, directories);
        }

        /// <summary>
        /// </summary>
        /// <param name="root"></param>
        /// <param name="relative"></param>
        /// <returns></returns>
        public static Path operator +(Path root, Path relative)
        {
            if (relative.IsAbsPath)
                throw new ArgumentException(nameof(relative));
            return new Path(root, relative.directories.ToArray());
        }

        private bool Equals(Path other)
        {
            return string.Equals(path, other.path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Path && Equals((Path)obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return path?.GetHashCode() ?? 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool operator ==(Path path1, Path path2)
        {
            return path1 == null ? path2 == null : path2 != null && path1.path == path2.path;
        }

        /// <summary>
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool operator !=(Path path1, Path path2)
        {
            return !(path1 == path2);
        }

        /// <summary>
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool operator <(Path path1, Path path2)
        {
            if (path1 == null || path2 == null)
                return false;
            return path2.path.Length > path1.path.Length && path2.path.StartsWith(path1.path);
        }

        /// <summary>
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool operator >(Path path1, Path path2)
        {
            return path2 < path1;
        }

        /// <summary>
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool operator <=(Path path1, Path path2)
        {
            return path1 == path2 || path1 < path2;
        }

        /// <summary>
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static bool operator >=(Path path1, Path path2)
        {
            return path1 == path2 || path1 > path2;
        }

        /// <summary>
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public Path RelativeTo(Path root)
        {
            if (this == root)
                return new Path();
            if (this > root)
                return new Path((string) null, directories.Take(root.directories.Count).ToArray());
            throw new ArgumentException(nameof(root));
        }
    }
}