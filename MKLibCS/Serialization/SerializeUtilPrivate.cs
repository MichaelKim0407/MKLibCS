using System;
using System.Reflection;
using MKLibCS.Generic;
using MKLibCS.Reflection;

namespace MKLibCS.Serialization
{
#if LEGACY
    using TypeInfo = Type;

#endif

    internal static class SerializeUtilPrivate
    {
        public static bool IsStruct(this TypeInfo type)
        {
            return type.IsValueType && !type.IsEnum;
        }

        public static bool IsSerializePredefSingle(this TypeInfo type)
        {
            return (GenericUtil.Parse.Contains(type)
                    && GenericUtil.Format.Contains(type))
                   || type.IsEnum;
        }

        public static bool IsSerializePredefCustom(this TypeInfo type)
        {
            return type.IsList() || type.IsDict() || type.IsKeyValuePair();
        }

        public static bool IsSerializeCustom(this TypeInfo type)
        {
            return type.IsSerializeObjectStructType()
                   || type.IsSerializeObjectCustomType()
                   || type.IsSerializePredefCustom();
        }

        #region SerializeObjectAttribute

        public static bool IsSerializeObjectType(this TypeInfo type)
        {
            return type.GetCustomAttribute<SerializeObjectAttribute>() != null;
        }

        public static SerializeObjectAttribute GetSerializeObjectAttribute(this TypeInfo type)
        {
            return type.GetCustomAttribute<SerializeObjectAttribute>();
        }

        public static bool IsSerializeObjectLoadDefaultType(this TypeInfo type)
        {
            return type.IsSerializeObjectType() && type.GetSerializeObjectAttribute().LoadDefault;
        }

        public static bool IsSerializeObjectSingleType(this TypeInfo type)
        {
            return type.IsSerializeObjectType()
                   && type.GetSerializeObjectAttribute().Method == SerializeObjectMethod.Single;
        }

        public static bool IsSerializeObjectSingle(this object obj)
        {
            return obj.GetObjTypeInfo().IsSerializeObjectSingleType();
        }

        public static bool IsSerializeObjectStructType(this TypeInfo type)
        {
            return type.IsSerializeObjectType()
                   && type.GetSerializeObjectAttribute().Method == SerializeObjectMethod.Struct;
        }

        public static bool IsSerializeObjectStruct(this object obj)
        {
            return obj.GetObjTypeInfo().IsSerializeObjectStructType();
        }

        public static bool IsSerializeObjectCustomType(this TypeInfo type)
        {
            return type.IsSerializeObjectType()
                   && type.GetSerializeObjectAttribute().Method == SerializeObjectMethod.Custom;
        }

        public static bool IsSerializeObjectCustom(this object obj)
        {
            return obj.GetObjTypeInfo().IsSerializeObjectCustomType();
        }

        #endregion

        #region SerializeItemAttribute

        public static SerializeItemAttribute GetSerializeItemAttribute(this MemberInfo member)
        {
            return member.GetCustomAttribute<SerializeItemAttribute>();
        }

        public static MemberInfo GetSerializeObjectSingleMember(this object obj)
        {
            foreach (var kvp in obj.GetObjTypeInfo()
                .GetFieldsAndPropertiesWithAttributeDict<SerializeItemAttribute>())
            {
                var member = kvp.Key;
                var att = kvp.Value[0];
                if (att == null || att.SkipItem(member.GetValue(obj)))
                    continue;
                return member;
            }
            return null;
        }

        public static Action<SerializeNode> GetSerializeObjectCustomLoadMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSerializeObjectAttribute().LoadMethod)
                .CreateDelegate<Action<SerializeNode>>();
        }

        public static Action<SerializeNode> GetSerializeObjectCustomSaveMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSerializeObjectAttribute().SaveMethod)
                .CreateDelegate<Action<SerializeNode>>();
        }

        public static Action GetSerializeObjectLoadDefaultMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSerializeObjectAttribute().LoadDefaultMethod)
                .CreateDelegate<Action>();
        }

        #endregion
    }
}