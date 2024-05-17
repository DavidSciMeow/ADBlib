using Meow.Util.ADB.Engine.CommandEnums;
using System;

namespace Meow.Util.ADB.Engine.CommonStruct
{
    public partial class Device : IDisposable
    {
        /// <summary>
        /// 设备点击屏幕 x,y
        /// </summary>
        /// <param name="x">x 坐标</param>
        /// <param name="y">y 坐标</param>
        public void Tap(long x, long y) => ADBEngine.DeviceExecute(DeviceID, ShellCommands.Tap(x, y));
        /// <summary>
        /// 设备点击屏幕 x,y
        /// </summary>
        /// <param name="pos">坐标元组</param>
        public void Tap((long x, long y) pos) => ADBEngine.DeviceExecute(DeviceID, ShellCommands.Tap(pos.x, pos.y));
    }
}
