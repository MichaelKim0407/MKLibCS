using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// Generic "/" method
        /// </summary>
        public static GenericMethod Divide { get; private set; }

        private static void InitDivide()
        {
            Divide = GenericMethod.Get("Divide", "Divide", "op_Division");

            Divide.Add<byte, byte>((a, b) => a/b);
            Divide.Add<byte, sbyte>((a, b) => a/b);
            Divide.Add<byte, char>((a, b) => a/b);
            Divide.Add<byte, short>((a, b) => a/b);
            Divide.Add<byte, ushort>((a, b) => a/b);
            Divide.Add<byte, int>((a, b) => a/b);
            Divide.Add<byte, uint>((a, b) => a/b);
            Divide.Add<byte, long>((a, b) => a/b);
            Divide.Add<byte, ulong>((a, b) => a/b);
            Divide.Add<byte, decimal>((a, b) => a/b);
            Divide.Add<byte, float>((a, b) => a/b);
            Divide.Add<byte, double>((a, b) => a/b);

            Divide.Add<sbyte, byte>((a, b) => a/b);
            Divide.Add<sbyte, sbyte>((a, b) => a/b);
            Divide.Add<sbyte, char>((a, b) => a/b);
            Divide.Add<sbyte, short>((a, b) => a/b);
            Divide.Add<sbyte, ushort>((a, b) => a/b);
            Divide.Add<sbyte, int>((a, b) => a/b);
            Divide.Add<sbyte, uint>((a, b) => a/b);
            Divide.Add<sbyte, long>((a, b) => a/b);
            //Divide.Add<sbyte, ulong>((a, b) => a / b);
            Divide.Add<sbyte, decimal>((a, b) => a/b);
            Divide.Add<sbyte, float>((a, b) => a/b);
            Divide.Add<sbyte, double>((a, b) => a/b);

            Divide.Add<char, byte>((a, b) => a/b);
            Divide.Add<char, sbyte>((a, b) => a/b);
            Divide.Add<char, char>((a, b) => a/b);
            Divide.Add<char, short>((a, b) => a/b);
            Divide.Add<char, ushort>((a, b) => a/b);
            Divide.Add<char, int>((a, b) => a/b);
            Divide.Add<char, uint>((a, b) => a/b);
            Divide.Add<char, long>((a, b) => a/b);
            Divide.Add<char, ulong>((a, b) => a/b);
            Divide.Add<char, decimal>((a, b) => a/b);
            Divide.Add<char, float>((a, b) => a/b);
            Divide.Add<char, double>((a, b) => a/b);

            Divide.Add<short, byte>((a, b) => a/b);
            Divide.Add<short, sbyte>((a, b) => a/b);
            Divide.Add<short, char>((a, b) => a/b);
            Divide.Add<short, short>((a, b) => a/b);
            Divide.Add<short, ushort>((a, b) => a/b);
            Divide.Add<short, int>((a, b) => a/b);
            Divide.Add<short, uint>((a, b) => a/b);
            Divide.Add<short, long>((a, b) => a/b);
            //Divide.Add<short, ulong>((a, b) => a / b);
            Divide.Add<short, decimal>((a, b) => a/b);
            Divide.Add<short, float>((a, b) => a/b);
            Divide.Add<short, double>((a, b) => a/b);

            Divide.Add<ushort, byte>((a, b) => a/b);
            Divide.Add<ushort, sbyte>((a, b) => a/b);
            Divide.Add<ushort, char>((a, b) => a/b);
            Divide.Add<ushort, short>((a, b) => a/b);
            Divide.Add<ushort, ushort>((a, b) => a/b);
            Divide.Add<ushort, int>((a, b) => a/b);
            Divide.Add<ushort, uint>((a, b) => a/b);
            Divide.Add<ushort, long>((a, b) => a/b);
            Divide.Add<ushort, ulong>((a, b) => a/b);
            Divide.Add<ushort, decimal>((a, b) => a/b);
            Divide.Add<ushort, float>((a, b) => a/b);
            Divide.Add<ushort, double>((a, b) => a/b);

            Divide.Add<int, byte>((a, b) => a/b);
            Divide.Add<int, sbyte>((a, b) => a/b);
            Divide.Add<int, char>((a, b) => a/b);
            Divide.Add<int, short>((a, b) => a/b);
            Divide.Add<int, ushort>((a, b) => a/b);
            Divide.Add<int, int>((a, b) => a/b);
            Divide.Add<int, uint>((a, b) => a/b);
            Divide.Add<int, long>((a, b) => a/b);
            //Divide.Add<int, ulong>((a, b) => a / b);
            Divide.Add<int, decimal>((a, b) => a/b);
            Divide.Add<int, float>((a, b) => a/b);
            Divide.Add<int, double>((a, b) => a/b);

            Divide.Add<uint, byte>((a, b) => a/b);
            Divide.Add<uint, sbyte>((a, b) => a/b);
            Divide.Add<uint, char>((a, b) => a/b);
            Divide.Add<uint, short>((a, b) => a/b);
            Divide.Add<uint, ushort>((a, b) => a/b);
            Divide.Add<uint, int>((a, b) => a/b);
            Divide.Add<uint, uint>((a, b) => a/b);
            Divide.Add<uint, long>((a, b) => a/b);
            Divide.Add<uint, ulong>((a, b) => a/b);
            Divide.Add<uint, decimal>((a, b) => a/b);
            Divide.Add<uint, float>((a, b) => a/b);
            Divide.Add<uint, double>((a, b) => a/b);

            Divide.Add<long, byte>((a, b) => a/b);
            Divide.Add<long, sbyte>((a, b) => a/b);
            Divide.Add<long, char>((a, b) => a/b);
            Divide.Add<long, short>((a, b) => a/b);
            Divide.Add<long, ushort>((a, b) => a/b);
            Divide.Add<long, int>((a, b) => a/b);
            Divide.Add<long, uint>((a, b) => a/b);
            Divide.Add<long, long>((a, b) => a/b);
            //Divide.Add<long, ulong>((a, b) => a / b);
            Divide.Add<long, decimal>((a, b) => a/b);
            Divide.Add<long, float>((a, b) => a/b);
            Divide.Add<long, double>((a, b) => a/b);

            Divide.Add<ulong, byte>((a, b) => a/b);
            //Divide.Add<ulong, sbyte>((a, b) => a / b);
            Divide.Add<ulong, char>((a, b) => a/b);
            //Divide.Add<ulong, short>((a, b) => a / b);
            Divide.Add<ulong, ushort>((a, b) => a/b);
            //Divide.Add<ulong, int>((a, b) => a / b);
            Divide.Add<ulong, uint>((a, b) => a/b);
            //Divide.Add<ulong, long>((a, b) => a / b);
            Divide.Add<ulong, ulong>((a, b) => a/b);
            Divide.Add<ulong, decimal>((a, b) => a/b);
            Divide.Add<ulong, float>((a, b) => a/b);
            Divide.Add<ulong, double>((a, b) => a/b);

            Divide.Add<decimal, byte>((a, b) => a/b);
            Divide.Add<decimal, sbyte>((a, b) => a/b);
            Divide.Add<decimal, char>((a, b) => a/b);
            Divide.Add<decimal, short>((a, b) => a/b);
            Divide.Add<decimal, ushort>((a, b) => a/b);
            Divide.Add<decimal, int>((a, b) => a/b);
            Divide.Add<decimal, uint>((a, b) => a/b);
            Divide.Add<decimal, long>((a, b) => a/b);
            Divide.Add<decimal, ulong>((a, b) => a/b);
            Divide.Add<decimal, decimal>((a, b) => a/b);
            //Divide.Add<decimal, float>((a, b) => a / b);
            //Divide.Add<decimal, double>((a, b) => a / b);

            Divide.Add<float, byte>((a, b) => a/b);
            Divide.Add<float, sbyte>((a, b) => a/b);
            Divide.Add<float, char>((a, b) => a/b);
            Divide.Add<float, short>((a, b) => a/b);
            Divide.Add<float, ushort>((a, b) => a/b);
            Divide.Add<float, int>((a, b) => a/b);
            Divide.Add<float, uint>((a, b) => a/b);
            Divide.Add<float, long>((a, b) => a/b);
            Divide.Add<float, ulong>((a, b) => a/b);
            //Divide.Add<float, decimal>((a, b) => a / b);
            Divide.Add<float, float>((a, b) => a/b);
            Divide.Add<float, double>((a, b) => a/b);

            Divide.Add<double, byte>((a, b) => a/b);
            Divide.Add<double, sbyte>((a, b) => a/b);
            Divide.Add<double, char>((a, b) => a/b);
            Divide.Add<double, short>((a, b) => a/b);
            Divide.Add<double, ushort>((a, b) => a/b);
            Divide.Add<double, int>((a, b) => a/b);
            Divide.Add<double, uint>((a, b) => a/b);
            Divide.Add<double, long>((a, b) => a/b);
            Divide.Add<double, ulong>((a, b) => a/b);
            //Divide.Add<double, decimal>((a, b) => a / b);
            Divide.Add<double, float>((a, b) => a/b);
            Divide.Add<double, double>((a, b) => a/b);
        }
    }
}