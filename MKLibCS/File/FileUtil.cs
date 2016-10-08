using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using MKLibCS.Reflection;
using MKLibCS.Generic;

namespace MKLibCS.File
{
    /// <summary>
    /// 
    /// </summary>
    static public class FileUtil
    {
        static private void LoadDefault(this object obj)
        {
            if (obj.GetObjTypeInfo().IsSLCustomLoadDefaultType())
                obj.GetSLCustomLoadDefaultMethod()();
        }

        #region Load & Save

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="node"></param>
        static public void Load(this object obj, FileNode node)
        {
            obj.LoadDefault();
            if (node != null)
                ReadNode(node, ref obj, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="node"></param>
        static public void Save(this object obj, FileNode node)
        {
            WriteNode(node, obj, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        static public void LoadFile(this object obj, string fileName)
        {
            obj.Load(new FileNode(fileName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        static public void SaveFile(this object obj, string fileName)
        {
            FileNode node = new FileNode();
            obj.Save(node);
            node.WriteFile(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <param name="member"></param>
        static public void LoadItem(
            this FileNode node,
            string name,
            ref object val,
            MemberInfo member
            )
        {
            Read(node, name, ref val, member);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <param name="member"></param>
        static public void SaveItem(
            this FileNode node,
            string name,
            object val,
            MemberInfo member
            )
        {
            Write(node, name, val, member);
        }

        #endregion

        #region Read & Write

        #region Read

        static private void ReadFieldOrProperty(
            this object obj,
            MemberInfo member,
            FileNode node,
            string name
            )
        {
            object val = member.GetValue(obj);
            Read(node, name, ref val, member);
            member.SetValue(obj, val);
        }

        static private object CreateObject(Type type)
        {
            return GenericUtil.CreateInstance(type);
        }

        static private void Read(
            FileNode node,
            string name,
            ref object result,
            MemberInfo member
            )
        {
            FileSLItemInfo itemInfo = member;
            ExceptionInfo exceptionInfo = member;
            if (result != null)
                result.LoadDefault();
            var type = itemInfo.Type;
            var typeInfo = type.GetTypeInfo();
            if (node.ContainsItem(name) && (typeInfo.IsSLPredefSingle() || typeInfo.IsSLCustomSingleType()))
            {
                result = CreateObject(type);
                ReadItem(node.GetItem(name), ref result, exceptionInfo);
            }
            else if (node.ContainsNode(name) && (typeInfo.IsSLComplex() || typeInfo.IsSLCustomSingleType()))
            {
                result = CreateObject(type);
                ReadNode(node.GetNode(name), ref result, exceptionInfo);
            }
            else // no matching item or node found
            {
                var att = itemInfo.Attr;
                if (att.skipNull)
                    result = null;
                else if (att.skipEmptyString && typeInfo.IsSubclassOf(typeof(string)))
                    result = "";
                else if (!att.useDefault)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_1);
            }
        }

        static private void ReadItem(
            string value,
            ref object result,
            ExceptionInfo exceptionInfo
            )
        {
            var type = result.GetType();
            var typeInfo = type.GetTypeInfo();
            if (GenericUtil.Parse.Contains(type))
                result = GenericUtil.Parse.Parse(type, value);
            else if (typeInfo.IsEnum)
            {
                object i = (int)result;
                ReadItem(value, ref i, exceptionInfo);
                result = i;
            }
            else if (result.IsSLCustomSingleObj())
            {
                var member = result.GetSLCustomSingleMember();
                if (member == null)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_0);
                else
                {
                    object val = member.GetValue(result);
                    if (val == null)
                        val = CreateObject(member.GetValueType());
                    ReadItem(value, ref val, member);
                    member.SetValue(result, val);
                }
            }
            else
                throw new TypeNotSupportedException(true, type);
        }

        static private void ReadNode(
            FileNode node,
            ref object result,
            ExceptionInfo exceptionInfo
            )
        {
            var type = result.GetType();
            var typeInfo = type.GetTypeInfo();
            if (result.IsSLCustomStructObj())
            {
                foreach (var member in typeInfo.GetFieldsAndProperties())
                {
                    FileSLItemAttribute att = member.GetFileSLItemAttribute();
                    if (att == null || att.isTesting)
                        continue;
                    ReadFieldOrProperty(result, member, node, att.saveName ?? member.Name);
                }
            }
            else if (result.IsSLCustomComplexObj())
                result.GetSLCustomComplexLoadMethod()(node);
            else if (result.IsSLCustomSingleObj())
            {
                var member = result.GetSLCustomSingleMember();
                if (member == null)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_0);
                else
                {
                    object val = member.GetValue(result);
                    if (val == null)
                        val = CreateObject(member.GetValueType());
                    ReadNode(node, ref val, member);
                    member.SetValue(result, val);
                }
            }
            else if (typeInfo.IsGenericType) // TODO: Change loading logic
            {
                var genericType = typeInfo.GetGenericTypeDefinition();
                var paramTypes = typeInfo.GenericTypeArguments;
                if (genericType == typeof(List<>))
                {
                    typeInfo.GetDeclaredMethod("Clear").Invoke(result, null);
                    if (node.ContainsItem("item"))
                    {
                        foreach (string value in node.GetItems("item"))
                        {
                            object item = CreateObject(paramTypes[0]);
                            ReadItem(value, ref item, exceptionInfo);
                            object[] par = { item };
                            typeInfo.GetDeclaredMethod("Add").Invoke(result, par);
                        }
                    }
                    else if (node.ContainsNode("item"))
                    {
                        foreach (FileNode itemNode in node.GetNodes("item"))
                        {
                            object item = CreateObject(paramTypes[0]);
                            ReadNode(itemNode, ref item, exceptionInfo);
                            object[] par = { item };
                            typeInfo.GetDeclaredMethod("Add").Invoke(result, par);
                        }
                    }
                }
                else if (genericType == typeof(Dictionary<,>))
                {
                    typeInfo.GetDeclaredMethod("Clear").Invoke(result, null);
                    foreach (FileNode itemNode in node.GetNodes("item"))
                    {
                        object key = CreateObject(paramTypes[0]);
                        object val = CreateObject(paramTypes[1]);
                        var kvpType = typeof(KeyValuePair<,>).MakeGenericType(paramTypes[0], paramTypes[1]).GetTypeInfo();
                        Read(itemNode, "key", ref key, kvpType.GetDeclaredProperty("Key"));
                        Read(itemNode, "value", ref val, kvpType.GetDeclaredProperty("Value"));
                        object[] par = { key, val };
                        typeInfo.GetDeclaredMethod("Add").Invoke(result, par);
                    }
                }
                else
                    throw new TypeNotSupportedException(true, type);
            }
            else
                throw new TypeNotSupportedException(true, result.GetType());
        }

        #endregion

        #region Write

        static private void Write(
            FileNode node,
            string name,
            object value,
            ExceptionInfo exceptionInfo
            )
        {
            if (value == null)
                throw new NullObjectException(false, name);
            var type = value.GetType();
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsSLPredefSingle())
            {
                if (GenericUtil.Format.Contains(type))
                    node.AddItem(name, GenericUtil.Format.Do(value));
                else if (typeInfo.IsEnum)
                    Write(node, name, (int)value, exceptionInfo);
                else
                    node.AddItem(name, value);
            }
            else if (typeInfo.IsSLComplex())
                WriteNode(node.AddNode(name), value, exceptionInfo);
            else if (value.IsSLCustomSingleObj())
            {
                var member = value.GetSLCustomSingleMember();
                if (member == null)
                    throw new WritingFailureException(exceptionInfo, WritingFailureException.Reason_0);
                else
                    Write(node, name, member.GetValue(value), member);
            }
            else
                throw new TypeNotSupportedException(false, type);
        }

        static private void WriteNode(
            FileNode node,
            object value,
            ExceptionInfo exceptionInfo
            )
        {
            var type = value.GetType();
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsSLPredefSingle())
                throw new WritingFailureException(exceptionInfo, WritingFailureException.Reason_1);
            else if (value.IsSLCustomStructObj())
            {
                foreach (var member in typeInfo.GetFieldsAndProperties())
                {
                    FileSLItemAttribute att = member.GetFileSLItemAttribute();
                    if (att == null || att.SkipItem(member.GetValue(value)))
                        continue;
                    Write(node, att.saveName ?? member.Name, member.GetValue(value), member);
                }
            }
            else if (value.IsSLCustomComplexObj())
                value.GetSLCustomComplexSaveMethod()(node);
            else if (value.IsSLCustomSingleObj())
            {
                var member = value.GetSLCustomSingleMember();
                if (member == null)
                    throw new WritingFailureException(exceptionInfo, WritingFailureException.Reason_0);
                else
                    WriteNode(node, member.GetValue(value), member);
            }
            else if (typeInfo.IsGenericType)
            {
                var genericType = type.GetGenericTypeDefinition();
                if (genericType == typeof(List<>) || genericType == typeof(Dictionary<,>))
                {
                    IEnumerator enumerator = (IEnumerator)typeInfo.GetDeclaredMethod("GetEnumerator").Invoke(value, null);
                    while (enumerator.MoveNext())
                    {
                        object item = enumerator.Current;
                        Write(node, "item", item, exceptionInfo);
                    }
                }
                else if (genericType == typeof(KeyValuePair<,>))
                {
                    object key = value.GetObjTypeInfo().GetDeclaredMethod("get_Key").Invoke(value, null);
                    Write(node, "key", key, exceptionInfo);
                    object val = value.GetObjTypeInfo().GetDeclaredMethod("get_Value").Invoke(value, null);
                    Write(node, "value", val, exceptionInfo);
                }
            }
        }

        #endregion

        #endregion
    }
}
