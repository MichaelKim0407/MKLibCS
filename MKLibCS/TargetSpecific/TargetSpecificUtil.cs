using MKLibCS.Generic;

namespace MKLibCS.TargetSpecific
{
    /// <summary>
    /// 
    /// </summary>
    public static class TargetSpecificUtil
    {
        private static string _Target = null;

        /// <summary>
        /// 
        /// </summary>
        public static string Target
        {
            get { return _Target; }
            set
            {
                GetMemberType = GenericMethod.Get("GetMemberType" + value);
                StreamReader = GenericMethod.Get("StreamReader" + value);
                StreamWriter = GenericMethod.Get("StreamWriter" + value);
                _Target = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public const string Framework45 = "4.5";

        /// <summary>
        /// 
        /// </summary>
        public const string Universal = "UWP";

        /// <summary>
        /// 
        /// </summary>
        public static GenericMethod GetMemberType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static GenericMethod StreamReader { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static GenericMethod StreamWriter { get; private set; }
    }
}