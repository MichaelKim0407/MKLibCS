using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// Generic "*" method
        /// </summary>
        public static GenericMethod Multiply { get; private set; }

        private static void InitMultiply()
        {
            Multiply = GenericMethod.Get("Multiply", "Multiply", "op_Multiply");

            Multiply.Add<bool, bool>((a, b) => a && b);

            Multiply.Add<byte, byte>((a, b) => a * b);
            Multiply.Add<byte, sbyte>((a, b) => a * b);
            Multiply.Add<byte, char>((a, b) => a * b);
            Multiply.Add<byte, short>((a, b) => a * b);
            Multiply.Add<byte, ushort>((a, b) => a * b);
            Multiply.Add<byte, int>((a, b) => a * b);
            Multiply.Add<byte, uint>((a, b) => a * b);
            Multiply.Add<byte, long>((a, b) => a * b);
            Multiply.Add<byte, ulong>((a, b) => a * b);
            Multiply.Add<byte, decimal>((a, b) => a * b);
            Multiply.Add<byte, float>((a, b) => a * b);
            Multiply.Add<byte, double>((a, b) => a * b);

            Multiply.Add<sbyte, byte>((a, b) => a * b);
            Multiply.Add<sbyte, sbyte>((a, b) => a * b);
            Multiply.Add<sbyte, char>((a, b) => a * b);
            Multiply.Add<sbyte, short>((a, b) => a * b);
            Multiply.Add<sbyte, ushort>((a, b) => a * b);
            Multiply.Add<sbyte, int>((a, b) => a * b);
            Multiply.Add<sbyte, uint>((a, b) => a * b);
            Multiply.Add<sbyte, long>((a, b) => a * b);
            //Multiply.Add<sbyte, ulong>((a, b) => a * b);
            Multiply.Add<sbyte, decimal>((a, b) => a * b);
            Multiply.Add<sbyte, float>((a, b) => a * b);
            Multiply.Add<sbyte, double>((a, b) => a * b);

            Multiply.Add<char, byte>((a, b) => a * b);
            Multiply.Add<char, sbyte>((a, b) => a * b);
            Multiply.Add<char, char>((a, b) => a * b);
            Multiply.Add<char, short>((a, b) => a * b);
            Multiply.Add<char, ushort>((a, b) => a * b);
            Multiply.Add<char, int>((a, b) => a * b);
            Multiply.Add<char, uint>((a, b) => a * b);
            Multiply.Add<char, long>((a, b) => a * b);
            Multiply.Add<char, ulong>((a, b) => a * b);
            Multiply.Add<char, decimal>((a, b) => a * b);
            Multiply.Add<char, float>((a, b) => a * b);
            Multiply.Add<char, double>((a, b) => a * b);

            Multiply.Add<short, byte>((a, b) => a * b);
            Multiply.Add<short, sbyte>((a, b) => a * b);
            Multiply.Add<short, char>((a, b) => a * b);
            Multiply.Add<short, short>((a, b) => a * b);
            Multiply.Add<short, ushort>((a, b) => a * b);
            Multiply.Add<short, int>((a, b) => a * b);
            Multiply.Add<short, uint>((a, b) => a * b);
            Multiply.Add<short, long>((a, b) => a * b);
            //Multiply.Add<short, ulong>((a, b) => a * b);
            Multiply.Add<short, decimal>((a, b) => a * b);
            Multiply.Add<short, float>((a, b) => a * b);
            Multiply.Add<short, double>((a, b) => a * b);

            Multiply.Add<ushort, byte>((a, b) => a * b);
            Multiply.Add<ushort, sbyte>((a, b) => a * b);
            Multiply.Add<ushort, char>((a, b) => a * b);
            Multiply.Add<ushort, short>((a, b) => a * b);
            Multiply.Add<ushort, ushort>((a, b) => a * b);
            Multiply.Add<ushort, int>((a, b) => a * b);
            Multiply.Add<ushort, uint>((a, b) => a * b);
            Multiply.Add<ushort, long>((a, b) => a * b);
            Multiply.Add<ushort, ulong>((a, b) => a * b);
            Multiply.Add<ushort, decimal>((a, b) => a * b);
            Multiply.Add<ushort, float>((a, b) => a * b);
            Multiply.Add<ushort, double>((a, b) => a * b);

            Multiply.Add<int, byte>((a, b) => a * b);
            Multiply.Add<int, sbyte>((a, b) => a * b);
            Multiply.Add<int, char>((a, b) => a * b);
            Multiply.Add<int, short>((a, b) => a * b);
            Multiply.Add<int, ushort>((a, b) => a * b);
            Multiply.Add<int, int>((a, b) => a * b);
            Multiply.Add<int, uint>((a, b) => a * b);
            Multiply.Add<int, long>((a, b) => a * b);
            //Multiply.Add<int, ulong>((a, b) => a * b);
            Multiply.Add<int, decimal>((a, b) => a * b);
            Multiply.Add<int, float>((a, b) => a * b);
            Multiply.Add<int, double>((a, b) => a * b);

            Multiply.Add<uint, byte>((a, b) => a * b);
            Multiply.Add<uint, sbyte>((a, b) => a * b);
            Multiply.Add<uint, char>((a, b) => a * b);
            Multiply.Add<uint, short>((a, b) => a * b);
            Multiply.Add<uint, ushort>((a, b) => a * b);
            Multiply.Add<uint, int>((a, b) => a * b);
            Multiply.Add<uint, uint>((a, b) => a * b);
            Multiply.Add<uint, long>((a, b) => a * b);
            Multiply.Add<uint, ulong>((a, b) => a * b);
            Multiply.Add<uint, decimal>((a, b) => a * b);
            Multiply.Add<uint, float>((a, b) => a * b);
            Multiply.Add<uint, double>((a, b) => a * b);

            Multiply.Add<long, byte>((a, b) => a * b);
            Multiply.Add<long, sbyte>((a, b) => a * b);
            Multiply.Add<long, char>((a, b) => a * b);
            Multiply.Add<long, short>((a, b) => a * b);
            Multiply.Add<long, ushort>((a, b) => a * b);
            Multiply.Add<long, int>((a, b) => a * b);
            Multiply.Add<long, uint>((a, b) => a * b);
            Multiply.Add<long, long>((a, b) => a * b);
            //Multiply.Add<long, ulong>((a, b) => a * b);
            Multiply.Add<long, decimal>((a, b) => a * b);
            Multiply.Add<long, float>((a, b) => a * b);
            Multiply.Add<long, double>((a, b) => a * b);

            Multiply.Add<ulong, byte>((a, b) => a * b);
            //Multiply.Add<ulong, sbyte>((a, b) => a * b);
            Multiply.Add<ulong, char>((a, b) => a * b);
            //Multiply.Add<ulong, short>((a, b) => a * b);
            Multiply.Add<ulong, ushort>((a, b) => a * b);
            //Multiply.Add<ulong, int>((a, b) => a * b);
            Multiply.Add<ulong, uint>((a, b) => a * b);
            //Multiply.Add<ulong, long>((a, b) => a * b);
            Multiply.Add<ulong, ulong>((a, b) => a * b);
            Multiply.Add<ulong, decimal>((a, b) => a * b);
            Multiply.Add<ulong, float>((a, b) => a * b);
            Multiply.Add<ulong, double>((a, b) => a * b);

            Multiply.Add<decimal, byte>((a, b) => a * b);
            Multiply.Add<decimal, sbyte>((a, b) => a * b);
            Multiply.Add<decimal, char>((a, b) => a * b);
            Multiply.Add<decimal, short>((a, b) => a * b);
            Multiply.Add<decimal, ushort>((a, b) => a * b);
            Multiply.Add<decimal, int>((a, b) => a * b);
            Multiply.Add<decimal, uint>((a, b) => a * b);
            Multiply.Add<decimal, long>((a, b) => a * b);
            Multiply.Add<decimal, ulong>((a, b) => a * b);
            Multiply.Add<decimal, decimal>((a, b) => a * b);
            //Multiply.Add<decimal, float>((a, b) => a * b);
            //Multiply.Add<decimal, double>((a, b) => a * b);

            Multiply.Add<float, byte>((a, b) => a * b);
            Multiply.Add<float, sbyte>((a, b) => a * b);
            Multiply.Add<float, char>((a, b) => a * b);
            Multiply.Add<float, short>((a, b) => a * b);
            Multiply.Add<float, ushort>((a, b) => a * b);
            Multiply.Add<float, int>((a, b) => a * b);
            Multiply.Add<float, uint>((a, b) => a * b);
            Multiply.Add<float, long>((a, b) => a * b);
            Multiply.Add<float, ulong>((a, b) => a * b);
            //Multiply.Add<float, decimal>((a, b) => a * b);
            Multiply.Add<float, float>((a, b) => a * b);
            Multiply.Add<float, double>((a, b) => a * b);

            Multiply.Add<double, byte>((a, b) => a * b);
            Multiply.Add<double, sbyte>((a, b) => a * b);
            Multiply.Add<double, char>((a, b) => a * b);
            Multiply.Add<double, short>((a, b) => a * b);
            Multiply.Add<double, ushort>((a, b) => a * b);
            Multiply.Add<double, int>((a, b) => a * b);
            Multiply.Add<double, uint>((a, b) => a * b);
            Multiply.Add<double, long>((a, b) => a * b);
            Multiply.Add<double, ulong>((a, b) => a * b);
            //Multiply.Add<double, decimal>((a, b) => a * b);
            Multiply.Add<double, float>((a, b) => a * b);
            Multiply.Add<double, double>((a, b) => a * b);
        }
    }
}
