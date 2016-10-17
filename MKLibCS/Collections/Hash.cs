using System.Collections.Generic;

namespace MKLibCS.Collections
{
    /// <summary>
    /// Provides hash functions for collections
    /// </summary>
    public static class Hash
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static int Hash_XOR<T>(this IEnumerable<T> collection)
        {
            int result = 0;
            foreach (var item in collection)
                result ^= item.GetHashCode();
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="prime"></param>
        /// <returns></returns>
        public static int Hash_Prime<T>(
            this IEnumerable<T> collection,
            int prime
            )
        {
            int result = 0;
            foreach (var item in collection)
            {
                result *= prime;
                result += item.GetHashCode();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="prime"></param>
        /// <param name="prime2"></param>
        /// <returns></returns>
        public static int Hash_Prime2<T>(
            this IEnumerable<T> collection,
            int prime,
            int prime2
            )
        {
            int result = 0;
            foreach (var item in collection)
            {
                result *= prime;
                result += item.GetHashCode();
                result %= prime2;
            }
            return result;
        }
    }
}