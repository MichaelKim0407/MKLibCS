﻿using System;
using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    /// <summary>
    /// Utilities and extensions for maths
    /// </summary>
    public static class MathUtil
    {
        #region Constants

        /// <summary>
        /// Square root of 2
        /// </summary>
        public const double Sqrt2 = 1.414213562;

        /// <summary>
        /// Sqrt2 divided by 2
        /// </summary>
        public const double Sqrt2_2 = Sqrt2/2;

        /// <summary>
        /// Square root of 3
        /// </summary>
        public const double Sqrt3 = 1.732050808;

        /// <summary>
        /// Sqrt3 divided by 2
        /// </summary>
        public const double Sqrt3_2 = Sqrt3/2;

        /// <summary>
        /// 
        /// </summary>
        public const double PI = 3.141592654;

        /// <summary>
        /// PI divided by 2
        /// </summary>
        public const double PI_2 = PI/2;

        /// <summary>
        /// PI divided by 3
        /// </summary>
        public const double PI_3 = PI/3;

        /// <summary>
        /// PI divided by 4
        /// </summary>
        public const double PI_4 = PI/4;

        /// <summary>
        /// PI divided by 6
        /// </summary>
        public const double PI_6 = PI/6;

        /// <summary>
        /// How many Deg is 1 Rad
        /// </summary>
        public const double RadToDeg = 180.0/PI;

        /// <summary>
        /// How many Rad is 1 Deg
        /// </summary>
        public const double DegToRad = 1/RadToDeg;

        #endregion

        #region Generic

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Abs<T>(this T value)
        {
            return (T) MathGenerics.Abs.Do(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        public static T Sqrt<T>(this T x)
        {
            return (T) MathGenerics.Sqrt.Do(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static T Max<T>(T val1, T val2)
        {
            try
            {
                return (T) MathGenerics.Max.Do(val1, val2);
            }
            catch (MissingGenericMethodException)
            {
                if (!(val1 is IComparable<T>))
                    throw;
                return (val1 as IComparable<T>).CompareTo(val2) >= 0 ? val1 : val2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T Max<T>(params T[] values)
        {
            var max = values[0];
            foreach (var val in values)
                max = Max(max, val);
            return max;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        public static T Min<T>(T val1, T val2)
        {
            try
            {
                return (T) MathGenerics.Min.Do(val1, val2);
            }
            catch (MissingGenericMethodException)
            {
                if (!(val1 is IComparable<T>))
                    throw;
                return (val1 as IComparable<T>).CompareTo(val2) <= 0 ? val1 : val2;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T Min<T>(params T[] values)
        {
            var min = values[0];
            foreach (var val in values)
                min = Min(min, val);
            return min;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Sign<T>(this T value)
        {
            try
            {
                return (int) MathGenerics.Sign.Do(value);
            }
            catch (MissingGenericMethodException)
            {
                if (!(value is IComparable<T>) || !MathGenerics.Zero.Contains<T>())
                    throw;
                var zero = MathGenerics.Zero.GetValue<T>();
                var compare = (value as IComparable<T>).CompareTo(zero);
                if (compare > 0)
                    return 1;
                if (compare == 0)
                    return 0;
                return -1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static T FortranSign<T, U>(T a, U b)
        {
            var abs = a.Abs();
            if (b.Sign() < 0)
                return (T) MathGenerics.Negative.Do(abs);
            return abs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static T Confine<T>(T x, T min, T max) where T : IComparable<T>
        {
            if (x.CompareTo(min) < 0)
                return min;
            if (x.CompareTo(max) > 0)
                return max;
            return x;
        }

        #endregion

        #region Extensions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Sin(this double x)
        {
            return Math.Sin(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float Sin(this float x)
        {
            return (float) Math.Sin(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Cos(this double x)
        {
            return Math.Cos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float Cos(this float x)
        {
            return (float) Math.Cos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double ACos(this double x)
        {
            return Math.Acos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float ACos(this float x)
        {
            return (float) Math.Acos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int Factorial(this int n)
        {
            if (n < 0)
                throw new ArgumentException("must be non-negative", nameof(n));
            if (n <= 1)
                return 1;
            return n*(n - 1).Factorial();
        }

        #endregion
    }
}