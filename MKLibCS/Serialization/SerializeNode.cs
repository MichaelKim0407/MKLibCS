using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MKLibCS.Logging;
using MKLibCS.Collections;
using MKLibCS.TargetSpecific;

namespace MKLibCS.Serialization
{
    /// <summary>
    /// </summary>
    public sealed class SerializeNode
    {
        private static readonly Logger logger = new Logger(typeof(SerializeNode));

        private static readonly Encoding Encoding = Encoding.UTF8;

        /// <summary>
        /// </summary>
        public SerializeNode()
        {
            parent = null;
        }

        /// <summary>
        ///     Reads the file.
        /// </summary>
        /// <param name="path"></param>
        public SerializeNode(string path)
        {
            parent = null;
            ReadFile(path);
        }

        private SerializeNode(SerializeNode parent, string name)
        {
            this.parent = parent;
            this.name = name;
        }

        private readonly SerializeNode parent;

        private readonly string name = "@root";

        /// <summary>
        /// </summary>
        public string Name => parent == null ? name : parent.Name + "." + name;

        #region Content

        #region ItemOrNode

        /// <summary>
        /// </summary>
        public interface IItemOrNode
        {
            /// <summary>
            /// </summary>
            string Key { get; }

            /// <summary>
            /// </summary>
            object Value { get; }
        }

        /// <summary>
        /// </summary>
        public IEnumerable<IItemOrNode> ItemsAndNodes
        {
            get
            {
                foreach (var item in items)
                    yield return item;
                foreach (var node in nodes)
                    yield return node;
            }
        }

        #endregion

        #region Item

        /// <summary>
        /// </summary>
        public sealed class Item : IItemOrNode
        {
            /// <exception cref="System.ArgumentNullException"></exception>
            /// <exception cref="System.ArgumentException"></exception>
            public Item(string key, string value)
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (key == "")
                    throw new ArgumentException(nameof(key) + " cannot be an empty string");
                if (value == "")
                    throw new ArgumentException(nameof(value) + " cannot be an empty string");
                this.key = key;
                this.value = value;
            }

            /// <summary>
            /// </summary>
            public readonly string key;

            /// <summary>
            /// </summary>
            public readonly string value;

            /// <summary>
            /// </summary>
            public string Key => key;

            /// <summary>
            /// </summary>
            public object Value => value;

