using System;
using MKLibCS.Generic;
using MKLibCS.Serialization;
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
    [GenericUsage(typeof(Calculable<>))]
    [SerializeObject(SerializeObjectMethod.Struct)]
    public struct Vector2<T> : IVector<T, Vector2<T>>
    {
        static Vector2()
        {
            var zero = Calculable<T>.Zero;
            var one = Calculable<T>.One;
            Zero = new Vector2<T>(zero, zero);
            XUnit = new Vector2<T>(one, zero);
            YUnit = new Vector2<T>(zero, one);
        }

        /// <summary>
        /// </summary>
        [SerializeItem] public Calculable<T> x;

        /// <summary>
        /// </summary>
        [SerializeItem] public Calculable<T> y;

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(T x, T y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector2<T> other)
        {
            return x.Equals(other.x);
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2<T> vec1, Vector2<T> vec2)
        {
            return vec1.Equals(vec2);
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static bool operator !=(Vector2<T> vec1, Vector2<T> vec2)
        {
            return !(vec1 == vec2);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector2<T>)
                return Equals((Vector2<T>) obj);
            return false;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return x.GetHashCode()*MathUtil.HashFactor + y.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }

        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider provider)
        {
            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(IFormattable)))
                return "(" + (x as IFormattable).ToString(format, provider) + ", "
                    + (y as IFormattable).ToString(format, provider) + ")";
            return ToString();
        }

        /// <summary>
        /// </summary>
        [GenericMethod(GenericMethodType.Creator)]
        public static Vector2<T> Zero { get; }

        /// <summary>
        /// </summary>
        [GenericMethod("One", GenericMethodType.Creator)]
        public static Vector2<T> XUnit { get; }

        /// <summary>
        /// </summary>
        public static Vector2<T> YUnit { get; }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector2<T> operator -(Vector2<T> vec)
        {
            return new Vector2<T>(-vec.x, -vec.y);
        }

        /// <summary>
        /// </summary>
        public Vector2<T> Negative => -this;

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector2<T> operator +(Vector2<T> vec1, Vector2<T> vec2)
        {
            return new Vector2<T>(vec1.x + vec2.x, vec1.y + vec2.y);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector2<T> Add(Vector2<T> other)
        {
            return this + other;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector2<T> operator -(Vector2<T> vec1, Vector2<T> vec2)
        {
            return new Vector2<T>(vec1.x - vec2.x, vec1.y - vec2.y);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector2<T> Subtract(Vector2<T> other)
        {
            return this - other;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static T operator *(Vector2<T> vec1, Vector2<T> vec2)
        {
            return vec1.x*vec2.x + vec1.y*vec2.y;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector2<T> operator *(Vector2<T> vec, T num)
        {
            return new Vector2<T>(vec.x*num, vec.y*num);
        }

        /// <summary>
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector2<T> Multiply(T num)
        {
            return this*num;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector2<T> operator /(Vector2<T> vec, T num)
        {
            return new Vector2<T>(vec.x/num, vec.y/num);
        }

        /// <summary>
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector2<T> Divide(T num)
        {
            return this/num;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public T Dot(Vector2<T> other)
        {
            return this*other;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector2<T> RespectivelyMultiply(Vector2<T> other)
        {
            return new Vector2<T>(x*other.x, y*other.y);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector2<T> RespectivelyDivide(Vector2<T> other)
        {
            return new Vector2<T>(x/other.x, y/other.y);
        }

        /// <summary>
        /// </summary>
        [GenericMethod]
        public Vector2<T> Abs => new Vector2<T>(x.Abs, y.Abs);

        /// <summary>
        /// </summary>
        public T r => x;

        /// <summary>
        /// </summary>
        public T r2 => Dot(this);

        /// <summary>
        /// </summary>
        public T r3 => (T) MathGenerics.Multiply.Do(r2, r);

        /// <summary>
        /// </summary>
        public Vector2<T> Dir => this == Zero ? this : this/r;

        /// <summary>
        /// </summary>
        public T Sum => x + y;
    }
}