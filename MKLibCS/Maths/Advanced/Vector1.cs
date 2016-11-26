using System;
using MKLibCS.Serialization;
using MKLibCS.Generic;
#if LEGACY
using MKLibCS.Reflection;

#else

using System.Reflection;
#endif

namespace MKLibCS.Maths.Advanced
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GenericUsage(typeof(MathGenerics))]
    [SerializeObject(SerializeObjectMethod.Single)]
    public struct Vector1<T> : IVector<T, Vector1<T>>
    {
        /// <summary>
        /// </summary>
        [SerializeItem] public T x;

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        public Vector1(T x)
        {
            this.x = x;
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator Vector1<T>(T x)
        {
            return new Vector1<T>(x);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector1<T> other)
        {
            return x.Equals(other.x);
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static bool operator ==(Vector1<T> vec1, Vector1<T> vec2)
        {
            return vec1.Equals(vec2);
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static bool operator !=(Vector1<T> vec1, Vector1<T> vec2)
        {
            return !(vec1 == vec2);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector1<T>)
                return Equals((Vector1<T>) obj);
            else
                return false;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return x.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + x + ")";
        }

        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider provider)
        {
            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(IFormattable)))
                return "(" + (x as IFormattable).ToString(format, provider) + ")";
            else
                return ToString();
        }

        /// <summary>
        /// </summary>
        [GenericMethod(GenericMethodType.Creator)]
        public static Vector1<T> Zero
        {
            get { return MathGenerics.Zero.GetValue<T>(); }
        }

        /// <summary>
        /// </summary>
        [GenericMethod("One", GenericMethodType.Creator)]
        public static Vector1<T> XUnit
        {
            get { return MathGenerics.One.GetValue<T>(); }
        }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector1<T> operator -(Vector1<T> vec)
        {
            return (T) MathGenerics.Negative.Do(vec.x);
        }

        /// <summary>
        /// </summary>
        public Vector1<T> Negative
        {
            get { return -this; }
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector1<T> operator +(Vector1<T> vec1, Vector1<T> vec2)
        {
            return (T) MathGenerics.Add.Do(vec1.x, vec2.x);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector1<T> Add(Vector1<T> other)
        {
            return this + other;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector1<T> operator -(Vector1<T> vec1, Vector1<T> vec2)
        {
            return (T) MathGenerics.Subtract.Do(vec1.x, vec2.x);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector1<T> Subtract(Vector1<T> other)
        {
            return this - other;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector1<T> operator *(Vector1<T> vec1, Vector1<T> vec2)
        {
            return (T) MathGenerics.Multiply.Do(vec1.x, vec2.x);
        }

        /// <summary>
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector1<T> Multiply(T num)
        {
            return this*num;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector1<T> operator /(Vector1<T> vec1, Vector1<T> vec2)
        {
            return (T) MathGenerics.Divide.Do(vec1.x, vec2.x);
        }

        /// <summary>
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector1<T> Divide(T num)
        {
            return this/num;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public T Dot(Vector1<T> other)
        {
            return (this*other).x;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector1<T> RespectivelyMultiply(Vector1<T> other)
        {
            return this*other;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector1<T> RespectivelyDivide(Vector1<T> other)
        {
            return this/other;
        }

        /// <summary>
        /// </summary>
        [GenericMethod]
        public Vector1<T> Abs
        {
            get { return x.Abs(); }
        }

        /// <summary>
        /// </summary>
        public T r
        {
            get { return x; }
        }

        /// <summary>
        /// </summary>
        public T r2
        {
            get { return Dot(this); }
        }

        /// <summary>
        /// </summary>
        public T r3
        {
            get { return (T) MathGenerics.Multiply.Do(r2, r); }
        }

        /// <summary>
        /// </summary>
        public Vector1<T> Dir
        {
            get { return this == Zero ? this : this/r; }
        }

        /// <summary>
        /// </summary>
        public T Sum
        {
            get { return x; }
        }
    }
}