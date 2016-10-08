using System;
using System.Collections.Generic;
using System.Reflection;

using MKLibCS.Reflection;
using MKLibCS.Generic;

namespace MKLibCS.File
{
    static class FileUtilPrivate
    {
        static public bool IsStruct(this TypeInfo type)
        {
            return type.IsValueType && !type.IsEnum;
        }

        static public bool IsSLPredefSingle(this TypeInfo type)
        {
            return (GenericUtil.Parse.Contains(type)
                && GenericUtil.Format.Contains(type))
                || type.IsEnum;
        }

        static public bool IsSLPredefComplex(this TypeInfo type)
        {
            return (type.IsGenericType
                    && (type.GetGenericTypeDefinition() == typeof(List<>)
                        || type.GetGenericTypeDefinition() == typeof(Dictionary<,>)
                        || type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>)));
        }

        static public bool IsSLComplex(this TypeInfo type)
        {
            return type.IsSLCustomStructType()
                || type.IsSLCustomComplexType()
                || type.IsSLPredefComplex();
        }

        #region FileSLCustomAttribute

        static public bool IsSLCustomType(this TypeInfo type)
        {
            return type.GetCustomAttribute<FileSLCustomAttribute>() != null;
        }

        static public FileSLCustomAttribute GetSLCustomAttribute(this TypeInfo type)
        {
            return type.GetCustomAttribute<FileSLCustomAttribute>();
        }

        static public bool IsSLCustomLoadDefaultType(this TypeInfo type)
        {
            return type.IsSLCustomType() && type.GetSLCustomAttribute().LoadDefault;
        }

        static public bool IsSLCustomSingleType(this TypeInfo type)
        {
            return type.IsSLCustomType()
                && type.GetSLCustomAttribute().method == FileSLCustomMethod.Single;
        }

        static public bool IsSLCustomSingleObj(this object obj)
        {
            return obj.GetObjTypeInfo().IsSLCustomSingleType();
        }

        static public bool IsSLCustomStructType(this TypeInfo type)
        {
            return type.IsSLCustomType()
                && type.GetSLCustomAttribute().method == FileSLCustomMethod.Struct;
        }

        static public bool IsSLCustomStructObj(this object obj)
        {
            return obj.GetObjTypeInfo().IsSLCustomStructType();
        }

        static public bool IsSLCustomComplexType(this TypeInfo type)
        {
            return type.IsSLCustomType()
                && type.GetSLCustomAttribute().method == FileSLCustomMethod.Complex;
        }

        static public bool IsSLCustomComplexObj(this object obj)
        {
            return obj.GetObjTypeInfo().IsSLCustomComplexType();
        }

        #endregion

        #region FileSLItemAttribute

        static public FileSLItemAttribute GetFileSLItemAttribute(this MemberInfo member)
        {
            return member.GetCustomAttribute<FileSLItemAttribute>();
        }

        static public MemberInfo GetSLCustomSingleMember(this object obj)
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

        static public Action<FileNode> GetSLCustomComplexLoadMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSLCustomAttribute().LoadMethod)
                .CreateDelegate<Action<FileNode>>();
        }

        static public Action<FileNode> GetSLCustomComplexSaveMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSLCustomAttribute().SaveMethod)
                .CreateDelegate<Action<FileNode>>();
        }

        static public Action GetSLCustomLoadDefaultMethod(this object obj)
        {
            return obj.GetObjTypeInfo()
                .GetMethod(obj.GetObjTypeInfo().GetSLCustomAttribute().LoadDefaultMethod)
                .CreateDelegate<Action>();
        }

        #endregion
    }
}
