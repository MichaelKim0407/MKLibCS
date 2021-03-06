﻿using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    /// <summary>
    /// Provides generic methods for maths
    /// </summary>
    [GenericUsage]
    public static partial class MathGenerics
    {
        static MathGenerics()
        {
            InitZero();
            InitOne();

            InitNegative();

            InitAdd();
            InitSubtract();
            InitMultiply();
            InitDivide();

            InitAbs();
            InitMax();
            InitMin();
            InitSign();
            InitSqrt();
        }
    }
}