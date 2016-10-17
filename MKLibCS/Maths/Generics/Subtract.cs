using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// Generic "-" (subtract) method
        /// </summary>
        public static GenericMethod Subtract { get; private set; }

        private static void InitSubtract()
        {
            Subtract = GenericMethod.Get("Subtract", "Subtract", "op_Subtraction");

            Subtract.Add<bool, bool>((a, b) => a ^ b);

            Subtract.Add<byte, byte>((a, b) => a - b);
            Subtract.Add<byte, sbyte>((a, b) => a - b);
            Subtract.Add<byte, char>((a, b) => a - b);
            Subtract.Add<byte, short>((a, b) => a - b);
            Subtract.Add<byte, ushort>((a, b) => a - b);
            Subtract.Add<byte, int>((a, b) => a - b);
            Subtract.Add<byte, uint>((a, b) => a - b);
            Subtract.Add<byte, long>((a, b) => a - b);
            Subtract.Add<byte, ulong>((a, b) => a - b);
            Subtract.Add<byte, decimal>((a, b) => a - b);
            Subtract.Add<byte, float>((a, b) => a - b);
            Subtract.Add<byte, double>((a, b) => a - b);

            Subtract.Add<sbyte, byte>((a, b) => a - b);
            Subtract.Add<sbyte, sbyte>((a, b) => a - b);
            Subtract.Add<sbyte, char>((a, b) => a - b);
            Subtract.Add<sbyte, short>((a, b) => a - b);
            Subtract.Add<sbyte, ushort>((a, b) => a - b);
            Subtract.Add<sbyte, int>((a, b) => a - b);
            Subtract.Add<sbyte, uint>((a, b) => a - b);
            Subtract.Add<sbyte, long>((a, b) => a - b);
            //Subtract.Add<sbyte, ulong>((a, b) => a - b);
            Subtract.Add<sbyte, decimal>((a, b) => a - b);
            Subtract.Add<sbyte, float>((a, b) => a - b);
            Subtract.Add<sbyte, double>((a, b) => a - b);

            Subtract.Add<char, byte>((a, b) => a - b);
            Subtract.Add<char, sbyte>((a, b) => a - b);
            Subtract.Add<char, char>((a, b) => a - b);
            Subtract.Add<char, short>((a, b) => a - b);
            Subtract.Add<char, ushort>((a, b) => a - b);
            Subtract.Add<char, int>((a, b) => a - b);
            Subtract.Add<char, uint>((a, b) => a - b);
            Subtract.Add<char, long>((a, b) => a - b);
            Subtract.Add<char, ulong>((a, b) => a - b);
            Subtract.Add<char, decimal>((a, b) => a - b);
            Subtract.Add<char, float>((a, b) => a - b);
            Subtract.Add<char, double>((a, b) => a - b);

            Subtract.Add<short, byte>((a, b) => a - b);
            Subtract.Add<short, sbyte>((a, b) => a - b);
            Subtract.Add<short, char>((a, b) => a - b);
            Subtract.Add<short, short>((a, b) => a - b);
            Subtract.Add<short, ushort>((a, b) => a - b);
            Subtract.Add<short, int>((a, b) => a - b);
            Subtract.Add<short, uint>((a, b) => a - b);
            Subtract.Add<short, long>((a, b) => a - b);
            //Subtract.Add<short, ulong>((a, b) => a - b);
            Subtract.Add<short, decimal>((a, b) => a - b);
            Subtract.Add<short, float>((a, b) => a - b);
            Subtract.Add<short, double>((a, b) => a - b);

            Subtract.Add<ushort, byte>((a, b) => a - b);
            Subtract.Add<ushort, sbyte>((a, b) => a - b);
            Subtract.Add<ushort, char>((a, b) => a - b);
            Subtract.Add<ushort, short>((a, b) => a - b);
            Subtract.Add<ushort, ushort>((a, b) => a - b);
            Subtract.Add<ushort, int>((a, b) => a - b);
            Subtract.Add<ushort, uint>((a, b) => a - b);
            Subtract.Add<ushort, long>((a, b) => a - b);
            Subtract.Add<ushort, ulong>((a, b) => a - b);
            Subtract.Add<ushort, decimal>((a, b) => a - b);
            Subtract.Add<ushort, float>((a, b) => a - b);
            Subtract.Add<ushort, double>((a, b) => a - b);

            Subtract.Add<int, byte>((a, b) => a - b);
            Subtract.Add<int, sbyte>((a, b) => a - b);
            Subtract.Add<int, char>((a, b) => a - b);
            Subtract.Add<int, short>((a, b) => a - b);
            Subtract.Add<int, ushort>((a, b) => a - b);
            Subtract.Add<int, int>((a, b) => a - b);
            Subtract.Add<int, uint>((a, b) => a - b);
            Subtract.Add<int, long>((a, b) => a - b);
            //Subtract.Add<int, ulong>((a, b) => a - b);
            Subtract.Add<int, decimal>((a, b) => a - b);
            Subtract.Add<int, float>((a, b) => a - b);
            Subtract.Add<int, double>((a, b) => a - b);

            Subtract.Add<uint, byte>((a, b) => a - b);
            Subtract.Add<uint, sbyte>((a, b) => a - b);
            Subtract.Add<uint, char>((a, b) => a - b);
            Subtract.Add<uint, short>((a, b) => a - b);
            Subtract.Add<uint, ushort>((a, b) => a - b);
            Subtract.Add<uint, int>((a, b) => a - b);
            Subtract.Add<uint, uint>((a, b) => a - b);
            Subtract.Add<uint, long>((a, b) => a - b);
            Subtract.Add<uint, ulong>((a, b) => a - b);
            Subtract.Add<uint, decimal>((a, b) => a - b);
            Subtract.Add<uint, float>((a, b) => a - b);
            Subtract.Add<uint, double>((a, b) => a - b);

            Subtract.Add<long, byte>((a, b) => a - b);
            Subtract.Add<long, sbyte>((a, b) => a - b);
            Subtract.Add<long, char>((a, b) => a - b);
            Subtract.Add<long, short>((a, b) => a - b);
            Subtract.Add<long, ushort>((a, b) => a - b);
            Subtract.Add<long, int>((a, b) => a - b);
            Subtract.Add<long, uint>((a, b) => a - b);
            Subtract.Add<long, long>((a, b) => a - b);
            //Subtract.Add<long, ulong>((a, b) => a - b);
            Subtract.Add<long, decimal>((a, b) => a - b);
            Subtract.Add<long, float>((a, b) => a - b);
            Subtract.Add<long, double>((a, b) => a - b);

            Subtract.Add<ulong, byte>((a, b) => a - b);
            //Subtract.Add<ulong, sbyte>((a, b) => a - b);
            Subtract.Add<ulong, char>((a, b) => a - b);
            //Subtract.Add<ulong, short>((a, b) => a - b);
            Subtract.Add<ulong, ushort>((a, b) => a - b);
            //Subtract.Add<ulong, int>((a, b) => a - b);
            Subtract.Add<ulong, uint>((a, b) => a - b);
            //Subtract.Add<ulong, long>((a, b) => a - b);
            Subtract.Add<ulong, ulong>((a, b) => a - b);
            Subtract.Add<ulong, decimal>((a, b) => a - b);
            Subtract.Add<ulong, float>((a, b) => a - b);
            Subtract.Add<ulong, double>((a, b) => a - b);

            Subtract.Add<decimal, byte>((a, b) => a - b);
            Subtract.Add<decimal, sbyte>((a, b) => a - b);
            Subtract.Add<decimal, char>((a, b) => a - b);
            Subtract.Add<decimal, short>((a, b) => a - b);
            Subtract.Add<decimal, ushort>((a, b) => a - b);
            Subtract.Add<decimal, int>((a, b) => a - b);
            Subtract.Add<decimal, uint>((a, b) => a - b);
            Subtract.Add<decimal, long>((a, b) => a - b);
            Subtract.Add<decimal, ulong>((a, b) => a - b);
            Subtract.Add<decimal, decimal>((a, b) => a - b);
            //Subtract.Add<decimal, float>((a, b) => a - b);
            //Subtract.Add<decimal, double>((a, b) => a - b);

            Subtract.Add<float, byte>((a, b) => a - b);
            Subtract.Add<float, sbyte>((a, b) => a - b);
            Subtract.Add<float, char>((a, b) => a - b);
            Subtract.Add<float, short>((a, b) => a - b);
            Subtract.Add<float, ushort>((a, b) => a - b);
            Subtract.Add<float, int>((a, b) => a - b);
            Subtract.Add<float, uint>((a, b) => a - b);
            Subtract.Add<float, long>((a, b) => a - b);
            Subtract.Add<float, ulong>((a, b) => a - b);
            //Subtract.Add<float, decimal>((a, b) => a - b);
            Subtract.Add<float, float>((a, b) => a - b);
            Subtract.Add<float, double>((a, b) => a - b);

            Subtract.Add<double, byte>((a, b) => a - b);
            Subtract.Add<double, sbyte>((a, b) => a - b);
            Subtract.Add<double, char>((a, b) => a - b);
            Subtract.Add<double, short>((a, b) => a - b);
            Subtract.Add<double, ushort>((a, b) => a - b);
            Subtract.Add<double, int>((a, b) => a - b);
            Subtract.Add<double, uint>((a, b) => a - b);
            Subtract.Add<double, long>((a, b) => a - b);
            Subtract.Add<double, ulong>((a, b) => a - b);
            //Subtract.Add<double, decimal>((a, b) => a - b);
            Subtract.Add<double, float>((a, b) => a - b);
            Subtract.Add<double, double>((a, b) => a - b);
        }
    }
}