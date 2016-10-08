using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using MKLibCS.Collections;
using MKLibCS.Generic;
using MKLibCS.File;

namespace MKLibCS.Maths.Advanced
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GenericUsage(typeof(MathGenerics))]
    [FileSLCustom(FileSLCustomMethod.Single)]
    public class Vector<T> : IVector<T, Vector<T>>
    {
        static Vector()
        {
            GenericUtil.Create.AddCreator(() => new Vector<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        [FileSLItem]
        public List<Vector1<T>> list;

        /// <summary>
        /// 
        /// </summary>
        virtual public int Dimensions
        {
            get { return list.Count; }
            set
            {
                var diff = value - Dimensions;
                if (diff == 0)
                    return;
                else if (diff > 0)
                    list.AddRange(Vector1<T>.Zero, diff);
                else
                    list.RemoveRange(value, diff);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get { return list[index].x; }
            set { list[index] = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T> Values
        {
            get
            {
                foreach (var v in list)
                    yield return v.x;
            }
            set
            {
                list = new List<Vector1<T>>();
                foreach (var v in value)
                    list.Add(v);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public Vector(IEnumerable<T> values)
        {
            this.Values = values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        public Vector(params T[] values)
            : this(values.AsEnumerable())
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copyFrom"></param>
        public Vector(Vector<T> copyFrom)
            : this(copyFrom.list)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copyFrom"></param>
        /// <param name="dimensions"></param>
        public Vector(Vector<T> copyFrom, int dimensions)
            : this(copyFrom)
        {
            this.Dimensions = dimensions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        static public explicit operator Vector<T>(T[] values)
        {
            return new Vector<T>(values);
        }

        private Vector(IEnumerable<Vector1<T>> list)
        {
            this.list = list.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vector<T> other)
        {
            return list.SequenceEqual(other.list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        static public bool operator ==(Vector<T> vec1, Vector<T> vec2)
        {
            return vec1.Equals(vec2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        static public bool operator !=(Vector<T> vec1, Vector<T> vec2)
        {
            return !(vec1 == vec2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is Vector<T>)
                return Equals(obj as Vector<T>);
            else
                return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return list.Hash_Prime(31);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + Values.ToString(", ") + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider provider)
        {
            if (typeof(T).GetTypeInfo().IsSubclassOf(typeof(IFormattable)))
                return "(" + Values.ToString(
                    x => (x as IFormattable).ToString(format, provider),
                    ", "
                    ) + ")";
            else
                return ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensions"></param>
        /// <returns></returns>
        static public Vector<T> Zero(int dimensions)
        {
            return new Vector<T>(Vector1<T>.Zero.CreateCollection(dimensions));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Vector<T> Zero()
        {
            return Zero(Dimensions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensions"></param>
        /// <param name="dimension"></param>
        /// <returns></returns>
        static public Vector<T> Unit(int dimensions, int dimension)
        {
            var zero = Zero(dimensions);
            zero.list[dimension] = Vector1<T>.XUnit;
            return zero;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimension"></param>
        /// <returns></returns>
        public Vector<T> Unit(int dimension)
        {
            return Unit(Dimensions, dimension);
        }

        static private Vector<T> DoOperation(
            Vector<T> vec,
            Func<Vector1<T>, Vector1<T>> oper
            )
        {
            return new Vector<T>(
                vec.list.ConvertAll(oper));
        }

        static private Vector<T> DoOperation(
            Vector<T> vec1,
            Vector<T> vec2,
            Func<Vector1<T>, Vector1<T>, Vector1<T>> oper
            )
        {
            return new Vector<T>(
                vec1.list.Operation(
                    new Vector<T>(vec2, vec1.Dimensions).list,
                    oper));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        [GenericMethod]
        static public Vector<T> operator -(Vector<T> vec)
        {
            return DoOperation(vec, a => -a);
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector<T> Negative
        {
            get { return -this; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        static public Vector<T> operator +(Vector<T> vec1, Vector<T> vec2)
        {
            return DoOperation(vec1, vec2, (a, b) => a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector<T> Add(Vector<T> other)
        {
            return this + other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        static public Vector<T> operator -(Vector<T> vec1, Vector<T> vec2)
        {
            return DoOperation(vec1, vec2, (a, b) => a - b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector<T> Subtract(Vector<T> other)
        {
            return this - other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [GenericMethod]
        static public Vector<T> operator *(Vector<T> vec, T num)
        {
            return DoOperation(vec, a => a * num);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <param name="vec"></param>
        /// <returns></returns>
        [GenericMethod]
        static public Vector<T> operator *(T num, Vector<T> vec)
        {
            return vec * num;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector<T> Multiply(T num)
        {
            return this * num;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        [GenericMethod]
        static public Vector<T> operator /(Vector<T> vec, T num)
        {
            return DoOperation(vec, a => a / num);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Vector<T> Divide(T num)
        {
            return this / num;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        [GenericMethod]
        static public T operator *(Vector<T> vec1, Vector<T> vec2)
        {
            return RespectivelyMultiply(vec1, vec2).Sum;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public T Dot(Vector<T> other)
        {
            return this * other;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        static public Vector<T> RespectivelyMultiply(Vector<T> vec1, Vector<T> vec2)
        {
            return DoOperation(vec1, vec2, (a, b) => a * b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector<T> RespectivelyMultiply(Vector<T> other)
        {
            return RespectivelyMultiply(this, other);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        static public Vector<T> RespectivelyDivide(Vector<T> vec1, Vector<T> vec2)
        {
            return DoOperation(vec1, vec2, (a, b) => a / b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Vector<T> RespectivelyDivide(Vector<T> other)
        {
            return RespectivelyDivide(this, other);
        }

        /// <summary>
        /// 
        /// </summary>
        [GenericMethod]
        public Vector<T> Abs { get { return DoOperation(this, a => a.Abs); } }

        /// <summary>
        /// 
        /// </summary>
        public T r { get { return r2.Sqrt(); } }
        /// <summary>
        /// 
        /// </summary>
        public T r2 { get { return this * this; } }
        /// <summary>
        /// 
        /// </summary>
        public T r3 { get { return (T)MathGenerics.Multiply.Do(r2, r); } }

        /// <summary>
        /// 
        /// </summary>
        public Vector<T> Dir { get { return this == Zero() ? this : this / r; } }

        /// <summary>
        /// 
        /// </summary>
        public T Sum
        {
            get
            {
                var sum = Vector1<T>.Zero;
                foreach (var value in list)
                    sum += value;
                return sum.Sum;
            }
        }
    }
}
