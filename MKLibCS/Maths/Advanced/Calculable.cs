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
    [GenericUsage(typeof(MathGenerics))]
    [SerializeObject(SerializeObjectMethod.Single)]
    public struct Calculable<T> : IFormattable, IEquatable<T>, IEquatable<Calculable<T>>
    {
        static Calculable()
        {
            Zero = MathGenerics.Zero.GetValue<T>();
            One = MathGenerics.One.GetValue<T>();
        }

        /// <summary>
        /// </summary>
        [SerializeItem] public T value;

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        public Calculable(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator Calculable<T>(T x)
        {
            return new Calculable<T>(x);
        }

        /// <summary>
        /// </summary>
        /// <param name="x"></param>
        public static implicit operator T(Calculable<T> x)
        {
            return x.value;
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Calculable<T> other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(Calculable<T> value1, Calculable<T> value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(Calculable<T> value1, Calculable<T> value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(T other)
        {
            return value.Equals(other);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(Calculable<T> value1, T value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(Calculable<T> value1, T value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator ==(T value1, Calculable<T> value2)
        {
            return value1.Equals(value2.value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool operator !=(T value1, Calculable<T> value2)
        {
            return !(value1 == value2);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is T)
                return Equals((T) obj);
            if (obj is Calculable<T>)
                return Equals((Calculable<T>) obj);
            return false;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return value.ToString();
        }

        /// <summary>
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider provider)
        {
            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(IFormattable)))
                return (value as IFormattable).ToString(format, provider);
            return ToString();
        }

        /// <summary>
        /// </summary>
        [GenericMethod(GenericMethodType.Creator)]
        public static Calculable<T> Zero { get; }

        /// <summary>
        /// </summary>
        [GenericMethod(GenericMethodType.Creator)]
        public static Calculable<T> One { get; }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Calculable<T> operator -(Calculable<T> value)
        {
            return (T) MathGenerics.Negative.Do(value.value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Calculable<T> operator +(Calculable<T> value1, Calculable<T> value2)
        {
            return (T) MathGenerics.Add.Do(value1.value, value2.value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Calculable<T> operator -(Calculable<T> value1, Calculable<T> value2)
        {
            return (T) MathGenerics.Subtract.Do(value1.value, value2.value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Calculable<T> operator *(Calculable<T> value1, Calculable<T> value2)
        {
            return (T) MathGenerics.Multiply.Do(value1.value, value2.value);
        }

        /// <summary>
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        [GenericMethod]
        public static Calculable<T> operator /(Calculable<T> value1, Calculable<T> value2)
        {
            return (T) MathGenerics.Divide.Do(value1.value, value2.value);
        }

        /// <summary>
        /// </summary>
        [GenericMethod]
        public Calculable<T> Abs => value.Abs();
    }
}