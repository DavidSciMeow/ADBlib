namespace Meow.Util.ADB.Engine.CommonStruct
{
    /// <summary>
    /// 描述进程负载信息的结构体。
    /// </summary>
    public struct ProcessLoadInfo
    {
        /// <summary>
        /// 总CPU使用百分比。
        /// </summary>
        public double TotalCpuPercentage { get; set; }

        /// <summary>
        /// 进程ID。
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// 进程名称。
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 用户CPU使用百分比。
        /// </summary>
        public double UserCpuPercentage { get; set; }

        /// <summary>
        /// 内核CPU使用百分比。
        /// </summary>
        public double KernelCpuPercentage { get; set; }

        /// <summary>
        /// 次要错误数量。
        /// </summary>
        public int MinorFaults { get; set; }
    }
}
