using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// Generic "-" (negative) method
        /// </summary>
        public static GenericMethod Negative { get; private set; }

        private static void InitNegative()
        {
            Negative = GenericMethod.Get("Negative", "Negative", "op_UnaryNegation");

            Negative.Add<bool>(a => !a);
            Negative.Add<byte>(a => (byte) -a);
            Negative.Add<sbyte>(a => (sbyte) -a);
            Negative.Add<char>(a => (char) -a);
            Negative.Add<short>(a => (short) -a);
            Negative.Add<ushort>(a => (ushort) -a);
            Negative.Add<int>(a => -a);
            Negative.Add<uint>(a => (uint) -a);
            Negative.Add<long>(a => -a);
            //Negative.AddMethod<ulong>(a => (ulong)-a);
            Negative.Add<decimal>(a => -a);
            Negative.Add<float>(a => -a);
            Negative.Add<double>(a => -a);
        }
    }
}