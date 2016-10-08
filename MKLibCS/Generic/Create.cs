using System;

namespace MKLibCS.Generic
{
    partial class GenericUtil
    {
        /// <summary>
        /// 
        /// </summary>
        static public GenericMethod Create { get; set; }

        static private void InitCreate()
        {
            Create = GenericMethod.Get("Create");

            Create.AddCreator(() => "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public object CreateInstance(Type type)
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
    }
}
