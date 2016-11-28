using System.IO;
using System.Reflection;
using System.Text;

namespace MKLibCS.TargetSpecific
{
    /// <summary>
    /// </summary>
    public static class TargetSpecificUtilForLegacy
    {
        /// <summary>
        /// </summary>
        public const string FrameworkLegacy = "LEGACY";

        /// <summary>
        /// </summary>
        public static void Init()
        {
            TargetSpecificUtil.Target = FrameworkLegacy;

            TargetSpecificUtil.GetMemberType.Add<MemberInfo>(m => m.MemberType);

            TargetSpecificUtil.StreamReader.Add<string>(path => new StreamReader(path));
            TargetSpecificUtil.StreamReader.Add<string, Encoding>((path, encoding) => new StreamReader(path, encoding));

            TargetSpecificUtil.StreamWriter.Add<string>(path => new StreamWriter(path));
            TargetSpecificUtil.StreamWriter.Add<string, bool>((path, append) => new StreamWriter(path, append));
            TargetSpecificUtil.StreamWriter.Add<string, bool, Encoding>(
                (path, append, encoding) => new StreamWriter(path, append, encoding));
        }
    }
}