using System;

using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// 
        /// </summary>
        public static GenericMethod Sign { get; private set; }

        private static void InitSign()
        {
            Sign = GenericMethod.Get("Sign", "Sign");

            Sign.Add<byte>(a => Math.Sign(a));
            Sign.Add<sbyte>(a => Math.Sign(a));
            Sign.Add<char>(a => Math.Sign(a));
            Sign.Add<short>(a => Math.Sign(a));
            Sign.Add<ushort>(a => Math.Sign(a));
            Sign.Add<int>(a => Math.Sign(a));
            Sign.Add<uint>(a => Math.Sign(a));
            Sign.Add<long>(a => Math.Sign(a));
            //Sign.Add<ulong>(a => Math.Sign(a));
            Sign.Add<decimal>(a => Math.Sign(a));
            Sign.Add<float>(a => Math.Sign(a));
            Sign.Add<double>(a => Math.Sign(a));
        }
    }
}
