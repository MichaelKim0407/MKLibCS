using MKLibCS.Generic;

namespace MKLibCS.TargetSpecific
{
    /// <summary>
    /// 
    /// </summary>
    static public class TargetSpecificUtil
    {
        static private string _Target = null;
        /// <summary>
        /// 
        /// </summary>
        static public string Target
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
        static public GenericMethod GetMemberType { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        static public GenericMethod StreamReader { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        static public GenericMethod StreamWriter { get; private set; }
    }
}
