using System;
using System.Collections.Generic;
using System.IO;

using MKLibCS.TargetSpecific;

namespace MKLibCS.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// 
            /// </summary>
            ALL = 0,
            /// <summary>
            /// 
            /// </summary>
            INTERNAL_DEBUG = 5,
            /// <summary>
            /// 
            /// </summary>
            DEBUG = 10,
            /// <summary>
            /// 
            /// </summary>
            INTERNAL_INFO = 15,
            /// <summary>
            /// 
            /// </summary>
            INFO = 20,
            /// <summary>
            /// 
            /// </summary>
            INTERNAL_WARNING = 25,
            /// <summary>
            /// 
            /// </summary>
            WARNING = 30,
            /// <summary>
            /// 
            /// </summary>
            INTERNAL_ERROR = 35,
            /// <summary>
            /// 
            /// </summary>
            ERROR = 40
        }

        /// <summary>
        /// 
        /// </summary>
        public const Level DefaultLevel = Level.INFO;
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultFormat = "[%LV %T %NAME] %MSG";
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultTimeFormat = "yyyyMMddHHmmssffff";

        private class LogFile : ILogFile
        {
            public string FileName { get; set; }
            public Level Level { get; set; }
            public string Format { get; set; }
            public string TimeFormat { get; set; }

            public LogFile(string fileName)
            {
                this.FileName = fileName;
                this.Level = DefaultLevel;
                this.Format = DefaultFormat;
                this.TimeFormat = DefaultTimeFormat;
            }
            public LogFile(string fileName, Level level)
                : this(fileName)
            {
                Level = level;
            }
            public LogFile(string fileName, string format, string timeFormat = DefaultTimeFormat)
                : this(fileName)
            {
                Format = format;
                TimeFormat = timeFormat;
            }
            public LogFile(string fileName, Level level, string format, string timeFormat = DefaultTimeFormat)
                : this(fileName, format, timeFormat)
            {
                Level = level;
            }

            public bool Enabled { get; set; }

            private void WriteLines(params string[] lines)
            {
                StreamWriter writer = TargetSpecificUtil.StreamWriter.Do(FileName, true) as StreamWriter;
                // StreamWriter writer = new StreamWriter(FileName, true);
                foreach (var line in lines)
                    writer.WriteLine(line);
                writer.Dispose();
            }

            public void Message(Level level, string name, string msg, params object[] args)
            {
                if (!Enabled)
                    return;
                if (level < Level)
                    return;
                var curTime = DateTime.Now;
                var line = Format.Replace("%LV", level.ToString())
                    .Replace("%T", curTime.ToString(TimeFormat))
                    .Replace("%NAME", name)
                    .Replace("%MSG", string.Format(msg, args));
                WriteLines(line);
            }

            public void Exception(string name, Exception e, string msg, params object[] args)
            {
                if (!Enabled)
                    return;
                var curTime = DateTime.Now;
                var line = Format.Replace("%LV", "ERROR")
                    .Replace("%T", curTime.ToString(TimeFormat))
                    .Replace("%NAME", name)
                    .Replace("%MSG", string.Format(msg, args));
                var ex = e.ToString();
                WriteLines(line, ex);
            }
        }

        static private List<ILogFile> files = new List<ILogFile>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static public ILogFile AddLogFile(ILogFile file)
        {
            files.Add(file);
            return file;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public ILogFile AddLogFile(string fileName)
        {
            var file = new LogFile(fileName);
            files.Add(file);
            return file;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="level"></param>
        /// <param name="format"></param>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        static public ILogFile AddLogFile(string fileName, Level level, string format, string timeFormat)
        {
            var file = new LogFile(fileName, level, fileName, timeFormat);
            files.Add(file);
            return file;
        }

        private string name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Log(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public Log(Type type)
        {
            this.name = type.FullName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Message(Level lvl, string msg, params object[] args)
        {
            foreach (var file in files)
                file.Message(lvl, name, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Message(uint lvl, string msg, params object[] args)
        {
            Message((Level)lvl, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalDebug(string msg, params object[] args)
        {
            Message(Level.INTERNAL_DEBUG, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Debug(string msg, params object[] args)
        {
            Message(Level.DEBUG, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalInfo(string msg, params object[] args)
        {
            Message(Level.INTERNAL_INFO, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Info(string msg, params object[] args)
        {
            Message(Level.INFO, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalWarning(string msg, params object[] args)
        {
            Message(Level.INTERNAL_WARNING, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Warning(string msg, params object[] args)
        {
            Message(Level.WARNING, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalError(string msg, params object[] args)
        {
            Message(Level.INTERNAL_ERROR, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Error(string msg, params object[] args)
        {
            Message(Level.ERROR, msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Exception(Exception e, string msg, params object[] args)
        {
            foreach (var file in files)
                file.Exception(name, e, msg, args);
        }
    }
}
