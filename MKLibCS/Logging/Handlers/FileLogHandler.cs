using System;
using System.IO;
using MKLibCS.TargetSpecific;

namespace MKLibCS.Logging.Handlers
{
    /// <summary>
    /// </summary>
    public class FileLogHandler : ILogHandler
    {
        /// <summary>
        /// </summary>
        public const Logger.Level DefaultLevel = Logger.Level.Info;

        /// <summary>
        /// </summary>
        public const string DefaultFormat = "[%LV %T %NAME] %MSG";

        /// <summary>
        /// </summary>
        public const string DefaultTimeFormat = "yyyyMMddHHmmssffff";

        /// <summary>
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// </summary>
        public Logger.Level Level { get; set; }

        /// <summary>
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// </summary>
        public string TimeFormat { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="fileName"></param>
        public FileLogHandler(string fileName)
        {
            FileName = fileName;
            Level = DefaultLevel;
            Format = DefaultFormat;
            TimeFormat = DefaultTimeFormat;
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="level"></param>
        public FileLogHandler(string fileName, Logger.Level level)
            : this(fileName)
        {
            Level = level;
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="format"></param>
        /// <param name="timeFormat"></param>
        public FileLogHandler(
            string fileName,
            string format,
            string timeFormat = DefaultTimeFormat
            )
            : this(fileName)
        {
            Format = format;
            TimeFormat = timeFormat;
        }

        /// <summary>
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="level"></param>
        /// <param name="format"></param>
        /// <param name="timeFormat"></param>
        public FileLogHandler(
            string fileName,
            Logger.Level level,
            string format,
            string timeFormat = DefaultTimeFormat
            )
            : this(fileName, format, timeFormat)
        {
            Level = level;
        }

        /// <summary>
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="lines"></param>
        private void WriteLines(params string[] lines)
        {
            var writer = TargetSpecificUtil.StreamWriter.Do(FileName, true) as StreamWriter;
            // StreamWriter writer = new StreamWriter(FileName, true);
            foreach (var line in lines)
                writer.WriteLine(line);
            writer.Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="level"></param>
        /// <param name="name"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Message(
            Logger.Level level,
            string name,
            string msg,
            params object[] args
            )
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

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Exception(
            string name,
            Exception e,
            string msg,
            params object[] args
            )
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
}