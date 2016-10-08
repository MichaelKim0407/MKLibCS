using System;

using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// 
        /// </summary>
        static public GenericMethod Max { get; private set; }

        static private void InitMax()
        {
            Max = GenericMethod.Get("Max", "Max");

            Max.Add<byte, byte>((a, b) => Math.Max(a, b));
            Max.Add<sbyte, sbyte>((a, b) => Math.Max(a, b));
            Max.Add<char, char>((a, b) => (char)Math.Max(a, b));
            Max.Add<short, short>((a, b) => Math.Max(a, b));
            Max.Add<ushort, ushort>((a, b) => Math.Max(a, b));
            Max.Add<int, int>((a, b) => Math.Max(a, b));
            Max.Add<uint, uint>((a, b) => Math.Max(a, b));
            Max.Add<long, long>((a, b) => Math.Max(a, b));
            Max.Add<ulong, ulong>((a, b) => Math.Max(a, b));
            Max.Add<decimal, decimal>((a, b) => Math.Max(a, b));
            Max.Add<float, float>((a, b) => Math.Max(a, b));
            Max.Add<double, double>((a, b) => Math.Max(a, b));
        }
    }
}
