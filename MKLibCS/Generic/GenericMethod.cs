using System;
using System.Collections.Generic;
using System.Linq;
using MKLibCS.Collections;
using MKLibCS.Reflection;
#if LEGACY
using TypeInfo = System.Type;

#else
using System.Reflection;

#endif

namespace MKLibCS.Generic
{
    /// <summary>
    /// </summary>
    public class GenericMethod
    {
        private static Dictionary<string, GenericMethod> allMethods = new Dictionary<string, GenericMethod>();

        private static Dictionary<string, string> methodBinding = new Dictionary<string, string>();

        /// <summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="boundMethods"></param>
        /// <returns></returns>
        public static GenericMethod Get(string name, params string[] boundMethods)
        {
            if (!allMethods.ContainsKey(name))
                allMethods.Add(name, new GenericMethod(name));
            foreach (var methodName in boundMethods)
                AddBinding(methodName, name);
            return allMethods[name];
        }

        /// <summary>
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.BindingNotFoundException"></exception>
        public static GenericMethod GetBound(string methodName)
        {
            try
            {
                return Get(methodBinding[methodName]);
            }
            catch (KeyNotFoundException)
            {
                throw new BindingNotFoundException(methodName);
            }
        }

        private static void AddBinding(string methodName, string name)
        {
            if (methodBinding.ContainsKey(methodName))
                methodBinding[methodName] = name;
            else
                methodBinding.Add(methodName, name);
        }

        private GenericMethod(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// </summary>
        public readonly string Name;

        private struct TypeMethod
        {
            public TypeMethod(Delegate method, params Type[] types)
            {
                this.method = method;
                this.types = types;
            }

            public Delegate method;
            public Type[] types;
        }

        private List<TypeMethod> methods = new List<TypeMethod>();

        /// <summary>
        /// </summary>
        public IEnumerable<Type[]> SupportedTypes => methods.ConvertAll(m => m.types);

        private static Type[] NextMatchingTypes(Type[] types, Type[] subs)
        {
            var count = types.Count();
            Type[] result = new Type[count];
            bool next = true;
            for (int i = count - 1; i >= 0; i--)
            {
                if (next)
                {
                    var sub = subs[i].GetTypeInfo().BaseType;
                    if (sub == null)
                        result[i] = types[i];
                    else
                    {
                        result[i] = sub;
                        next = false;
                    }
                }
                else
                    result[i] = subs[i];
            }
            if (next)
                return null;
            else
                return result;
        }

        private static IEnumerable<Type[]> AllMatchingTypes(Type[] types)
        {
            var t = types;
            while (t != null)
            {
                yield return t;
                t = NextMatchingTypes(types, t);
            }
        }

        private int IndexOf(Type[] types)
        {
            foreach (var type in types)
                GenericUtil.InitType(type);
            return methods.FindIndex(m => m.types.SequenceEqual(types));
        }

        #region Add

        /// <summary>
        /// </summary>
        /// <param name="method"></param>
        /// <param name="types"></param>
        public void Add(Delegate method, params Type[] types)
        {
            var i = IndexOf(types);
            if (i != -1)
                methods.RemoveAt(i);
            methods.Add(new TypeMethod(method, types));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="method"></param>
        public void Add<T>(Func<T, object> method)
        {
            Add(method, typeof(T));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="method"></param>
        public void Add<T1, T2>(Func<T1, T2, object> method)
        {
            Add(method, typeof(T1), typeof(T2));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="method"></param>
        public void Add<T1, T2, T3>(Func<T1, T2, T3, object> method)
        {
            Add(method, typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="creator"></param>
        public void AddCreator<T>(Func<T> creator)
        {
            Add(creator, typeof(T));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        public void AddValue<T>(T val) where T : struct
        {
            AddCreator(() => val);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="parser"></param>
        public void AddParser<T, U>(Func<U, T> parser)
        {
            Add(parser, typeof(T));
        }

        #endregion

        #region Contains

        /// <summary>
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public bool Contains(params Type[] types)
        {
            return IndexOf(types) != -1;
        }

#if !LEGACY
    /// <summary>
    /// </summary>
    /// <param name="types"></param>
    /// <returns></returns>
        public bool Contains(params TypeInfo[] types)
        {
            return Contains(Enumerable.ToArray(types.ConvertAll(t => t.AsType())));
        }
#endif

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public bool Contains<T1, T2>()
        {
            return Contains(typeof(T1), typeof(T2));
        }

        #endregion

        #region Get

        /// <summary>
        ///     Gets the method defined for types.
        /// </summary>
        /// <param name="types">Parameter types for the method</param>
        /// <returns>The method</returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Method for types is not defined.</exception>
        public Delegate Get(params Type[] types)
        {
            int i = -1;
            foreach (var t in AllMatchingTypes(types))
            {
                i = IndexOf(t);
                if (i != -1)
                    break;
            }
            if (i == -1)
                throw new MissingGenericMethodException(Name, types);
            return methods[i].method;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Method for T is not defined.</exception>
        public Func<T, object> Get<T>()
        {
            try
            {
                return (Func<T, object>) Get(typeof(T));
            }
            catch (InvalidCastException)
            {
                return t => Do(t);
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Method for T1, T2 is not defined.</exception>
        public Func<T1, T2, object> Get<T1, T2>()
        {
            try
            {
                return (Func<T1, T2, object>) Get(typeof(T1), typeof(T2));
            }
            catch (InvalidCastException)
            {
                return (t1, t2) => Do(t1, t2);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Parser for type is not defined.</exception>
        public Delegate GetParser(Type type)
        {
            return Get(type);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Parser for T is not defined.</exception>
        public Func<object, T> GetParser<T>()
        {
            try
            {
                return (Func<object, T>) GetParser(typeof(T));
            }
            catch (InvalidCastException)
            {
                return t => (T) Parse(typeof(T), t);
            }
        }

        #endregion

        #region Do

        /// <summary>
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Method for types of parameters is not defined.</exception>
        public object Do(params object[] parameters)
        {
            var method = Get(CollectionsUtil.ConvertAll(parameters, p => p.GetType()).ToArray());
            return method.DynamicInvoke(parameters);
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Value for types is not defined.</exception>
        public object GetValue(Type type)
        {
            var valueGetter = Get(type);
            return valueGetter.DynamicInvoke(new object[0]);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Value for T is not defined.</exception>
        public T GetValue<T>()
        {
            return (T) GetValue(typeof(T));
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Parser for type is not defined.</exception>
        public object Parse(Type type, object item)
        {
            var parser = GetParser(type);
            return parser.DynamicInvoke(CollectionsUtil.CreateArray(item, 1));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <exception cref="MKLibCS.Generic.MissingGenericMethodException">Parser for T is not defined.</exception>
        public T Parse<T>(object item)
        {
            return (T) Parse(typeof(T), item);
        }

        #endregion
    }
}