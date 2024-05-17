namespace Meow.Util.ADB.Engine.CommonStruct
{
    /// <summary>
    /// CPU信息
    /// </summary>
    public struct CPUInfo
    {
        /// <summary>
        /// 一分钟平均负载
        /// </summary>
        public double AvgL1min { get; set; }
        /// <summary>
        /// 五分钟平均负载
        /// </summary>
        public double AvgL5min { get; set; }
        /// <summary>
        /// 十五分钟平均负载
        /// </summary>
        public double AvgL15min { get; set; }

        /// <summary>
        /// 采样期间负载
        /// </summary>
        public double Total { get; set; }
        /// <summary>
        /// 用户负载
        /// </summary>
        public double User { get; set; }
        /// <summary>
        /// 核心负载
        /// </summary>
        public double Kernel { get; set; }
        /// <summary>
        /// 等待IO负载
        /// </summary>
        public double IOWait { get; set; }
        /// <summary>
        /// 硬中断负载
        /// </summary>
        public double IRQ { get; set; }
        /// <summary>
        /// 软中断负载
        /// </summary>
        public double SoftIRQ { get; set; }
    }
}
