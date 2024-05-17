using System;

namespace Meow.Util.ADB.Engine.CommonStruct
{
    public partial class Device : IDisposable
    {
        /// <summary>
        /// 设备UI更新事件
        /// </summary>
        /// <param name="data">数据</param>
        public delegate void DeviceUIEventHandler(string data);
        /// <summary>
        /// 设备事件回调
        /// </summary>
        public event DeviceUIEventHandler DeviceUIEvent;
    }
}
