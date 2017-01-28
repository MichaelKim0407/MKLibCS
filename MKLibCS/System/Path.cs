using System.Collections.Generic;
using System.Linq;
using MKLibCS.Collections;

namespace MKLibCS.System
{
    /// <summary>
    /// TODO: WIP
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

        private string disc;
        private List<string> directories;

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
            Join(directories);
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

        /// <summary>
        /// </summary>
        /// <param name="directories"></param>
        public void Join(params string[] directories)
        {
            foreach (var dir in directories)
                this.directories.AddRange(Split(dir));
        }

        /// <summary>
        /// </summary>
        public bool IsAbsPath => disc != null;
    }
}