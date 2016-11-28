using System;
using System.Collections.Generic;
using System.Linq;
using MKLibCS.Maths;

namespace MKLibCS.Collections
{
    /// <summary>
    /// Utilities and extensions for System.Collections &amp; System.Collections.Generic types
    /// </summary>
    public static class CollectionsUtil
    {
        #region A

        /// <summary>
        /// Adds identical items to a list
        /// </summary>
        /// <typeparam name="T">The type of elements in the list, as well as the item added</typeparam>
        /// <param name="list"></param>
        /// <param name="item">The item added</param>
        /// <param name="size">The number of identical items to be added</param>
        public static void AddRange<T>(this List<T> list, T item, int size)
        {
            list.AddRange(item.CreateCollection(size));
        }

        #endregion

        #region C

        /// <summary>
        /// Combines collections into a single collection
        /// </summary>
        /// <typeparam name="T">The type of elements in collections</typeparam>
        /// <param name="collections">A collection of collections</param>
        /// <returns>The combined collection</returns>
        public static IEnumerable<T> Combine<T>(this IEnumerable<IEnumerable<T>> collections)
        {
            foreach (var c in collections)
                foreach (var item in c)
                    yield return item;
        }

        /// <summary>
        /// Combines collections into a single collection
        /// </summary>
        /// <typeparam name="T">The type of elements in collections</typeparam>
        /// <param name="collections">An array of collections</param>
        /// <returns>The combined collection</returns>
        public static IEnumerable<T> Combine<T>(params IEnumerable<T>[] collections)
        {
            return collections.Combine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="size"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T[] CreateArray<T>(this T item, int size)
        {
            return item.CreateCollection(size).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IEnumerable<T> CreateCollection<T>(this T item, int size)
        {
            return new object[size].Select(o => item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="size"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<T> CreateList<T>(this T item, int size)
        {
            return item.CreateCollection(size).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static T[] CreateNumericArray<T>(T start, T end, T step) where T : IComparable<T>
        {
            return CreateNumericCollection(start, end, step).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static IEnumerable<T> CreateNumericCollection<T>(T start, T end, T step) where T : IComparable<T>
        {
            var compare = step.CompareTo(MathGenerics.Zero.GetValue<T>());
            if (compare == 0)
                throw new ArgumentException("cannot be zero", nameof(step));
            if (compare > 0)
            {
                for (var i = start; i.CompareTo(end) <= 0; i = (T) MathGenerics.Add.Do(i, step))
                    yield return i;
            }
            else
            {
                for (var i = start; i.CompareTo(end) >= 0; i = (T) MathGenerics.Add.Do(i, step))
                    yield return i;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public static List<T> CreateNumericList<T>(T start, T end, T step) where T : IComparable<T>
        {
            return CreateNumericCollection(start, end, step).ToList();
        }

        #endregion

        #region E

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<T> Exclude<T>(this IEnumerable<T> collection, params T[] items)
        {
            return collection.Except(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static IEnumerable<T> Exclude<T>(this IEnumerable<T> collection, params IEnumerable<T>[] ex)
        {
            return collection.Except(ex.Combine());
        }

        #endregion

        #region F

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(
            this IEnumerable<T> collection,
            Action<T> action
            )
        {
            foreach (var item in collection)
                action(item);
        }

        #endregion

        #region I

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, T item)
        {
            var list = collection as List<T>;
            if (list != null)
                return list.IndexOf(item);
            return collection.ToList().IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection.Count() == 0;
        }

        #endregion

        #region O

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="collection"></param>
        /// <param name="other"></param>
        /// <param name="operation"></param>
        /// <returns></returns>
        public static IEnumerable<V> Operation<T, U, V>(
            this IEnumerable<T> collection,
            IEnumerable<U> other,
            Func<T, U, V> operation
            )
        {
            var list = collection as IList<T> ?? collection.ToList();
            var otherList = other as IList<U> ?? other.ToList();
            if (otherList.Count() != list.Count())
                throw new ArgumentException("does not have the correct length", nameof(other));
            for (var i = 0; i < list.Count(); i++)
                yield return operation(list.ElementAt(i), otherList.ElementAt(i));
        }

        #endregion

        #region T

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<T> Take<T>(this IEnumerable<T> collection, int start, int count)
        {
            var list = collection as IList<T> ?? collection.ToList();
            for (var i = 0; i < count; i++)
                yield return list.ElementAt(start + i);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="keyValuePairCollection"></param>
        /// <returns></returns>
        public static Dictionary<T, U> ToDictionary<T, U>(
            this IEnumerable<KeyValuePair<T, U>> keyValuePairCollection
            )
        {
            return keyValuePairCollection.ToDictionary(i => i.Key, i => i.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="toStringFunc"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string ToString<T>(
            this IEnumerable<T> collection,
            Func<T, string> toStringFunc,
            string seperator
            )
        {
            var result = "";
            var list = collection as IList<T> ?? collection.ToList();
            for (var i = 0; i < list.Count() - 1; i++)
            {
                result += toStringFunc(list.ElementAt(i));
                result += seperator;
            }
            if (!list.IsEmpty())
                result += toStringFunc(list.Last());
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static string ToString<T>(this IEnumerable<T> collection, string seperator)
        {
            return collection.ToString(i => i.ToString(), seperator);
        }

        #endregion
    }
}