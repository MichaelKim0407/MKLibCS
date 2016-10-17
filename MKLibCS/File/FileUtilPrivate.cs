using System;
using System.Collections.Generic;
using System.Reflection;

using MKLibCS.Reflection;
using MKLibCS.Generic;

namespace MKLibCS.File
{
    static class FileUtilPrivate
    {
        public static bool IsStruct(this TypeInfo type)
        {
            return type.IsValueType && !type.IsEnum;
        }

        public static bool IsSLPredefSingle(this TypeInfo type)
        {
            return (GenericUtil.Parse.Contains(type)
                && GenericUtil.Format.Contains(type))
                || type.IsEnum;
        }

        public static bool IsSLPredefComplex(this TypeInfo type)
        {
            return (type.IsGenericType
                    && (type.GetGenericTypeDefinition() == typeof(List<>)
                        || type.GetGenericTypeDefinition() == typeof(Dictionary<,>)
                        || type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)));
        }

        public static bool IsSLComplex(this TypeInfo type)
        {
            return type.IsSLCustomStructType()
                || type.IsSLCustomComplexType()
                || type.IsSLPredefComplex();
        }

        #region FileSLCustomAttribute

        public static bool IsSLCustomType(this TypeInfo type)
        {
            return type.GetCustomAttribute<FileSLCustomAttribute>() != null;
        }

        public static FileSLCustomAttribute GetSLCustomAttribute(this TypeInfo type)
        {
            return type.GetCustomAttribute<FileSLCustomAttribute>();
        }

        public static bool IsSLCustomLoadDefaultType(this TypeInfo type)
        {
            return type.IsSLCustomType() && type.GetSLCustomAttribute().LoadDefault;
        }

        public static bool IsSLCustomSingleType(this TypeInfo type)
        {
            return type.IsSLCustomType()
                && type.GetSLCustomAttribute().method == FileSLCustomMethod.Single;
        }

        public static bool IsSLCustomSingleObj(this object obj)
        {
            return obj.GetObjTypeInfo().IsSLCustomSingleType();
        }

        public static bool IsSLCustomStructType(this TypeInfo type)
        {
            return type.IsSLCustomType()
                && type.GetSLCustomAttribute().method == FileSLCustomMethod.Struct;
        }

        public static bool IsSLCustomStructObj(this object obj)
        {
            return obj.GetObjTypeInfo().IsSLCustomStructType();
        }

        public static bool IsSLCustomComplexType(this TypeInfo type)
        {
            return type.IsSLCustomType()
                && type.GetSLCustomAttribute().method == FileSLCustomMethod.Complex;
        }

        public static bool IsSLCustomComplexObj(this object obj)
        {
            return obj.GetObjTypeInfo().IsSLCustomComplexType();
        }

        #endregion

        #region FileSLItemAttribute

        public static FileSLItemAttribute GetFileSLItemAttribute(this MemberInfo member)
        {
            return member.GetCustomAttribute<FileSLItemAttribute>();
        }

        public static MemberInfo GetSLCustomSingleMember(this object obj)
        {
            foreach (var member in obj.GetObjTypeInfo().GetFieldsAndProperties())
            {
                FileSLItemAttribute att = member.GetFileSLItemAttribute();
                if (att == null || att.SkipItem(member.GetValue(obj)))
                    continue;
                return member;
            }
            return null;
        }

        public static Action<FileNode> GetSLCustomComplexLoadMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSLCustomAttribute().LoadMethod)
                .CreateDelegate<Action<FileNode>>();
        }

        public static Action<FileNode> GetSLCustomComplexSaveMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSLCustomAttribute().SaveMethod)
                .CreateDelegate<Action<FileNode>>();
        }

        public static Action GetSLCustomLoadDefaultMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSLCustomAttribute().LoadDefaultMethod)
                .CreateDelegate<Action>();
        }

        #endregion
    }
}
