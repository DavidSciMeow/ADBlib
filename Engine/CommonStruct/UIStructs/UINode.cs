using Newtonsoft.Json.Linq;

namespace Meow.Util.ADB.Engine.CommonStruct.UIStructs
{
    /// <summary>
    /// UI节点的结构
    /// </summary>
    public readonly struct UINode
    {
        /// <summary>
        /// 本框的深度
        /// </summary>
        public long Depth { get; }
        /// <summary>
        /// 本框在当前级别的索引
        /// </summary>
        public long Index { get; }
        /// <summary>
        /// 框上文字
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// 资源的ID号
        /// </summary>
        public string Resource_id { get; }
        /// <summary>
        /// 资源的类
        /// </summary>
        public string Class { get; }
        /// <summary>
        /// 包名
        /// </summary>
        public string Package { get; }
        /// <summary>
        /// 内部描述
        /// </summary>
        public string Content_desc { get; }
        /// <summary>
        /// 是否可以被选中
        /// </summary>
        public bool Checkable { get; }
        /// <summary>
        /// 是否已被选中
        /// </summary>
        public bool Checked { get; }
        /// <summary>
        /// 是否可以被点击
        /// </summary>
        public bool Clickable { get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; }
        /// <summary>
        /// 是否可以获得焦点
        /// </summary>
        public bool Focusable { get; }
        /// <summary>
        /// 是否已获得焦点
        /// </summary>
        public bool Focused { get; }
        /// <summary>
        /// 元素是否可以滚动
        /// </summary>
        public bool Scrollable { get; }
        /// <summary>
        /// 是否可以长按
        /// </summary>
        public bool Longclickable { get; }
        /// <summary>
        /// 是否是密码字段
        /// </summary>
        public bool Password { get; }
        /// <summary>
        /// 是否已被选中
        /// </summary>
        public bool Selected { get; }
        /// <summary>
        /// 位置的左上角标和右下角标
        /// </summary>
        public (long X1, long Y1, long X2, long Y2) Bounds { get;}

        /// <summary>
        /// 获取元素可点击的中心点
        /// </summary>
        public (long X, long Y) ClickCenter => ((Bounds.X1 + Bounds.X2) / 2, (Bounds.Y1 + Bounds.Y2) / 2);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{Bounds.X1,-4},{Bounds.Y1,-4}] [{Bounds.X2,-4},{Bounds.X2,-4}] [{Depth,-3},{Index,-3}] \t" +
                //$"{(Checkable ? "Y" : "N")}" +
                //$"{(Checked ? "Y" : "N")}"+
                //$"{(Clickable ? "Y" : "N")}" +
                //$"{(Enabled ? "Y" : "N")}" +
                //$"{(Focusable ? "Y" : "N")}" +
                //$"{(Focused ? "Y" : "N")}" +
                //$"{(Scrollable ? "Y" : "N")}" +
                //$"{(Longclickable ? "Y" : "N")}" +
                //$"{(Password ? "Y" : "N")}" +
                //$"{(Selected ? "Y" : "N")}" +
                $" \t {(Text.Trim().Equals(Content_desc.Trim()) && !string.IsNullOrWhiteSpace(Text) && !string.IsNullOrWhiteSpace(Content_desc) ? $"{Text}" : $"{Text}:[{Content_desc}]")}";
        }

        /// <summary>
        /// 实例化一个UINode的结构
        /// </summary>
        /// <param name="jo">UINode原生类</param>
        /// <param name="depth">UINode深度</param>
        public UINode(JObject jo, int depth)
        {
            Depth = depth;
            Index = int.TryParse(jo["@index"].ToString(), out var index) ? index : -1;
            Text = jo["@text"].ToString();
            Resource_id = jo["@resource-id"].ToString();
            Class = jo["@class"].ToString();
            Package = jo["@package"].ToString();
            Content_desc = jo["@content-desc"].ToString();
            Checkable = bool.TryParse(jo["@checkable"].ToString(), out var checkable) && checkable;
            Checked = bool.TryParse(jo["@checked"].ToString(), out var @checked) && @checked;
            Clickable = bool.TryParse(jo["@clickable"].ToString(), out var clickable) && clickable;
            Enabled = bool.TryParse(jo["@enabled"].ToString(), out var enabled) && enabled;
            Focusable = bool.TryParse(jo["@focusable"].ToString(), out var focusable) && focusable;
            Focused = bool.TryParse(jo["@focused"].ToString(), out var focused) && focused;
            Scrollable = bool.TryParse(jo["@scrollable"].ToString(), out var scrollable) && scrollable;
            Longclickable = bool.TryParse(jo["@long-clickable"].ToString(), out var longclickable) && longclickable;
            Password = bool.TryParse(jo["@password"].ToString(), out var password) && password;
            Selected = bool.TryParse(jo["@selected"].ToString(), out var selected) && selected;
            var pd = jo["@bounds"].ToString().Replace("][", ",").Replace("[", "").Replace("]", "").Trim().Split(',');
            if (pd.Length == 4)
            {
                long.TryParse(pd[0], out var x1);
                long.TryParse(pd[1], out var y1);
                long.TryParse(pd[2], out var x2);
                long.TryParse(pd[3], out var y2);
                Bounds = (x1, y1, x2, y2);
            }
            else
            {
                Bounds = (-1, -1, -1, -1);
            }
        }
    }
}
