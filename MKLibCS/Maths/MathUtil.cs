using System;

using MKLibCS.Generic;

namespace MKLibCS.Maths
{
    /// <summary>
    /// Utilities and extensions for maths
    /// </summary>
    static public class MathUtil
    {
        #region Constants

        /// <summary>
        /// Square root of 2
        /// </summary>
        public const double Sqrt2 = 1.414213562;
        /// <summary>
        /// Sqrt2 divided by 2
        /// </summary>
        public const double Sqrt2_2 = Sqrt2 / 2;
        /// <summary>
        /// Square root of 3
        /// </summary>
        public const double Sqrt3 = 1.732050808;
        /// <summary>
        /// Sqrt3 divided by 2
        /// </summary>
        public const double Sqrt3_2 = Sqrt3 / 2;
        /// <summary>
        /// 
        /// </summary>
        public const double PI = 3.141592654;
        /// <summary>
        /// PI divided by 2
        /// </summary>
        public const double PI_2 = PI / 2;
        /// <summary>
        /// PI divided by 3
        /// </summary>
        public const double PI_3 = PI / 3;
        /// <summary>
        /// PI divided by 4
        /// </summary>
        public const double PI_4 = PI / 4;
        /// <summary>
        /// PI divided by 6
        /// </summary>
        public const double PI_6 = PI / 6;
        /// <summary>
        /// How many Deg is 1 Rad
        /// </summary>
        public const double RadToDeg = 180.0 / PI;
        /// <summary>
        /// How many Rad is 1 Deg
        /// </summary>
        public const double DegToRad = 1 / RadToDeg;

        #endregion

        #region Generic

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        static public T Abs<T>(this T value)
        {
            return (T)MathGenerics.Abs.Do(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x"></param>
        /// <returns></returns>
        static public T Sqrt<T>(this T x)
        {
            return (T)MathGenerics.Sqrt.Do(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <returns></returns>
        static public T Max<T>(T val1, T val2)
        {
            try
            {
                return (T)MathGenerics.Max.Do(val1, val2);
            }
            catch(MissingGenericMethodException e)
            {
                if (val1 is IComparable<T>)
                {
                    if ((val1 as IComparable<T>).CompareTo(val2) >= 0)
                        return val1;
                    else
                        return val2;
                }
                else
                    throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        static public T Max<T>(params T[] values)
        {
            T max = values[0];
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
        static public T Min<T>(T val1, T val2)
        {
            try
            {
                return (T)MathGenerics.Min.Do(val1, val2);
            }
            catch (MissingGenericMethodException e)
            {
                if (val1 is IComparable<T>)
                {
                    if ((val1 as IComparable<T>).CompareTo(val2) <= 0)
                        return val1;
                    else
                        return val2;
                }
                else
                    throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        static public T Min<T>(params T[] values)
        {
            T min = values[0];
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
        static public int Sign<T>(this T value)
        {
            try
            {
                return (int)MathGenerics.Sign.Do(value);
            }
            catch(MissingGenericMethodException e)
            {
                if (value is IComparable<T> && MathGenerics.Zero.Contains<T>())
                {
                    var zero = MathGenerics.Zero.GetValue<T>();
                    var compare = (value as IComparable<T>).CompareTo(zero);
                    if (compare > 0)
                        return 1;
                    else if (compare == 0)
                        return 0;
                    else
                        return -1;
                }
                else
                    throw e;
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
        static public T FortranSign<T, U>(T a, U b)
        {
            T abs = a.Abs();
            if (b.Sign() < 0)
                return (T)MathGenerics.Negative.Do(abs);
            else
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
        static public T Confine<T>(T x, T min, T max) where T : IComparable<T>
        {
            if (x.CompareTo(min) < 0)
                return min;
            else if (x.CompareTo(max) > 0)
                return max;
            else
                return x;
        }

        #endregion

        #region Extensions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public double Sin(this double x)
        {
            return Math.Sin(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float Sin(this float x)
        {
            return (float)Math.Sin(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public double Cos(this double x)
        {
            return Math.Cos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float Cos(this float x)
        {
            return (float)Math.Cos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public double ACos(this double x)
        {
            return Math.Acos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        static public float ACos(this float x)
        {
            return (float)Math.Acos(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static public int Factorial(this int n)
        {
            if (n < 0)
                throw new ArgumentException("must be non-negative", "n");
            else if (n <= 1)
                return 1;
            else
                return n * (n - 1).Factorial();
        }

        #endregion
    }
}
