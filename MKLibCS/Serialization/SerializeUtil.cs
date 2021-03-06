﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MKLibCS.Generic;
using MKLibCS.Logging;
using MKLibCS.Reflection;

namespace MKLibCS.Serialization
{
    /// <summary>
    /// </summary>
    public static class SerializeUtil
    {
        private static readonly Logger logger = new Logger(typeof(SerializeUtil));
        
        private static readonly GenericMethod _default = GenericMethod.Get("Serialize_Default");
        private static readonly GenericMethod _load = GenericMethod.Get("Serialize_Load");
        private static readonly GenericMethod _save = GenericMethod.Get("Serialize_Save");

        private static void LoadDefault(this object obj)
        {
            if (!obj.GetObjTypeInfo().IsSerializeObjectLoadDefaultType())
                return;
            logger.InternalDebug("Loading default value for object of type {0}", obj.GetType().FullName);
            _default.Do(obj);
        }

        #region Load & Save

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="node"></param>
        public static object Load(this Type type, SerializeNode node)
        {
            return node != null ? ReadNode(node, type, null) : CreateObject(type);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        public static T Load<T>(SerializeNode node)
        {
            return (T) Load(typeof(T), node);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="node"></param>
        public static void Save(this object obj, SerializeNode node)
        {
            WriteNode(node, obj, null);
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fileName"></param>
        public static object LoadFile(this Type type, string fileName)
        {
            return type.Load(new SerializeNode(fileName));
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        public static T LoadFile<T>(string fileName)
        {
            return (T) LoadFile(typeof(T), fileName);
        }

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void SaveFile(this object obj, string fileName)
        {
            var node = new SerializeNode();
            obj.Save(node);
            node.WriteFile(fileName);
        }

        /// <summary>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="member"></param>
        public static object LoadItem(
            this SerializeNode node,
            string name,
            MemberInfo member
            )
        {
            return Read(node, name, member);
        }

        /// <summary>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="val"></param>
        /// <param name="member"></param>
        public static void SaveItem(
            this SerializeNode node,
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

        private static void ReadFieldOrProperty(
            this object obj,
            MemberInfo member,
            SerializeNode node,
            string name
            )
        {
            var val = Read(node, name, member);
            member.SetValue(obj, val);
        }

        private static object CreateObject(Type type)
        {
            var obj = GenericUtil.CreateInstance(type);
            obj.LoadDefault();
            return obj;
        }

        private static object Read(
            SerializeNode node,
            string name,
            MemberInfo member
            )
        {
            SerializeItemInfo itemInfo = member;
            ExceptionInfo exceptionInfo = member;
            var type = itemInfo.Type;
            var typeInfo = type.GetTypeInfo();
            logger.InternalDebug("Item \"{0}\" has type \"{1}\"", name, typeInfo.FullName);
            var result = CreateObject(type);
            if (node.ContainsItem(name) &&
                (typeInfo.IsSerializePredefSingle() || typeInfo.IsSerializeObjectSingleType()))
            {
                logger.InternalDebug("Parsing item \"{0}\"", name);
                result = ReadItem(node.GetItem(name), type, exceptionInfo);
            }
            else if (node.ContainsNode(name) && (typeInfo.IsSerializeCustom() || typeInfo.IsSerializeObjectSingleType()))
            {
                logger.InternalDebug("Creating node \"{0}\"", name);
                result = ReadNode(node.GetNode(name), type, exceptionInfo);
            }
            else // no matching item or node found
            {
                var att = itemInfo.Attr;
                if (att.SkipNull)
                {
                    result = null;
                    logger.InternalDebug("Item \"{0}\" not found in node. Assign null.", name);
                }
                else if (att.SkipEmptyString && typeInfo.IsSubclassOf(typeof(string)))
                {
                    result = "";
                    logger.InternalDebug("Item \"{0}\" not found in node. Assign empty string.", name);
                }
                else if (!att.UseDefault)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_1);
                else
                    logger.InternalDebug("Item \"{0}\" not found in node. Do nothing.", name);
            }
            return result;
        }

        private static object ReadItem(
            string value,
            Type type,
            ExceptionInfo exceptionInfo
            )
        {
            var result = CreateObject(type);
            var typeInfo = type.GetTypeInfo();
            if (GenericUtil.Parse.Contains(type))
            {
                result = GenericUtil.Parse.Parse(type, value);
                logger.InternalDebug("Value parsed as {0}", result);
            }
            else if (typeInfo.IsEnum)
            {
                result = ReadItem(value, typeof(int), exceptionInfo);
            }
            else if (result.IsSerializeObjectSingle())
            {
                var member = typeInfo.GetSerializeObjectSingleMember();
                if (member == null)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_0);
                var val = member.GetValue(result);
                if (val == null)
                    val = CreateObject(member.GetValueType());
                result = ReadItem(value, type, member);
                member.SetValue(result, val);
            }
            else
                throw new TypeNotSupportedException(true, type);
            return result;
        }

        private static object ReadNode(
            SerializeNode node,
            Type type,
            ExceptionInfo exceptionInfo
            )
        {
            var result = CreateObject(type);
            var typeInfo = type.GetTypeInfo();
            if (result.IsSerializeObjectStruct())
            {
                foreach (var kvp in typeInfo.GetFieldsAndPropertiesWithAttributeDict<SerializeItemAttribute>())
                {
                    var member = kvp.Key;
                    var att = kvp.Value[0];
                    if (att == null || att.Ignore)
                        continue;
                    ReadFieldOrProperty(result, member, node, att.SerializeName ?? member.Name);
                }
            }
            else if (result.IsSerializeObjectCustom())
                _load.Do(result, node);
            else if (result.IsSerializeObjectSingle())
            {
                var member = typeInfo.GetSerializeObjectSingleMember();
                if (member == null)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_0);
                var val = member.GetValue(result);
                if (val == null)
                    val = CreateObject(member.GetValueType());
                result = ReadNode(node, type, member);
                member.SetValue(result, val);
            }
            else if (typeInfo.IsGenericType)
            {
                Type[] paramTypes;
                if (typeInfo.IsList(out paramTypes))
                {
                    result = CreateObject(type);
                    var itemType = paramTypes[0];
                    if (node.ContainsItem("item"))
                    {
                        foreach (var value in node.GetItems("item"))
                        {
                            var item = ReadItem(value, itemType, exceptionInfo);
                            object[] par = {item};
                            typeInfo.GetMethod("Add").Invoke(result, par);
                        }
                    }
                    else if (node.ContainsNode("item"))
                    {
                        foreach (var itemNode in node.GetNodes("item"))
                        {
                            var item = ReadNode(itemNode, paramTypes[0], exceptionInfo);
                            object[] par = {item};
                            typeInfo.GetMethod("Add").Invoke(result, par);
                        }
                    }
                }
                else if (typeInfo.IsDict(out paramTypes))
                {
                    result = CreateObject(type);
                    var keyType = paramTypes[0];
                    var valueType = paramTypes[1];
                    foreach (var itemNode in node.GetNodes("item"))
                    {
                        var kvpType = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType).GetTypeInfo();
                        var key = Read(itemNode, "key", kvpType.GetProperty("Key"));
                        var val = Read(itemNode, "value", kvpType.GetProperty("Value"));
                        object[] par = {key, val};
                        typeInfo.GetMethod("Add").Invoke(result, par);
                    }
                }
                else
                    throw new TypeNotSupportedException(true, type);
            }
            else
                throw new TypeNotSupportedException(true, result.GetType());
            return result;
        }

