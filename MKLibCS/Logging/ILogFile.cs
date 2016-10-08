using System;

namespace MKLibCS.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILogFile
    {
        /// <summary>
        /// 
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Log.Level Level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string Format { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string TimeFormat { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void Message(Log.Level lvl, string name, string msg, object[] args);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void Exception(string name, Exception e, string msg, object[] args);
    }
}
