using System;

namespace MKLibCS.Logging
{
    /// <summary>
    /// </summary>
    public interface ILogHandler
    {
        /// <summary>
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void Message(Logger.Level lvl, string name, string msg, object[] args);

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        void Exception(string name, Exception e, string msg, object[] args);
    }
}