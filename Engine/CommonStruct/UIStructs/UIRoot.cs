using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Meow.Util.ADB.Engine.CommonStruct.UIStructs
{
    /// <summary>
    /// UI的集合父类
    /// </summary>
    public class UIRoot : IEnumerable<KeyValuePair<UINode, List<UINode>>>, IEquatable<UIRoot>
    {
        /// <summary>
        /// 本UI结构所在的设备
        /// </summary>
        public Device InstanceOfDevice { get; set; }
        /// <summary>
        /// 屏幕旋转方向
        /// </summary>
        public int Rotation { get; set; }
        /// <summary>
        /// 解析后的节点列表
        /// </summary>
        public Dictionary<UINode, List<UINode>> NodeList { get; } = new Dictionary<UINode, List<UINode>>();
        /// <summary>
        /// 实际UI的数据
        /// </summary>
        public JObject ActualData { get; }
        /// <summary>
        /// 根节点
        /// </summary>
        public UINode Root { get; }

        /// <summary>
        /// UI实例类
        /// </summary>
        /// <param name="jo">是标准的解析协议列表</param>
        /// <param name="dev">归属设备</param>
        public UIRoot(JObject jo, Device dev)
        {
            ActualData = jo;
            InstanceOfDevice = dev;
            Rotation = int.TryParse(jo["hierarchy"]["@rotation"].ToString(), out var index) ? index : 0;

            Queue<(JToken Node, int Depth, UINode Parent)> queue = new Queue<(JToken Node, int Depth, UINode Parent)>();
            queue.Enqueue((jo["hierarchy"]["node"], 0, default));
            Root = new UINode(jo["hierarchy"]["node"] as JObject, 0);

            while (queue.Count > 0)
            {
                var (node, depth, parent) = queue.Dequeue();
                if (node is JObject jn)
                {
                    UINode newNode = new UINode(jn, depth);

                    if (!NodeList.ContainsKey(newNode))
                    {
                        NodeList[newNode] = new List<UINode>();
                    }

                    if (!parent.Equals(default(UINode)))
                    {
                        NodeList[parent].Add(newNode);
                    }

                    if (jn.ContainsKey("node"))
                    {
                        if (jn["node"] is JArray _ja && _ja.Count > 0)
                        {
                            foreach (var i in _ja)
                            {
                                queue.Enqueue((i, depth + 1, newNode));
                            }
                        }
                        else if (jn["node"] is JObject _jn)
                        {
                            queue.Enqueue((_jn, depth + 1, newNode));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 根据节点一致性选择子节点(如果没有就返回空)
        /// </summary>
        /// <param name="node">比对节点</param>
        /// <returns></returns>
        public List<UINode> this[UINode node] => NodeList?[node] ?? new List<UINode>();
        /// <summary>
        /// 判定节点是否存在
        /// </summary>
        /// <param name="node">一致性节点</param>
        /// <returns></returns>
        public bool Contains(UINode node) => NodeList.ContainsKey(node);

        /// <summary>
        /// 选择含有某种文本的所有表项
        /// </summary>
        /// <param name="text">文本模式</param>
        /// <param name="_equals">是否完全匹配(Equals)</param>
        /// <returns></returns>
        public UINode[] SelectItemByText(string text, bool _equals = false)
        {
            List<UINode> result = new List<UINode>();
            foreach(var i in NodeList)
            {
                if (_equals)
                {
                    if (i.Key.Text.Trim().Equals(text.Trim()))
                    {
                        result.Add(i.Key);
                    }
                }
                else
                {
                    if (i.Key.Text.Trim().Contains(text.Trim()))
                    {
                        result.Add(i.Key);
                    }
                }
            }
            return result.ToArray();
        }
        /// <summary>
        /// (正则)选择含有某种文本的所有表项
        /// </summary>
        /// <param name="pattern">文本模式</param>
        /// <returns></returns>
        public UINode[] SelectItemByText(Regex pattern)
        {
            List<UINode> result = new List<UINode>();
            foreach (var i in NodeList)
            {
                if (pattern.IsMatch(i.Key.Text))
                {
                    result.Add(i.Key);
                }
            }
            return result.ToArray();
        }
        /// <summary>
        /// 选择含有某种文本的第一个表项
        /// </summary>
        /// <param name="text">文本模式</param>
        /// <param name="_equals">是否完全匹配(Equals)</param>
        /// <returns></returns>
        public UINode? SelectFirstItemByText(string text, bool _equals = false)
        {
            foreach (var i in NodeList)
            {
                if (_equals)
                {
                    if (i.Key.Text.Trim().Equals(text.Trim()))
                    {
                        return i.Key;
                    }
                }
                else
                {
                    if (i.Key.Text.Trim().Contains(text.Trim()))
                    {
                        return i.Key;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// (正则)选择含有某种文本的所有表项
        /// </summary>
        /// <param name="pattern">文本模式</param>
        /// <returns></returns>
        public UINode? SelectFirstItemByText(Regex pattern)
        {
            foreach (var i in NodeList)
            {
                if (pattern.IsMatch(i.Key.Text))
                {
                    return i.Key;
                }
            }
            return null;
        }




        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Stack<(UINode Node, int Depth)> stack = new Stack<(UINode Node, int Depth)>();
            stack.Push((Root, 0));

            while (stack.Count > 0)
            {
                var (node, depth) = stack.Pop();

                // 添加当前节点到StringBuilder
                string prefix = new string(' ', depth * 2);
                sb.AppendLine(prefix + node);

                // 如果当前节点有子节点，将子节点添加到栈中
                if (NodeList.ContainsKey(node))
                {
                    var children = NodeList[node];
                    for (int i = children.Count - 1; i >= 0; i--)
                    {
                        stack.Push((children[i], depth + 1));
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => ActualData.ToString().GetHashCode();
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public bool Equals(UIRoot other) => JToken.DeepEquals(ActualData, other.ActualData);
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<UINode, List<UINode>>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<UINode, List<UINode>>>)NodeList).GetEnumerator();
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)NodeList).GetEnumerator();
        }
    }
}