        #endregion

        #region Write

        private static void Write(
            SerializeNode node,
            string name,
            object value,
            ExceptionInfo exceptionInfo
            )
        {
            if (value == null)
                throw new NullObjectException(false, name);
            var type = value.GetType();
            var typeInfo = type.GetTypeInfo();
            if (typeInfo.IsSerializePredefSingle())
            {
                if (GenericUtil.Format.Contains(type))
                    node.AddItem(name, GenericUtil.Format.Do(value));
                else if (typeInfo.IsEnum)
                    Write(node, name, (int) value, exceptionInfo);
                else
                    node.AddItem(name, value);
            }
            else if (typeInfo.IsSerializeCustom())
                WriteNode(node.AddNode(name), value, exceptionInfo);
            else if (value.IsSerializeObjectSingle())
            {
                var member = typeInfo.GetSerializeObjectSingleMember();
                if (member == null)
                    throw new WritingFailureException(exceptionInfo, WritingFailureException.Reason_0);
                Write(node, name, member.GetValue(value), member);
            }
            else
                throw new TypeNotSupportedException(false, type);
        }

        private static void WriteNode(
            SerializeNode node,
            object value,
            ExceptionInfo exceptionInfo
            )
        {
            var typeInfo = value.GetObjTypeInfo();
            if (typeInfo.IsSerializePredefSingle())
                throw new WritingFailureException(exceptionInfo, WritingFailureException.Reason_1);
            if (value.IsSerializeObjectStruct())
            {
                foreach (var kvp in typeInfo.GetFieldsAndPropertiesWithAttributeDict<SerializeItemAttribute>())
                {
                    var member = kvp.Key;
                    var att = kvp.Value[0];
                    if (att == null || att.SkipItem(member.GetValue(value)))
                        continue;
                    Write(node, att.SerializeName ?? member.Name, member.GetValue(value), member);
                }
            }
            else if (value.IsSerializeObjectCustom())
                _save.Do(value, node);
            else if (value.IsSerializeObjectSingle())
            {
                var member = typeInfo.GetSerializeObjectSingleMember();
                if (member == null)
                    throw new WritingFailureException(exceptionInfo, WritingFailureException.Reason_0);
                WriteNode(node, member.GetValue(value), member);
            }
            else if (typeInfo.IsGenericType)
            {
                if (typeInfo.IsList() || typeInfo.IsDict())
                {
                    var enumerator =
                        (IEnumerator) typeInfo.GetMethod("GetEnumerator").Invoke(value, null);
                    while (enumerator.MoveNext())
                    {
                        var item = enumerator.Current;
                        Write(node, "item", item, exceptionInfo);
                    }
                }
                else if (typeInfo.IsKeyValuePair())
                {
                    var key = value.GetObjTypeInfo().GetMethod("get_Key").Invoke(value, null);
                    Write(node, "key", key, exceptionInfo);
                    var val = value.GetObjTypeInfo().GetMethod("get_Value").Invoke(value, null);
                    Write(node, "value", val, exceptionInfo);
                }
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStringSerialized(this object obj)
        {
            var node = new SerializeNode();
            obj.Save(node);
            return node.ToString();
        }
    }
}