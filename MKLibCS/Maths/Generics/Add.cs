using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// Generic "+" method
        /// </summary>
        public static GenericMethod Add { get; private set; }

        private static void InitAdd()
        {
            Add = GenericMethod.Get("Add", "Add", "op_Addition");

            Add.Add<bool, bool>((a, b) => a || b);

            Add.Add<byte, byte>((a, b) => a + b);
            Add.Add<byte, sbyte>((a, b) => a + b);
            Add.Add<byte, char>((a, b) => a + b);
            Add.Add<byte, short>((a, b) => a + b);
            Add.Add<byte, ushort>((a, b) => a + b);
            Add.Add<byte, int>((a, b) => a + b);
            Add.Add<byte, uint>((a, b) => a + b);
            Add.Add<byte, long>((a, b) => a + b);
            Add.Add<byte, ulong>((a, b) => a + b);
            Add.Add<byte, decimal>((a, b) => a + b);
            Add.Add<byte, float>((a, b) => a + b);
            Add.Add<byte, double>((a, b) => a + b);

            Add.Add<sbyte, byte>((a, b) => a + b);
            Add.Add<sbyte, sbyte>((a, b) => a + b);
            Add.Add<sbyte, char>((a, b) => a + b);
            Add.Add<sbyte, short>((a, b) => a + b);
            Add.Add<sbyte, ushort>((a, b) => a + b);
            Add.Add<sbyte, int>((a, b) => a + b);
            Add.Add<sbyte, uint>((a, b) => a + b);
            Add.Add<sbyte, long>((a, b) => a + b);
            //Add.Add<sbyte, ulong>((a, b) => a + b);
            Add.Add<sbyte, decimal>((a, b) => a + b);
            Add.Add<sbyte, float>((a, b) => a + b);
            Add.Add<sbyte, double>((a, b) => a + b);

            Add.Add<char, byte>((a, b) => a + b);
            Add.Add<char, sbyte>((a, b) => a + b);
            Add.Add<char, char>((a, b) => a + b);
            Add.Add<char, short>((a, b) => a + b);
            Add.Add<char, ushort>((a, b) => a + b);
            Add.Add<char, int>((a, b) => a + b);
            Add.Add<char, uint>((a, b) => a + b);
            Add.Add<char, long>((a, b) => a + b);
            Add.Add<char, ulong>((a, b) => a + b);
            Add.Add<char, decimal>((a, b) => a + b);
            Add.Add<char, float>((a, b) => a + b);
            Add.Add<char, double>((a, b) => a + b);

            Add.Add<short, byte>((a, b) => a + b);
            Add.Add<short, sbyte>((a, b) => a + b);
            Add.Add<short, char>((a, b) => a + b);
            Add.Add<short, short>((a, b) => a + b);
            Add.Add<short, ushort>((a, b) => a + b);
            Add.Add<short, int>((a, b) => a + b);
            Add.Add<short, uint>((a, b) => a + b);
            Add.Add<short, long>((a, b) => a + b);
            //Add.Add<short, ulong>((a, b) => a + b);
            Add.Add<short, decimal>((a, b) => a + b);
            Add.Add<short, float>((a, b) => a + b);
            Add.Add<short, double>((a, b) => a + b);

            Add.Add<ushort, byte>((a, b) => a + b);
            Add.Add<ushort, sbyte>((a, b) => a + b);
            Add.Add<ushort, char>((a, b) => a + b);
            Add.Add<ushort, short>((a, b) => a + b);
            Add.Add<ushort, ushort>((a, b) => a + b);
            Add.Add<ushort, int>((a, b) => a + b);
            Add.Add<ushort, uint>((a, b) => a + b);
            Add.Add<ushort, long>((a, b) => a + b);
            Add.Add<ushort, ulong>((a, b) => a + b);
            Add.Add<ushort, decimal>((a, b) => a + b);
            Add.Add<ushort, float>((a, b) => a + b);
            Add.Add<ushort, double>((a, b) => a + b);

            Add.Add<int, byte>((a, b) => a + b);
            Add.Add<int, sbyte>((a, b) => a + b);
            Add.Add<int, char>((a, b) => a + b);
            Add.Add<int, short>((a, b) => a + b);
            Add.Add<int, ushort>((a, b) => a + b);
            Add.Add<int, int>((a, b) => a + b);
            Add.Add<int, uint>((a, b) => a + b);
            Add.Add<int, long>((a, b) => a + b);
            // Add.Add<int, ulong>((a, b) => a + b);
            Add.Add<int, decimal>((a, b) => a + b);
            Add.Add<int, float>((a, b) => a + b);
            Add.Add<int, double>((a, b) => a + b);

            Add.Add<uint, byte>((a, b) => a + b);
            Add.Add<uint, sbyte>((a, b) => a + b);
            Add.Add<uint, char>((a, b) => a + b);
            Add.Add<uint, short>((a, b) => a + b);
            Add.Add<uint, ushort>((a, b) => a + b);
            Add.Add<uint, int>((a, b) => a + b);
            Add.Add<uint, uint>((a, b) => a + b);
            Add.Add<uint, long>((a, b) => a + b);
            Add.Add<uint, ulong>((a, b) => a + b);
            Add.Add<uint, decimal>((a, b) => a + b);
            Add.Add<uint, float>((a, b) => a + b);
            Add.Add<uint, double>((a, b) => a + b);

            Add.Add<long, byte>((a, b) => a + b);
            Add.Add<long, sbyte>((a, b) => a + b);
            Add.Add<long, char>((a, b) => a + b);
            Add.Add<long, short>((a, b) => a + b);
            Add.Add<long, ushort>((a, b) => a + b);
            Add.Add<long, int>((a, b) => a + b);
            Add.Add<long, uint>((a, b) => a + b);
            Add.Add<long, long>((a, b) => a + b);
            //Add.Add<long, ulong>((a, b) => a + b);
            Add.Add<long, decimal>((a, b) => a + b);
            Add.Add<long, float>((a, b) => a + b);
            Add.Add<long, double>((a, b) => a + b);

            Add.Add<ulong, byte>((a, b) => a + b);
            //Add.Add<ulong, sbyte>((a, b) => a + b);
            Add.Add<ulong, char>((a, b) => a + b);
            //Add.Add<ulong, short>((a, b) => a + b);
            Add.Add<ulong, ushort>((a, b) => a + b);
            //Add.Add<ulong, int>((a, b) => a + b);
            Add.Add<ulong, uint>((a, b) => a + b);
            //Add.Add<ulong, long>((a, b) => a + b);
            Add.Add<ulong, ulong>((a, b) => a + b);
            Add.Add<ulong, decimal>((a, b) => a + b);
            Add.Add<ulong, float>((a, b) => a + b);
            Add.Add<ulong, double>((a, b) => a + b);

            Add.Add<decimal, byte>((a, b) => a + b);
            Add.Add<decimal, sbyte>((a, b) => a + b);
            Add.Add<decimal, char>((a, b) => a + b);
            Add.Add<decimal, short>((a, b) => a + b);
            Add.Add<decimal, ushort>((a, b) => a + b);
            Add.Add<decimal, int>((a, b) => a + b);
            Add.Add<decimal, uint>((a, b) => a + b);
            Add.Add<decimal, long>((a, b) => a + b);
            Add.Add<decimal, ulong>((a, b) => a + b);
            Add.Add<decimal, decimal>((a, b) => a + b);
            //Add.Add<decimal, float>((a, b) => a + b);
            //Add.Add<decimal, double>((a, b) => a + b);

            Add.Add<float, byte>((a, b) => a + b);
            Add.Add<float, sbyte>((a, b) => a + b);
            Add.Add<float, char>((a, b) => a + b);
            Add.Add<float, short>((a, b) => a + b);
            Add.Add<float, ushort>((a, b) => a + b);
            Add.Add<float, int>((a, b) => a + b);
            Add.Add<float, uint>((a, b) => a + b);
            Add.Add<float, long>((a, b) => a + b);
            Add.Add<float, ulong>((a, b) => a + b);
            //Add.Add<float, decimal>((a, b) => a + b);
            Add.Add<float, float>((a, b) => a + b);
            Add.Add<float, double>((a, b) => a + b);

            Add.Add<double, byte>((a, b) => a + b);
            Add.Add<double, sbyte>((a, b) => a + b);
            Add.Add<double, char>((a, b) => a + b);
            Add.Add<double, short>((a, b) => a + b);
            Add.Add<double, ushort>((a, b) => a + b);
            Add.Add<double, int>((a, b) => a + b);
            Add.Add<double, uint>((a, b) => a + b);
            Add.Add<double, long>((a, b) => a + b);
            Add.Add<double, ulong>((a, b) => a + b);
            //Add.Add<double, decimal>((a, b) => a + b);
            Add.Add<double, float>((a, b) => a + b);
            Add.Add<double, double>((a, b) => a + b);
        }
    }
}