using System;
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

        private static void LoadDefault(this object obj)
        {
            if (obj.GetObjTypeInfo().IsSerializeObjectLoadDefaultType())
            {
                logger.InternalDebug("Loading default value for object of type {0}", obj.GetType().FullName);
                obj.GetSerializeObjectLoadDefaultMethod()();
            }
        }

        #region Load & Save

        /// <summary>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="node"></param>
        public static void Load(this object obj, SerializeNode node)
        {
            obj.LoadDefault();
            if (node != null)
                ReadNode(node, ref obj, null);
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
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        public static void LoadFile(this object obj, string fileName)
        {
            obj.Load(new SerializeNode(fileName));
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
        /// <param name="val"></param>
        /// <param name="member"></param>
        public static void LoadItem(
            this SerializeNode node,
            string name,
            ref object val,
            MemberInfo member
            )
        {
            Read(node, name, ref val, member);
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
            var val = member.GetValue(obj);
            Read(node, name, ref val, member);
            member.SetValue(obj, val);
        }

        private static object CreateObject(Type type)
        {
            return GenericUtil.CreateInstance(type);
        }

        private static void Read(
            SerializeNode node,
            string name,
            ref object result,
            MemberInfo member
            )
        {
            SerializeItemInfo itemInfo = member;
            ExceptionInfo exceptionInfo = member;
            var type = itemInfo.Type;
            var typeInfo = type.GetTypeInfo();
            logger.InternalDebug("Item \"{0}\" has type \"{1}\"", name, typeInfo.FullName);
            if (result != null)
                result.LoadDefault();
            if (node.ContainsItem(name) &&
                (typeInfo.IsSerializePredefSingle() || typeInfo.IsSerializeObjectSingleType()))
            {
                result = CreateObject(type);
                logger.InternalDebug("Parsing item \"{0}\"", name);
                ReadItem(node.GetItem(name), ref result, exceptionInfo);
            }
            else if (node.ContainsNode(name) && (typeInfo.IsSerializeCustom() || typeInfo.IsSerializeObjectSingleType()))
            {
                result = CreateObject(type);
                logger.InternalDebug("Creating node \"{0}\"", name);
                ReadNode(node.GetNode(name), ref result, exceptionInfo);
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
        }

        private static void ReadItem(
            string value,
            ref object result,
            ExceptionInfo exceptionInfo
            )
        {
            var type = result.GetType();
            var typeInfo = type.GetTypeInfo();
            if (GenericUtil.Parse.Contains(type))
            {
                result = GenericUtil.Parse.Parse(type, value);
                logger.InternalDebug("Value parsed as {0}", result);
            }
            else if (typeInfo.IsEnum)
            {
                object i = (int) result;
                ReadItem(value, ref i, exceptionInfo);
                result = i;
            }
            else if (result.IsSerializeObjectSingle())
            {
                var member = result.GetSerializeObjectSingleMember();
                if (member == null)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_0);
                var val = member.GetValue(result);
                if (val == null)
                    val = CreateObject(member.GetValueType());
                ReadItem(value, ref val, member);
                member.SetValue(result, val);
            }
            else
                throw new TypeNotSupportedException(true, type);
        }

        private static void ReadNode(
            SerializeNode node,
            ref object result,
            ExceptionInfo exceptionInfo
            )
        {
            var type = result.GetType();
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
                result.GetSerializeObjectCustomLoadMethod()(node);
            else if (result.IsSerializeObjectSingle())
            {
                var member = result.GetSerializeObjectSingleMember();
                if (member == null)
                    throw new ParsingFailureException(exceptionInfo, ParsingFailureException.Reason_0);
                var val = member.GetValue(result);
                if (val == null)
                    val = CreateObject(member.GetValueType());
                ReadNode(node, ref val, member);
                member.SetValue(result, val);
            }
            else if (typeInfo.IsGenericType) // TODO: Change loading logic <- I forgot what I wanted to change
            {
                Type[] paramTypes;
                if (typeInfo.IsList(out paramTypes))
                {
                    var itemType = paramTypes[0];
                    typeInfo.GetMethod("Clear").Invoke(result, null);
                    if (node.ContainsItem("item"))
                    {
                        foreach (var value in node.GetItems("item"))
                        {
                            var item = CreateObject(itemType);
                            ReadItem(value, ref item, exceptionInfo);
                            object[] par = {item};
                            typeInfo.GetMethod("Add").Invoke(result, par);
                        }
                    }
                    else if (node.ContainsNode("item"))
                    {
                        foreach (var itemNode in node.GetNodes("item"))
                        {
                            var item = CreateObject(paramTypes[0]);
                            ReadNode(itemNode, ref item, exceptionInfo);
                            object[] par = {item};
                            typeInfo.GetMethod("Add").Invoke(result, par);
                        }
                    }
                }
                else if (typeInfo.IsDict(out paramTypes))
                {
                    var keyType = paramTypes[0];
                    var valueType = paramTypes[1];
                    typeInfo.GetMethod("Clear").Invoke(result, null);
                    foreach (var itemNode in node.GetNodes("item"))
                    {
                        var key = CreateObject(keyType);
                        var val = CreateObject(valueType);
                        var kvpType = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType).GetTypeInfo();
                        Read(itemNode, "key", ref key, kvpType.GetProperty("Key"));
                        Read(itemNode, "value", ref val, kvpType.GetProperty("Value"));
                        object[] par = {key, val};
                        typeInfo.GetMethod("Add").Invoke(result, par);
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
                var member = value.GetSerializeObjectSingleMember();
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
                value.GetSerializeObjectCustomSaveMethod()(node);
            else if (value.IsSerializeObjectSingle())
            {
                var member = value.GetSerializeObjectSingleMember();
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
    }
}