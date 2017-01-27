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
    public struct Vector3<T> : IVector<T, Vector3<T>>
    {
        static Vector3()
        {
            var zero = Calculable<T>.Zero;
            var one = Calculable<T>.One;
            Zero = new Vector3<T>(zero, zero, zero);
            XUnit = new Vector3<T>(one, zero, zero);
            YUnit = new Vector3<T>(zero, one, zero);
            ZUnit = new Vector3<T>(zero, zero, one);
        }

        /// <summary>
        /// </summary>
        [SerializeItem] public Calculable<T> x;

        /// <summary>
        /// </summary>
        [SerializeItem] public Calculable<T> y;

        /// <summary>
        /// </summary>
        [SerializeItem] public Calculable<T> z;

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(T x, T y, T z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector3<T> other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3<T> vec1, Vector3<T> vec2)
        {
            return vec1.Equals(vec2);
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3<T> vec1, Vector3<T> vec2)
        {
            return !(vec1 == vec2);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector3<T>)
                return Equals((Vector3<T>) obj);
            return false;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (x.GetHashCode()*MathUtil.HashFactor + y.GetHashCode())
                   *MathUtil.HashFactor + z.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
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
                       + (y as IFormattable).ToString(format, provider) + ", "
                       + (z as IFormattable).ToString(format, provider) + ")";
            return ToString();
        }

        /// <summary>
        /// </summary>
        [GenericMethod(GenericMethodType.Creator)]
        public static Vector3<T> Zero { get; }

        /// <summary>
        /// </summary>
        [GenericMethod("One", GenericMethodType.Creator)]
        public static Vector3<T> XUnit { get; }

        /// <summary>
        /// </summary>
        public static Vector3<T> YUnit { get; }

        /// <summary>
        /// </summary>
        public static Vector3<T> ZUnit { get; }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector3<T> operator -(Vector3<T> vec)
        {
            return new Vector3<T>(-vec.x, -vec.y, -vec.z);
        }

        /// <summary>
        /// </summary>
        public Vector3<T> Negative => -this;

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector3<T> operator +(Vector3<T> vec1, Vector3<T> vec2)
        {
            return new Vector3<T>(vec1.x + vec2.x, vec1.y + vec2.y, vec1.z + vec2.z);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3<T> Add(Vector3<T> other)
        {
            return this + other;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector3<T> operator -(Vector3<T> vec1, Vector3<T> vec2)
        {
            return new Vector3<T>(vec1.x - vec2.x, vec1.y - vec2.y, vec1.z - vec2.z);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3<T> Subtract(Vector3<T> other)
        {
            return this - other;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static T operator *(Vector3<T> vec1, Vector3<T> vec2)
        {
            return vec1.x*vec2.x + vec1.y*vec2.y + vec1.z*vec2.z;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector3<T> operator *(Vector3<T> vec, T num)
        {
            return new Vector3<T>(vec.x*num, vec.y*num, vec.z*num);
        }

        /// <summary>
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector3<T> Multiply(T num)
        {
            return this*num;
        }

        /// <summary>
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Vector3<T> operator /(Vector3<T> vec, T num)
        {
            return new Vector3<T>(vec.x/num, vec.y/num, vec.z/num);
        }

        /// <summary>
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector3<T> Divide(T num)
        {
            return this/num;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public T Dot(Vector3<T> other)
        {
            return this*other;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3<T> RespectivelyMultiply(Vector3<T> other)
        {
            return new Vector3<T>(x*other.x, y*other.y, z*other.z);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector3<T> RespectivelyDivide(Vector3<T> other)
        {
            return new Vector3<T>(x/other.x, y/other.y, z/other.z);
        }

        /// <summary>
        /// </summary>
        [GenericMethod]
        public Vector3<T> Abs => new Vector3<T>(x.Abs, y.Abs, z.Abs);

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
        public Vector3<T> Dir => this == Zero ? this : this/r;

        /// <summary>
        /// </summary>
        public T Sum => x + y + z;
    }
}