            /// <summary>
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return key.ToString() + ValueIs_Str + value.ToString();
            }
        }

        private sealed class ItemList
        {
            private List<Item> list = new List<Item>();

            public void Add(string key, string value)
            {
                list.Add(new Item(key, value));
            }

            public bool ContainsKey(string key)
            {
                return list.Any(i => i.key == key);
            }

            public int Count
            {
                get { return list.Count; }
            }

            public IEnumerator<Item> GetEnumerator()
            {
                return list.GetEnumerator();
            }

            /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
            public Item GetItem(string key)
            {
                if (ContainsKey(key))
                    return list.Find(i => i.key == key);
                else
                    throw new KeyNotFoundException();
            }

            /// <returns>Returns a list of items that has the given key. If no item is found, returns an empty list.</returns>
            public List<Item> GetItems(string key)
            {
                if (ContainsKey(key))
                    return list.FindAll(i => i.key == key);
                else
                    return new List<Item>();
            }

            public IEnumerable<Item> AsEnumerable()
            {
                return list;
            }
        }

        private ItemList items = new ItemList();

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddItem(string key, object value)
        {
            items.Add(key, value.ToString());
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsItem(string key)
        {
            return items.ContainsKey(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetItem(string key)
        {
            return items.GetItem(key).value;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<string> GetItems(string key)
        {
            return items.GetItems(key).ConvertAll(i => i.value);
        }

        /// <summary>
        /// </summary>
        public IEnumerable<Item> Items => items.AsEnumerable();

        #endregion

        #region Node

        /// <summary>
        /// </summary>
        public sealed class Node : IItemOrNode
        {
            /// <exception cref="System.ArgumentNullException"></exception>
            /// <exception cref="System.ArgumentException"></exception>
            public Node(string key, SerializeNode parent)
            {
                if (key == null)
                    throw new ArgumentNullException(nameof(key));
                if (parent == null)
                    throw new ArgumentNullException(nameof(parent));
                if (key == "")
                    throw new ArgumentException(nameof(key) + " cannot be an empty string");
                this.key = key;
                node = new SerializeNode(parent, key);
            }

            /// <summary>
            /// </summary>
            public readonly string key;

            /// <summary>
            /// </summary>
            public readonly SerializeNode node;

            /// <summary>
            /// </summary>
            public string Key => key;

            /// <summary>
            /// </summary>
            public object Value => node;

            /// <summary>
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return key + " { }";
            }
        }

        private sealed class NodeList
        {
            private List<Node> list = new List<Node>();

            public SerializeNode Add(string key, SerializeNode parent)
            {
                Node node = new Node(key, parent);
                list.Add(node);
                return node.node;
            }

            public bool ContainsKey(string key)
            {
                return list.Any(n => n.key == key);
            }

            public int Count
            {
                get { return list.Count; }
            }

            public IEnumerator<Node> GetEnumerator()
            {
                return list.GetEnumerator();
            }

            /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
            public Node GetNode(string key)
            {
                if (ContainsKey(key))
                    return list.Find(n => n.key == key);
                else
                    throw new KeyNotFoundException();
            }

            /// <returns>Returns a list of items that has the given key. If no item is found, returns an empty list.</returns>
            public List<Node> GetNodes(string key)
            {
                if (ContainsKey(key))
                    return list.FindAll(n => n.key == key);
                else
                    return new List<Node>();
            }

            public Node Last()
            {
                {
                    return list.Last();
                }
            }

            public IEnumerable<Node> AsEnumerable()
            {
                return list;
            }
        }

        private NodeList nodes = new NodeList();

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SerializeNode AddNode(string key)
        {
            return nodes.Add(key, this);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsNode(string key)
        {
            return nodes.ContainsKey(key);
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public SerializeNode GetNode(string key)
        {
            return nodes.GetNode(key).node;
        }

        /// <summary>
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<SerializeNode> GetNodes(string key)
        {
            return nodes.GetNodes(key).ConvertAll(n => n.node);
        }

        /// <summary>
        /// </summary>
        public IEnumerable<Node> Nodes => nodes.AsEnumerable();

        #endregion

        #endregion

        #region IO

        private const string NodeBegin = "{";
        private const string NodeEnd = "}";
        private const char ValueIs = '=';
        private const string ValueIs_Str = " = ";
        private const string Comment = "//";
        private const string Space = " ";
        private const string Tab = "\t";

        #region Read

        private enum LastReadAction
        {
            NONE,
            NEWNODE,
            STARTNODE,
            ENDNODE,
            ITEM
        }

        private LastReadAction lastReadAction;

        /// <exception cref="CorruptFileException"></exception>
        private void Read(StreamReader reader, int level, ref int nLine)
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                nLine++;

                if (line.Contains(Comment))
                    line = line.Remove(line.IndexOf(Comment));
                line = line.Replace(Tab, string.Empty);
                string oldline = line;
                line = line.Replace(Space, string.Empty);

                if (line == string.Empty)
                    continue;

                if (lastReadAction == LastReadAction.NEWNODE && line != NodeBegin)
                    throw new CorruptFileException(nLine);

                if (line == NodeBegin)
                {
                    if (lastReadAction != LastReadAction.NEWNODE)
                        throw new CorruptFileException(nLine);
                    nodes.Last().node.Read(reader, level + 1, ref nLine);
                    lastReadAction = LastReadAction.STARTNODE;
                }
                else if (line == NodeEnd)
                {
                    if (level == 0)
                        throw new CorruptFileException(nLine);
                    lastReadAction = LastReadAction.ENDNODE;
                    return;
                }
                else if (line.ToCharArray().Contains(ValueIs))
                {
                    var splitIndex = oldline.IndexOf(ValueIs);
                    var key = oldline.Substring(0, splitIndex);
                    var value = oldline.Substring(splitIndex + 1);
                    while (key.EndsWith(Space))
                        key = key.Remove(key.Length - 1);
                    while (value.StartsWith(Space))
                        value = value.Remove(0, 1);
                    if (key == "" || value == "")
                        throw new CorruptFileException(nLine);
                    logger.InternalDebug("In node \"{0}\": new item \"{1}\" = {2}", Name, key, value);
                    items.Add(key, value);
                    lastReadAction = LastReadAction.ITEM;
                }
                else
                {
                    logger.InternalDebug("In node \"{0}\": new child node \"{1}\"", Name, line);
                    nodes.Add(line, new SerializeNode(this, line));
                    lastReadAction = LastReadAction.NEWNODE;
                }
            }
            if (level != 0)
                throw new CorruptFileException(nLine);
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        public void ReadFile(string path)
        {
            logger.InternalDebug("Opening file: \"{0}\"", path);
            StreamReader reader = TargetSpecificUtil.StreamReader.Do(path, Encoding) as StreamReader;
            // StreamReader reader = new StreamReader(path, Encoding.UTF8);
            lastReadAction = LastReadAction.NONE;
            int nLine = 0;
            logger.InternalDebug("Reading file: \"{0}\"", path);
            Read(reader, 0, ref nLine);
            logger.InternalDebug("File read: \"{0}\", {1} line(s)", path, nLine);
            reader.Dispose();
        }

        #endregion

        #region Write

        private void Write(TextWriter writer, int level)
        {
            string tabs = string.Empty;
            for (int l = 0; l < level; l++)
                tabs += Tab;
            foreach (Item item in items)
                writer.WriteLine(tabs + item);
            foreach (Node node in nodes)
            {
                writer.WriteLine(tabs + node.key);
                writer.WriteLine(tabs + NodeBegin);
                node.node.Write(writer, level + 1);
                writer.WriteLine(tabs + NodeEnd);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="path"></param>
        public void WriteFile(string path)
        {
            logger.InternalDebug("Creating file: \"{0}\"", path);
            StreamWriter writer = TargetSpecificUtil.StreamWriter.Do(path, false, Encoding) as StreamWriter;
            // StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
            logger.InternalDebug("Writing file: \"{0}\"", path);
            Write(writer, 0);
            logger.InternalDebug("File written: \"{0}\"", path);
            writer.Dispose();
        }

        #endregion

        #endregion

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringWriter writer = new StringWriter();
            Write(writer, 0);
            return writer.GetStringBuilder().ToString();
        }
    }
}