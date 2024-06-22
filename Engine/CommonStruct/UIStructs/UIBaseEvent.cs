using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Meow.Util.ADB.Engine.CommonStruct.Enums;

namespace Meow.Util.ADB.Engine.CommonStruct.UIStructs
{
    /// <summary>
    /// 表示Android应用中的用户界面事件。
    /// </summary>
    public readonly struct UIBaseEvent
    {
        /// <summary>
        /// 获取或设置事件的类型。
        /// </summary>
        public UIEventType EventType { get; }

        /// <summary>
        /// 获取或设置事件发生的时间。(自开机)
        /// </summary>
        public TimeSpan EventTime { get; }

        /// <summary>
        /// 获取或设置事件发生的时间。
        /// </summary>
        public DateTime EventOccurTime { get; }

        /// <summary>
        /// 获取或设置发生事件的应用的包名。
        /// </summary>
        public string PackageName { get; }

        /// <summary>
        /// 获取或设置事件的移动粒度。
        /// </summary>
        public int MovementGranularity { get; }

        /// <summary>
        /// 获取或设置与事件关联的动作。
        /// </summary>
        public int Action { get; }

        /// <summary>
        /// 获取或设置事件中的内容更改类型。
        /// </summary>
        public UIContentChangeType ContentChangeTypes { get; }

        /// <summary>
        /// 获取或设置事件中的窗口更改类型。
        /// </summary>
        public UIWindowsChangeType WindowChangeTypes { get; }

        /// <summary>
        /// 获取或设置事件源的类名。
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// 获取或设置与事件关联的文本。
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// 获取或设置事件的内容描述。
        /// </summary>
        public string ContentDescription { get; }

        /// <summary>
        /// 获取或设置与事件关联的项目数量。
        /// </summary>
        public int ItemCount { get; }

        /// <summary>
        /// 获取或设置与事件关联的当前项目索引。
        /// </summary>
        public int CurrentItemIndex { get; }

        /// <summary>
        /// 获取或设置一个值，该值指示事件源是否已启用。
        /// </summary>
        public bool Enabled { get; }

        /// <summary>
        /// 获取或设置一个值，该值指示事件源是否为密码字段。
        /// </summary>
        public bool Password { get; }

        /// <summary>
        /// 获取或设置一个值，该值指示事件源是否已选中。
        /// </summary>
        public bool Checked { get; }

        /// <summary>
        /// 获取或设置一个值，该值指示事件源是否处于全屏模式。
        /// </summary>
        public bool FullScreen { get; }

        /// <summary>
        /// 获取或设置一个值，该值指示事件源是否可滚动。
        /// </summary>
        public bool Scrollable { get; }

        /// <summary>
        /// 获取或设置事件发生前的文本。
        /// </summary>
        public string BeforeText { get; }

        /// <summary>
        /// 获取或设置事件中更改的开始索引。
        /// </summary>
        public int FromIndex { get; }

        /// <summary>
        /// 获取或设置事件中更改的结束索引。
        /// </summary>
        public int ToIndex { get; }

        /// <summary>
        /// 获取或设置事件的滚动X位置。
        /// </summary>
        public int ScrollX { get; }

        /// <summary>
        /// 获取或设置事件的滚动Y位置。
        /// </summary>
        public int ScrollY { get; }

        /// <summary>
        /// 获取或设置事件的最大滚动X位置。
        /// </summary>
        public int MaxScrollX { get; }

        /// <summary>
        /// 获取或设置事件的最大滚动Y位置。
        /// </summary>
        public int MaxScrollY { get; }

        /// <summary>
        /// 获取或设置事件的滚动delta X。
        /// </summary>
        public int ScrollDeltaX { get; }

        /// <summary>
        /// 获取或设置事件的滚动delta Y。
        /// </summary>
        public int ScrollDeltaY { get; }

        /// <summary>
        /// 获取或设置事件中添加的项目数量。
        /// </summary>
        public int AddedCount { get; }

        /// <summary>
        /// 获取或设置事件中移除的项目数量。
        /// </summary>
        public int RemovedCount { get; }

        /// <summary>
        /// 获取或设置与事件关联的可分发数据。
        /// </summary>
        public string ParcelableData { get; }

        /// <summary>
        /// 获取或设置事件的记录数量。
        /// </summary>
        public int RecordCount { get; }

        /// <summary>
        /// UI事件基类
        /// </summary>
        /// <param name="data">数据</param>
        public UIBaseEvent(string data)
        {
            EventType = UIEventType.TYPE_UNSPEC;
            EventOccurTime = DateTime.Now;
            EventTime = TimeSpan.Zero;
            PackageName = string.Empty;
            MovementGranularity = -1;
            Action = -1;
            ContentChangeTypes = UIContentChangeType.CONTENT_CHANGE_TYPE_UNDEFINED;
            WindowChangeTypes = UIWindowsChangeType.WINDOWS_UNSPEC;
            ClassName = string.Empty;
            Text = string.Empty;
            ContentDescription = string.Empty;
            ItemCount = -1;
            CurrentItemIndex = -1;
            Enabled = false;
            Password = false;
            Checked = false;
            FullScreen = false;
            Scrollable = false;
            BeforeText = string.Empty;
            FromIndex = -1;
            ToIndex = -1;
            ScrollX = -1;
            ScrollY = -1;
            MaxScrollX = -1;
            MaxScrollY = -1;
            ScrollDeltaX = -1;
            ScrollDeltaY = -1;
            AddedCount = -1;
            RemovedCount = -1;
            ParcelableData = string.Empty;
            RecordCount = -1;

            if (data != null)
            {
                var properties = data.Substring(19).Split(';');
                var parsedLog = new Dictionary<string, string>();
                foreach (var prop in properties)
                {
                    var keyValuePair = prop.Split(':');
                    if (keyValuePair.Length == 2)
                    {
                        var key = keyValuePair[0].Trim();
                        var value = keyValuePair[1].Trim();
                        if (value.StartsWith("[") && value.EndsWith("]"))
                        {
                            value = value.Trim('[', ']');
                        }
                        parsedLog[key] = value;
                    }
                }

                if (parsedLog.ContainsKey(nameof(EventType)) && Enum.TryParse<UIEventType>(parsedLog[nameof(EventType)], out var _et)) EventType = _et;
                if (parsedLog.ContainsKey(nameof(EventOccurTime)) && DateTime.TryParseExact(data.Substring(0, 19).Trim(), "MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _dateTime)) EventOccurTime = _dateTime;
                if (parsedLog.ContainsKey(nameof(EventTime)) && long.TryParse(parsedLog[nameof(EventTime)], out var _ett)) EventTime = TimeSpan.FromMilliseconds(_ett);
                if (parsedLog.ContainsKey(nameof(PackageName))) PackageName = parsedLog[nameof(PackageName)];
                if (parsedLog.ContainsKey(nameof(MovementGranularity)) && int.TryParse(parsedLog[nameof(MovementGranularity)], out var _mgl)) MovementGranularity = _mgl;
                if (parsedLog.ContainsKey(nameof(Action)) && int.TryParse(parsedLog[nameof(Action)], out var _a)) Action = _a;
                if (parsedLog.ContainsKey(nameof(ContentChangeTypes)) && Enum.TryParse<UIContentChangeType>(parsedLog[nameof(ContentChangeTypes)], out var _cct)) ContentChangeTypes = _cct;
                if (parsedLog.ContainsKey(nameof(WindowChangeTypes)) && Enum.TryParse<UIWindowsChangeType>(parsedLog[nameof(WindowChangeTypes)], out var _wct)) WindowChangeTypes = _wct;
                if (parsedLog.ContainsKey(nameof(ClassName))) ClassName = parsedLog[nameof(ClassName)];
                if (parsedLog.ContainsKey(nameof(Text))) Text = parsedLog[nameof(Text)];
                if (parsedLog.ContainsKey(nameof(ContentDescription))) ContentDescription = parsedLog[nameof(ContentDescription)];
                if (parsedLog.ContainsKey(nameof(ItemCount)) && int.TryParse(parsedLog[nameof(ItemCount)], out var _ic)) ItemCount = _ic;
                if (parsedLog.ContainsKey(nameof(CurrentItemIndex)) && int.TryParse(parsedLog[nameof(CurrentItemIndex)], out var _cii)) CurrentItemIndex = _cii;
                if (parsedLog.ContainsKey(nameof(Enabled))) Enabled = parsedLog[nameof(Enabled)].ToLowerInvariant().Equals("true");
                if (parsedLog.ContainsKey(nameof(Password))) Password = parsedLog[nameof(Password)].ToLowerInvariant().Equals("true");
                if (parsedLog.ContainsKey(nameof(Checked))) Checked = parsedLog[nameof(Checked)].ToLowerInvariant().Equals("true");
                if (parsedLog.ContainsKey(nameof(FullScreen))) FullScreen = parsedLog[nameof(FullScreen)].ToLowerInvariant().Equals("true");
                if (parsedLog.ContainsKey(nameof(Scrollable))) Scrollable = parsedLog[nameof(Scrollable)].ToLowerInvariant().Equals("true");
                if (parsedLog.ContainsKey(nameof(BeforeText))) BeforeText = parsedLog[nameof(BeforeText)];
                if (parsedLog.ContainsKey(nameof(FromIndex)) && int.TryParse(parsedLog[nameof(FromIndex)], out var _fi)) FromIndex = _fi;
                if (parsedLog.ContainsKey(nameof(ToIndex)) && int.TryParse(parsedLog[nameof(ToIndex)], out var _ti)) FromIndex = _ti;
                if (parsedLog.ContainsKey(nameof(ScrollX)) && int.TryParse(parsedLog[nameof(ScrollX)], out var _sx)) ScrollX = _sx;
                if (parsedLog.ContainsKey(nameof(ScrollY)) && int.TryParse(parsedLog[nameof(ScrollY)], out var _sy)) ScrollY = _sy;
                if (parsedLog.ContainsKey(nameof(MaxScrollX)) && int.TryParse(parsedLog[nameof(MaxScrollX)], out var _msx)) MaxScrollX = _msx;
                if (parsedLog.ContainsKey(nameof(MaxScrollY)) && int.TryParse(parsedLog[nameof(MaxScrollY)], out var _msy)) MaxScrollY = _msy;
                if (parsedLog.ContainsKey(nameof(ScrollDeltaX)) && int.TryParse(parsedLog[nameof(ScrollDeltaX)], out var _sdx)) ScrollDeltaX = _sdx;
                if (parsedLog.ContainsKey(nameof(ScrollDeltaY)) && int.TryParse(parsedLog[nameof(ScrollDeltaY)], out var _sdy)) ScrollDeltaY = _sdy;
                if (parsedLog.ContainsKey(nameof(AddedCount)) && int.TryParse(parsedLog[nameof(AddedCount)], out var _ac)) AddedCount = _ac;
                if (parsedLog.ContainsKey(nameof(RemovedCount)) && int.TryParse(parsedLog[nameof(RemovedCount)], out var _rc)) RemovedCount = _rc;
                if (parsedLog.ContainsKey(nameof(ParcelableData))) ParcelableData = parsedLog[nameof(ParcelableData)].Replace("]","").Replace("[","");
                if (parsedLog.ContainsKey(nameof(RecordCount)) && int.TryParse(parsedLog[nameof(RecordCount)], out var _rc2)) RecordCount = _rc2;

            }
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if(EventType != UIEventType.TYPE_UNSPEC)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{EventOccurTime:MM/dd hh:mm:ss:fffff} EventType:{EventType, -33} \t [{PackageName}]");
                if (ContentChangeTypes != UIContentChangeType.CONTENT_CHANGE_TYPE_UNDEFINED) sb.Append($"\tInnerCType:{ContentChangeTypes}");
                if (WindowChangeTypes != UIWindowsChangeType.WINDOWS_UNSPEC) sb.Append($"\tInnerWType:{WindowChangeTypes}");
                sb.AppendLine($"{Text}");
                return sb.ToString();
            }
            return $"{DateTime.Now:MM/dd hh:mm:ss:fffff} Invalid Operation";
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return EventOccurTime.GetHashCode() ^ EventType.GetHashCode();
        }
    }
}
