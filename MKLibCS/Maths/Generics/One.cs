using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    partial class MathGenerics
    {
        /// <summary>
        /// Generic value 1
        /// </summary>
        static public GenericMethod One { get; set; }

        static private void InitOne()
        {
            One = GenericMethod.Get("One", "One");

            One.AddValue(true);
            One.AddValue<byte>(1);
            One.AddValue<sbyte>(1);
            One.AddValue('\x01');
            One.AddValue<short>(1);
            One.AddValue<ushort>(1);
            One.AddValue(1);
            One.AddValue(1U);
            One.AddValue(1L);
            One.AddValue(1UL);
            One.AddValue(1.0M);
            One.AddValue(1.0F);
            One.AddValue(1.0);
        }
    }
}
