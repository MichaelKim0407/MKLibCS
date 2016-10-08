using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// Generic value 0
        /// </summary>
        static public GenericMethod Zero { get; private set; }

        static private void InitZero()
        {
            Zero = GenericMethod.Get("Zero", "Zero");

            Zero.AddValue(false);
            Zero.AddValue<byte>(0);
            Zero.AddValue<sbyte>(0);
            Zero.AddValue('\0');
            Zero.AddValue<short>(0);
            Zero.AddValue<ushort>(0);
            Zero.AddValue(0);
            Zero.AddValue(0U);
            Zero.AddValue(0L);
            Zero.AddValue(0UL);
            Zero.AddValue(0.0M);
            Zero.AddValue(0.0F);
            Zero.AddValue(0.0);
        }
    }
}
