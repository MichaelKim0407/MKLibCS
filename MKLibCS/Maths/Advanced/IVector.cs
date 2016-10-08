using System;

namespace MKLibCS.Maths.Advanced
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TElem"></typeparam>
    /// <typeparam name="TVec"></typeparam>
    public interface IVector<TElem, TVec> : IFormattable, IEquatable<TVec>
    {
        /// <summary>
        /// 
        /// </summary>
        TVec Negative { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        TVec Add(TVec other);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        TVec Subtract(TVec other);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        TVec Multiply(TElem num);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        TVec Divide(TElem num);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        TElem Dot(TVec other);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        TVec RespectivelyMultiply(TVec other);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        TVec RespectivelyDivide(TVec other);

        /// <summary>
        /// 
        /// </summary>
        TVec Abs { get; }

        /// <summary>
        /// 
        /// </summary>
        TElem r { get; }
        /// <summary>
        /// 
        /// </summary>
        TElem r2 { get; }
        /// <summary>
        /// 
        /// </summary>
        TElem r3 { get; }

        /// <summary>
        /// 
        /// </summary>
        TVec Dir { get; }

        /// <summary>
        /// 
        /// </summary>
        TElem Sum { get; }
    }
}
