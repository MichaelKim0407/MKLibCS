using System.Collections.Generic;
using System.Linq;
using MKLibCS.Collections;
using MKLibCS.TargetSpecific;

namespace MKLibCS.System
{
    /// <summary>
    /// </summary>
    public class Path
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
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="directories"></param>
        public Path(string path, params string[] directories)
            : this(path)
        {
            Append(directories);
        }

        /// <summary>
        /// </summary>
        public Path()
            : this(".")
        {
        }

        private Path(Path parent, params string[] directories)
        {
            disc = parent.disc;
            this.directories = new List<string>();
            this.directories.AddRange(parent.directories);
            Append(directories);
        }

        private static List<string> Split(string dir)
        {
            var result = dir.Split(PathSeperators).ToList();
            result.RemoveAll(s => s == "");
            return result;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
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

        private void Append(params string[] directories)
        {
            foreach (var dir in directories)
                this.directories.AddRange(Split(dir));
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
        public bool IsAbsPath => disc != null;

        /// <summary>
        /// </summary>
        public Path AbsPath
        {
            get
            {
                if (IsAbsPath)
                    return this;
                var absPath = (string) TargetSpecificUtil.GetFullPath.Do(ToString());
                return new Path(absPath);
            }
        }

        /// <summary>
        /// </summary>
        public static Path CurPath => new Path().AbsPath;
    }
}