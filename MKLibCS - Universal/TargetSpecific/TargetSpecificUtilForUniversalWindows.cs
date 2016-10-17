using System;
using System.IO;
using System.Text;

namespace MKLibCS.TargetSpecific
{
    /// <summary>
    /// Provides implementation of target-specific methods for Universal Windows Platform
    /// </summary>
    public class TargetSpecificUtilForUniversalWindows
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            TargetSpecificUtil.Target = TargetSpecificUtil.Universal;

            // TargetSpecificUtil.GetMemberType.Add<MemberInfo>(m => (MKLibCS.Reflection.MemberTypes)m.MemberType);

            Func<string, FileStream> openFileForRead = path => new FileStream(path, FileMode.Open, FileAccess.Read);

            TargetSpecificUtil.StreamReader.Add<string>(
                path =>
                new StreamReader(openFileForRead(path)));
            TargetSpecificUtil.StreamReader.Add<string, Encoding>(
                (path, encoding) =>
                new StreamReader(openFileForRead(path), encoding));

            Func<string, bool, FileStream> openFileForWrite = (path, append)
                => new FileStream(path, append ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write);

            TargetSpecificUtil.StreamWriter.Add<string>(
                path =>
                new StreamWriter(openFileForWrite(path, false)));
            TargetSpecificUtil.StreamWriter.Add<string, bool>(
                (path, append) =>
                new StreamWriter(openFileForWrite(path, append)));
            TargetSpecificUtil.StreamWriter.Add<string, bool, Encoding>(
                (path, append, encoding) =>
                new StreamWriter(openFileForWrite(path, append), encoding));
        }
    }
}
