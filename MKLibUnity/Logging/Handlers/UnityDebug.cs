using System;
using MKLibCS.Logging;
using UnityEngine;
using ILogHandler = MKLibCS.Logging.ILogHandler;

namespace MKLibCS.Unity.Logging.Handlers
{
    /// <summary>
    /// </summary>
    public class UnityDebug : ILogHandler
    {
        /// <summary>
        /// </summary>
        public const Log.Level DefaultLevel = Log.Level.Info;

        /// <summary>
        /// </summary>
        public const string DefaultFormat = "[%T %NAME] %MSG";

        /// <summary>
        /// </summary>
        public const string DefaultTimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// </summary>
        public Log.Level Level { get; set; }

        /// <summary>
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// </summary>
        public string TimeFormat { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="level"></param>
        public UnityDebug(Log.Level level = DefaultLevel)
        {
            Level = level;
            Format = DefaultFormat;
            TimeFormat = DefaultTimeFormat;
        }

        /// <summary>
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Message(Log.Level lvl, string name, string msg, object[] args)
        {
            if (lvl < Level)
                return;
            var curTime = DateTime.Now;
            var line = Format.Replace("%LV", lvl.ToString())
                .Replace("%T", curTime.ToString(TimeFormat))
                .Replace("%NAME", name)
                .Replace("%MSG", string.Format(msg, args));
            if (lvl < Log.Level.InternalWarning)
                Debug.Log(line);
            else if (lvl < Log.Level.InternalError)
                Debug.LogWarning(line);
            else
                Debug.LogError(line);
        }

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Exception(string name, Exception e, string msg, object[] args)
        {
            Debug.LogException(e);
        }
    }
}