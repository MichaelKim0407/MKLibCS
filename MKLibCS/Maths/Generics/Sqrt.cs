using System;
using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// 
        /// </summary>
        public static GenericMethod Sqrt { get; private set; }

        private static void InitSqrt()
        {
            Sqrt = GenericMethod.Get("Sqrt", "Sqrt");

            Sqrt.Add<byte>(a => (byte) Math.Sqrt(a));
            Sqrt.Add<sbyte>(a => (sbyte) Math.Sqrt(a));
            Sqrt.Add<char>(a => (char) Math.Sqrt(a));
            Sqrt.Add<short>(a => (short) Math.Sqrt(a));
            Sqrt.Add<ushort>(a => (ushort) Math.Sqrt(a));
            Sqrt.Add<int>(a => (int) Math.Sqrt(a));
            Sqrt.Add<uint>(a => (uint) Math.Sqrt(a));
            Sqrt.Add<long>(a => (long) Math.Sqrt(a));
            Sqrt.Add<ulong>(a => (ulong) Math.Sqrt(a));
            //Sqrt.Add<decimal>(a => (decimal)Math.Sqrt(a));
            Sqrt.Add<float>(a => (float) Math.Sqrt(a));
            Sqrt.Add<double>(a => Math.Sqrt(a));
        }
    }
}