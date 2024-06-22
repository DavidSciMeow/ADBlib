using System;

namespace Meow.Util.ADB.Engine.CommonStruct.Enums
{
    /// <summary>
    /// UI的事件类型
    /// </summary>
    [Flags]
    public enum UIEventType
    {
        /// <summary>
        /// 测试用空值
        /// </summary>
        TYPE_UNSPEC = 0,

        /// <summary>
        /// 表示点击视图的事件。
        /// </summary>
        TYPE_VIEW_CLICKED = 1,

        /// <summary>
        /// 表示视图被长按的事件。
        /// </summary>
        TYPE_VIEW_LONG_CLICKED = 2,

        /// <summary>
        /// 表示视图被选择的事件。
        /// </summary>
        TYPE_VIEW_SELECTED = 4,

        /// <summary>
        /// 表示视图聚焦的事件。
        /// </summary>
        TYPE_VIEW_FOCUSED = 8,

        /// <summary>
        /// 视图的内部文字发生改变的事件。
        /// </summary>
        TYPE_VIEW_TEXT_CHANGED = 16,

        /// <summary>
        /// 窗体状态发生改变的事件。
        /// </summary>
        TYPE_WINDOW_STATE_CHANGED = 32,

        /// <summary>
        /// 表示通知状态改变的事件。
        /// </summary>
        TYPE_NOTIFICATION_STATE_CHANGED = 64,

        /// <summary>
        /// 表示视图悬停进入的事件。
        /// </summary>
        TYPE_VIEW_HOVER_ENTER = 128,

        /// <summary>
        /// 表示视图悬停退出的事件。
        /// </summary>
        TYPE_VIEW_HOVER_EXIT = 256,

        /// <summary>
        /// 表示触摸探索手势开始的事件。
        /// </summary>
        TYPE_TOUCH_EXPLORATION_GESTURE_START = 512,

        /// <summary>
        /// 表示触摸探索手势结束的事件。
        /// </summary>
        TYPE_TOUCH_EXPLORATION_GESTURE_END = 1024,

        /// <summary>
        /// 视图正在滚动的事件。
        /// </summary>
        TYPE_VIEW_SCROLLED = 4096,

        /// <summary>
        /// 在文本视图中文本选择的改变的事件。
        /// </summary>
        TYPE_VIEW_TEXT_SELECTION_CHANGED = 8192,

        /// <summary>
        /// 表示公告的事件。
        /// </summary>
        TYPE_ANNOUNCEMENT = 16384,

        /// <summary>
        /// 表示视图辅助功能聚焦的事件。
        /// </summary>
        TYPE_VIEW_ACCESSIBILITY_FOCUSED = 32768,

        /// <summary>
        /// 表示清除辅助功能焦点的事件。
        /// </summary>
        TYPE_VIEW_ACCESSIBILITY_FOCUS_CLEARED = 65536,

        /// <summary>
        /// 表示手势检测开始的事件。
        /// </summary>
        TYPE_GESTURE_DETECTION_START = 262144,

        /// <summary>
        /// 表示手势检测结束的事件。
        /// </summary>
        TYPE_GESTURE_DETECTION_END = 524288,

        /// <summary>
        /// 窗体的内部内容发生改变的事件。
        /// </summary>
        TYPE_WINDOW_CONTENT_CHANGED = 2048,

        /// <summary>
        /// 在文本视图中以某种粒度移动或遍历文本的事件。
        /// </summary>
        TYPE_VIEW_TEXT_TRAVERSED_AT_MOVEMENT_GRANULARITY = 131072,

        /// <summary>
        /// 表示触摸交互开始的事件。
        /// </summary>
        TYPE_TOUCH_INTERACTION_START = 1048576,

        /// <summary>
        /// 表示触摸交互结束的事件。
        /// </summary>
        TYPE_TOUCH_INTERACTION_END = 2097152,

        /// <summary>
        /// 表示点击视图上下文的事件。
        /// </summary>
        TYPE_VIEW_CONTEXT_CLICKED = 8388608,

        /// <summary>
        /// 窗体发生改变的事件。
        /// </summary>
        TYPE_WINDOWS_CHANGED = 4194304,

        /// <summary>
        /// 表示辅助阅读上下文的事件。
        /// </summary>
        TYPE_ASSIST_READING_CONTEXT = 16777216,

        /// <summary>
        /// 表示语音状态改变的事件。
        /// </summary>
        TYPE_SPEECH_STATE_CHANGE = 33554432,

        /// <summary>
        /// 目标正在被滚动的事件。
        /// </summary>
        TYPE_VIEW_TARGETED_BY_SCROLL = 67108864,

        /// <summary>
        /// 表示所有类型的掩码。
        /// </summary>
        TYPES_ALL_MASK = -1
    }
}