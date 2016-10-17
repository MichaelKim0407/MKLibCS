using System;

using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// 
        /// </summary>
        public static GenericMethod Abs { get; private set; }

        private static void InitAbs()
        {
            Abs = GenericMethod.Get("Abs", "Abs");

            Abs.Add<byte>(a => (byte)Math.Abs(a));
            Abs.Add<sbyte>(a => Math.Abs(a));
            Abs.Add<char>(a => (char)Math.Abs(a));
            Abs.Add<short>(a => Math.Abs(a));
            Abs.Add<ushort>(a => (ushort)Math.Abs(a));
            Abs.Add<int>(a => Math.Abs(a));
            Abs.Add<uint>(a => (uint)Math.Abs(a));
            Abs.Add<long>(a => Math.Abs(a));
            //Abs.Add<ulong>(a => (ulong)Math.Abs(a));
            Abs.Add<decimal>(a => Math.Abs(a));
            Abs.Add<float>(a => Math.Abs(a));
            Abs.Add<double>(a => Math.Abs(a));
        }
    }
}
