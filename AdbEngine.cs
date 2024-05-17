using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Meow.Util.ADB.Engine.CommonStruct;
using Meow.Util.ADB.Engine.Enums;

namespace Meow.Util.ADB
{
    /// <summary>
    /// ADB最佳引擎代管类
    /// </summary>
    public class ADBEngine : IDisposable
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        private bool disposedValue;
        /// <summary>
        /// 构造函数，创建ADBEngine实例。可选参数restart决定是否在实例创建时重启ADB服务。
        /// </summary>
        public ADBEngine(bool restart = false)
        {
            if (restart)
            {
                Console.WriteLine("正在重启 ADB Engine");
                Execute("kill-server");
            }
            else
            {
                Console.WriteLine("正在启动 ADB Engine");
            }
            Execute("start-server");
        }
        /// <summary>
        /// 执行ADB命令并返回输出结果。
        /// </summary>
        public static string Execute(string command)
        {
            using (var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "./Tools/adb.exe",
                    Arguments = command,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            })
            {
                process.Start();
                return process.StandardOutput.ReadToEnd();
            }
        }
        /// <summary>
        /// 在指定设备上执行ADB命令并返回输出结果。
        /// </summary>
        public static string DeviceExecute(string deviceID, string command) => Execute($"-s {deviceID} {command}");
        /// <summary>
        /// 启动ADB服务并返回输出结果。
        /// </summary>
        public string StartServer() => Execute("start-server");
        /// <summary>
        /// 停止ADB服务并返回输出结果。
        /// </summary>
        public string KillServer() => Execute("kill-server");
        /// <summary>
        /// 获取连接的设备列表。
        /// </summary>
        public Device[] GetDevices()
        {
            List<Device> Devices = new List<Device>();
            var lines = Execute("devices");
            foreach (var line in lines.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Skip(1))
            {
                var parts = line.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    Devices.Add(new Device(parts[0], Enum.TryParse<DeviceState>(parts[1].Replace(" ", "_"), out var rr) ? rr : throw new Exception("Parse Err")));
                }
            }
            return Devices.ToArray();
        }
        /// <summary>
        /// 释放ADBEngine实例，并可选地停止ADB服务。
        /// </summary>
        protected virtual void Dispose(bool disposing) => _ = (!disposedValue && disposing) ? (Execute("kill-server"), disposedValue = true) : ("", false);
        /// <summary>
        /// 析构函数，当ADBEngine实例被销毁时停止ADB服务。
        /// </summary>
        ~ADBEngine() => Execute("kill-server");
        /// <summary>
        /// 释放ADBEngine实例，并阻止垃圾回收器调用析构函数。
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
