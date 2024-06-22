using Meow.Util.ADB.Engine.CommandEnums;
using Meow.Util.ADB.Engine.CommonStruct.UIStructs;
using Meow.Util.ADB.Engine.Enums;
using System;

namespace Meow.Util.ADB.Engine.CommonStruct
{
    public partial class Device : IDisposable
    {
        /// <summary>
        /// 是否可以使用ADB键盘输入(需要安装com.android.adbkeyboard)
        /// </summary>
        public bool CanInputWithADBKeyboard { get; } = false;
        /// <summary>
        /// 朝当前焦点输入文本
        /// </summary>
        /// <param name="input"></param>
        public void InputToCurrentFocus(string input) => ExecuteProcess($"-s {DeviceID} shell am broadcast -a ADB_INPUT_TEXT --es msg '{input}'");
        /// <summary>
        /// 删除当前焦点文本(一个)
        /// </summary>
        public void InputDelCurrentFocus() => ExecuteShellCommand(ShellCommands.Keyevent(Key.KEYCODE_DEL));
        /// <summary>
        /// 删除当前焦点文本(若干)
        /// </summary>
        public void InputDelCurrentFocus(int length)
        {
            for (int i = 0; i < length; i++)
            {
                ExecuteShellCommand(ShellCommands.Keyevent(Key.KEYCODE_DEL));
            }
        }
    }
}
