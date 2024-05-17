using System;

namespace Meow.Util.ADB.Engine.CommonStruct.Enums
{
    /// <summary>
    /// UI的WINDOWS_CHANGE内部事件类型
    /// </summary>
    [Flags]
    public enum UIWindowsChangeType
    {
        /// <summary>
        /// 窗体标题改变
        /// </summary>
        WINDOWS_CHANGE_TITLE = 4,
        /// <summary>
        /// 一个窗体被移除
        /// </summary>
        WINDOWS_CHANGE_REMOVED = 2,
        /// <summary>
        /// 窗体切换画中画模式
        /// </summary>
        WINDOWS_CHANGE_PIP = 1024,
        /// <summary>
        /// 窗体的父级改变
        /// </summary>
        WINDOWS_CHANGE_PARENT = 256,
        /// <summary>
        /// 窗体的层级改变
        /// </summary>
        WINDOWS_CHANGE_LAYER = 16,
        /// <summary>
        /// 窗体的焦点被改变
        /// </summary>
        WINDOWS_CHANGE_FOCUSED = 64,
        /// <summary>
        /// 窗体的子项改变
        /// </summary>
        WINDOWS_CHANGE_CHILDREN = 512,
        /// <summary>
        /// 窗体的边界变化
        /// </summary>
        WINDOWS_CHANGE_BOUNDS = 8,
        /// <summary>
        /// 窗体被添加
        /// </summary>
        WINDOWS_CHANGE_ADDED = 1,
        /// <summary>
        /// 窗体是不是被激活
        /// </summary>
        WINDOWS_CHANGE_ACTIVE = 32,
        /// <summary>
        /// 窗体被易用性功能激活
        /// </summary>
        WINDOWS_CHANGE_ACCESSIBILITY_FOCUSED = 128,
        /// <summary>
        /// 没标明的功能
        /// </summary>
        WINDOWS_UNSPEC = -1,
    }
}
