namespace Meow.Util.ADB.Engine.Enums
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public enum DeviceState
    {
        /// <summary>
        /// 已连接
        /// </summary>
        device,
        /// <summary>
        /// 离线
        /// </summary>
        offline,
        /// <summary>
        /// 未授权
        /// </summary>
        unauthorised,
        /// <summary>
        /// 正在引导
        /// </summary>
        bootloader,
        /// <summary>
        /// 恢复模式
        /// </summary>
        recovery,
        /// <summary>
        /// 无设备
        /// </summary>
        no_device,
        /// <summary>
        /// 未知
        /// </summary>
        unknown
    }
}
