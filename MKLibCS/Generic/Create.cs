using System;

namespace MKLibCS.Generic
{
    partial class GenericUtil
    {
        /// <summary>
        /// </summary>
        public static GenericMethod Create { get; set; }

        private static void InitCreate()
        {
            Create = GenericMethod.Get("Create");

            Create.AddCreator(() => "");
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(Type type)
        {
            try
            {
                return Create.GetValue(type);
            }
            catch (MissingGenericMethodException)
            {
                try
                {
                    return Activator.CreateInstance(type);
                }
                catch (MissingMemberException)
                {
                    throw new CreateInstanceFailureException(type);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateInstance<T>()
        {
            return (T) CreateInstance(typeof(T));
        }
    }
}