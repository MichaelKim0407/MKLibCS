﻿using System;
using System.Collections.Generic;

namespace MKLibCS.Logging
{
    /// <summary>
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// </summary>
            All = 0,

            /// <summary>
            /// </summary>
            InternalDebug = 5,

            /// <summary>
            /// </summary>
            Debug = 10,

            /// <summary>
            /// </summary>
            InternalInfo = 15,

            /// <summary>
            /// </summary>
            Info = 20,

            /// <summary>
            /// </summary>
            InternalWarning = 25,

            /// <summary>
            /// </summary>
            Warning = 30,

            /// <summary>
            /// </summary>
            InternalError = 35,

            /// <summary>
            /// </summary>
            Error = 40
        }

        private static List<ILogHandler> handlers = new List<ILogHandler>();

        /// <summary>
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static ILogHandler AddLogHandler(ILogHandler handler)
        {
            handlers.Add(handler);
            return handler;
        }

        private string name;

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        public Logger(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        public Logger(Type type)
        {
            name = type.FullName;
        }

        /// <summary>
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Message(Level lvl, string msg, params object[] args)
        {
            foreach (var handler in handlers)
                handler.Message(lvl, name, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="lvl"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Message(uint lvl, string msg, params object[] args)
        {
            Message((Level) lvl, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalDebug(string msg, params object[] args)
        {
            Message(Level.InternalDebug, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Debug(string msg, params object[] args)
        {
            Message(Level.Debug, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalInfo(string msg, params object[] args)
        {
            Message(Level.InternalInfo, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Info(string msg, params object[] args)
        {
            Message(Level.Info, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalWarning(string msg, params object[] args)
        {
            Message(Level.InternalWarning, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Warning(string msg, params object[] args)
        {
            Message(Level.Warning, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void InternalError(string msg, params object[] args)
        {
            Message(Level.InternalError, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Error(string msg, params object[] args)
        {
            Message(Level.Error, msg, args);
        }

        /// <summary>
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Exception(Exception e, string msg, params object[] args)
        {
            foreach (var handler in handlers)
                handler.Exception(name, e, msg, args);
        }
    }
}