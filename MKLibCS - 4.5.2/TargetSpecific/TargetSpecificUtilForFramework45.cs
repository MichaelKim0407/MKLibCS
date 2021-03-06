﻿using System.IO;
using System.Reflection;
using System.Text;
using MemberTypes = MKLibCS.Reflection.MemberTypes;

namespace MKLibCS.TargetSpecific
{
    /// <summary>
    ///     Provides implementation of target-specific methods for .NET Framework 4.5
    /// </summary>
    public class TargetSpecificUtilForFramework45
    {
        /// <summary>
        /// </summary>
        public const string Framework45 = "4.5";

        /// <summary>
        /// </summary>
        public static void Init()
        {
            TargetSpecificUtil.Target = Framework45;

            TargetSpecificUtil.GetMemberType.Add<MemberInfo>(m => (MemberTypes) m.MemberType);

            TargetSpecificUtil.StreamReader.Add<string>(path => new StreamReader(path));
            TargetSpecificUtil.StreamReader.Add<string, Encoding>((path, encoding) => new StreamReader(path, encoding));

            TargetSpecificUtil.StreamWriter.Add<string>(path => new StreamWriter(path));
            TargetSpecificUtil.StreamWriter.Add<string, bool>((path, append) => new StreamWriter(path, append));
            TargetSpecificUtil.StreamWriter.Add<string, bool, Encoding>(
                (path, append, encoding) => new StreamWriter(path, append, encoding));
        }
    }
}