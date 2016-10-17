using System;
using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// 
        /// </summary>
        public static GenericMethod Min { get; private set; }

        private static void InitMin()
        {
            Min = GenericMethod.Get("Min", "Min");

            Min.Add<byte, byte>((a, b) => Math.Min(a, b));
            Min.Add<sbyte, sbyte>((a, b) => Math.Min(a, b));
            Min.Add<char, char>((a, b) => (char) Math.Min(a, b));
            Min.Add<short, short>((a, b) => Math.Min(a, b));
            Min.Add<ushort, ushort>((a, b) => Math.Min(a, b));
            Min.Add<int, int>((a, b) => Math.Min(a, b));
            Min.Add<uint, uint>((a, b) => Math.Min(a, b));
            Min.Add<long, long>((a, b) => Math.Min(a, b));
            Min.Add<ulong, ulong>((a, b) => Math.Min(a, b));
            Min.Add<decimal, decimal>((a, b) => Math.Min(a, b));
            Min.Add<float, float>((a, b) => Math.Min(a, b));
            Min.Add<double, double>((a, b) => Math.Min(a, b));
        }
    }
}