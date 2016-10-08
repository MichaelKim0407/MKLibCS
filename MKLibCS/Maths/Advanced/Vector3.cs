using MKLibCS.Generic;

namespace MKLibCS.Maths.Advanced
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [GenericUsage(typeof(Vector<>))]
    public class Vector3<T> : Vector<T>, IVector<T, Vector3<T>>
    {
        /// <summary>
        /// 
        /// </summary>
        public T x
        {
            get { return base[0]; }
            set { base[0] = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public T y
        {
            get { return base[1]; }
            set { base[1] = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public T z
        {
            get { return base[2]; }
            set { base[2] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Dimensions
        {
            get { return 3; }
            set { base.Dimensions = 3; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public Vector3(T x, T y, T z)
            : base(x, y, z)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        public Vector3(Vector<T> vec)
            : base(vec, 3)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="copyFrom"></param>
        public Vector3(Vector3<T> copyFrom)
            : this(copyFrom as Vector<T>)
        {
        }
    }
}
