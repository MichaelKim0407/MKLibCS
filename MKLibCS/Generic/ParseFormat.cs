using System;

using Regex = System.Text.RegularExpressions.Regex;

namespace MKLibCS.Generic
{
    partial class GenericUtil
    {
        /// <summary>
        /// 
        /// </summary>
        static public GenericMethod Parse { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        static public GenericMethod Format { get; private set; }

        static private void InitParseFormat()
        {
            Parse = GenericMethod.Get("Parse", "Parse");
            Format = GenericMethod.Get("Format", "Format");

            AddStringMethods(Unescape, Escape);

            AddStringMethods(bool.Parse);
            AddStringMethods(byte.Parse);
            AddStringMethods(sbyte.Parse);
            AddStringMethods(s => s[0]);
            AddStringMethods(short.Parse);
            AddStringMethods(ushort.Parse);
            AddStringMethods(int.Parse);
            AddStringMethods(uint.Parse);
            AddStringMethods(long.Parse);
            AddStringMethods(ulong.Parse);
            AddStringMethods(decimal.Parse);
            AddStringMethods(float.Parse);
            AddStringMethods(double.Parse);
        }

        /// <summary>
        /// Adds parser and formatter for type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static public void AddStringMethods<T>(Func<string, T> parser, Func<T, string> formatter)
        {
            Parse.AddParser(parser);
            Format.Add(formatter);
        }

        /// <summary>
        /// <para>Adds parser and formatter for type T.</para>
        /// <para>Formatter is default (obj.ToString)</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parser"></param>
        static public void AddStringMethods<T>(Func<string, T> parser)
        {
            AddStringMethods(parser, ToString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public string ToString<T>(T obj)
        {
            return obj.ToString();
        }

        /// <summary>
        /// Converts the string into the format that can be saved to file.
        /// </summary>
        /// <param name="str">The original string</param>
        /// <returns>The escaped string</returns>
        static public string Escape(string str)
        {
            return Regex.Escape(str);
        }

        /// <summary>
        /// Retrieves the string from the escaped format.
        /// </summary>
        /// <param name="str">The escaped string</param>
        /// <returns>The original string</returns>
        static public string Unescape(string str)
        {
            return Regex.Unescape(str);
        }
    }
}
