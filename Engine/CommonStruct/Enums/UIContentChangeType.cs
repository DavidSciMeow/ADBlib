using System;

namespace Meow.Util.ADB.Engine.CommonStruct.Enums
{
    /// <summary>
    /// 表示不同类型的UI内容更改的枚举。
    /// </summary>
    [Flags]
    public enum UIContentChangeType
    {
        /// <summary>
        /// 内容描述的更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_CONTENT_DESCRIPTION = 4,

        /// <summary>
        /// 无效的内容更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_CONTENT_INVALID = 1024,

        /// <summary>
        /// 拖动操作取消。
        /// </summary>
        CONTENT_CHANGE_TYPE_DRAG_CANCELLED = 512,

        /// <summary>
        /// 拖动操作放下。
        /// </summary>
        CONTENT_CHANGE_TYPE_DRAG_DROPPED = 256,

        /// <summary>
        /// 开始拖动操作。
        /// </summary>
        CONTENT_CHANGE_TYPE_DRAG_STARTED = 128,

        /// <summary>
        /// 启用状态的更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_ENABLED = 4096,

        /// <summary>
        /// 内容更改错误。
        /// </summary>
        CONTENT_CHANGE_TYPE_ERROR = 2048,

        /// <summary>
        /// UI中出现的窗格。
        /// </summary>
        CONTENT_CHANGE_TYPE_PANE_APPEARED = 16,

        /// <summary>
        /// UI中消失的窗格。
        /// </summary>
        CONTENT_CHANGE_TYPE_PANE_DISAPPEARED = 32,

        /// <summary>
        /// 窗格标题的更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_PANE_TITLE = 8,

        /// <summary>
        /// 状态描述的更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_STATE_DESCRIPTION = 64,

        /// <summary>
        /// 子树的更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_SUBTREE = 1,

        /// <summary>
        /// 文本的更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_TEXT = 2,

        /// <summary>
        /// 未定义的内容更改。
        /// </summary>
        CONTENT_CHANGE_TYPE_UNDEFINED = 0,

        /// <summary>
        /// 无效的位置。
        /// </summary>
        INVALID_POSITION = -1,

        /// <summary>
        /// 最大文本长度。此字段已过时。
        /// </summary>
        [Obsolete]
        MAX_TEXT_LENGTH = 500,
    }
}