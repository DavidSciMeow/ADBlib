using Meow.Util.ADB.Engine.CommandEnums;
using Meow.Util.ADB.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Meow.Util.ADB.Engine.CommonStruct
{
    /// <summary>
    /// 安卓设备类
    /// </summary>
    public partial class Device : IDisposable
    {
        /// <summary>
        /// 设备名
        /// </summary>
        public string DeviceID { get; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public DeviceState State { get; }
        /// <summary>
        /// 设备安装的所有包
        /// </summary>
        public List<string> DevicePackage { get; } = new List<string>();
        /// <summary>
        /// 是否可以使用ADB键盘输入(需要安装com.android.adbkeyboard)
        /// </summary>
        public bool CanInputWithADBKeyboard { get; } = false;
        /// <summary>
        /// UI的状态监控
        /// </summary>
        public bool DeviceUIStateMonitor { get; private set; } = true;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="state">设备状态</param>
        /// <param name="deviceUIStateMonitor">UI的状态监控</param>
        public Device(string deviceID, DeviceState state, bool deviceUIStateMonitor = true)
        {
            DeviceID = deviceID;
            State = state;
            DeviceUIStateMonitor = deviceUIStateMonitor;
            var pklist = ExecuteShellProcess(ShellCommands.PackageList()).Split(new[] { "\r\n", "\r", "\n" },StringSplitOptions.None);
            foreach(var i in pklist)
            {
                if(i.Contains("package:"))
                {
                    DevicePackage.Add(i.Replace("package:", ""));
                    if (i.Equals("package:com.android.adbkeyboard"))
                    {
                        CanInputWithADBKeyboard = true;
                    }
                }
            }
            ExecuteUIShellCommand();


        }

        /// <summary>
        /// UI的事件进程
        /// </summary>
        private Process DeviceUIEventProcess { get; set; }

        /// <summary>
        /// 监控UI调用
        /// </summary>
        public void ExecuteUIShellCommand()
        {
            DeviceUIEventProcess = Process.Start(new ProcessStartInfo
            {
                FileName = "./Tools/adb.exe",
                Arguments = $"-s {DeviceID} shell uiautomator events",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            });
            DeviceUIEventProcess.OutputDataReceived += (sender, e) => DeviceUIEvent?.Invoke(e.Data);
            DeviceUIEventProcess.Start();
            DeviceUIEventProcess.BeginOutputReadLine();
            DeviceUIEventProcess.Exited += (d, e) => {
                if (DeviceUIStateMonitor)
                {
                    Console.WriteLine("正在重启UI监控");
                    ExecuteUIShellCommand();
                }
                else
                {
                    Console.WriteLine("已经退出UI监控");
                }
            };
        }
        /// <summary>
        /// 强制关闭监控UI调用
        /// </summary>
        public void ExecuteUIShellCommandClose() => DeviceUIEventProcess.Close();

        /// <summary>
        /// 在主线程执行命令并等待(默认等待1秒)
        /// </summary>
        /// <param name="command">命令字符串</param>
        /// <param name="milisecWait">等待间隔</param>
        public void ExecuteShellCommand(string command, int milisecWait = 1000)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "./Tools/adb.exe",
                Arguments = $"-s {DeviceID} shell {command}",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            });
            if (milisecWait > 0)
            {
                Task.Delay(milisecWait).Wait();
            }
        }
        /// <summary>
        /// 执行adb内函数进程
        /// </summary>
        /// <param name="arguments">参数列</param>
        /// <returns></returns>
        private string ExecuteProcess(string arguments)
        {
            using (var proc = Process.Start(new ProcessStartInfo
            {
                FileName = "./Tools/adb.exe",
                Arguments = arguments,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }))
            {
                var read = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return read;
            }
        }

        /// <summary>
        /// 执行shell进程并获取结果
        /// </summary>
        /// <param name="arguments">参数列</param>
        /// <returns></returns>
        private string ExecuteShellProcess(string arguments)
        {
            using (var proc = Process.Start(new ProcessStartInfo
            {
                FileName = "./Tools/adb.exe",
                Arguments = $"-s {DeviceID} shell {arguments}",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            }))
            {
                var read = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return read;
            }
        }

        /// <summary>
        /// 打印设备ID和设备状态
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{DeviceID} :: {State}";
        /// <summary>
        /// GC回收
        /// </summary>
        public void Dispose()
        {
            DeviceUIEventProcess?.Close();
            DeviceUIEventProcess?.Dispose();
        }
        /// <summary>
        /// 析构方法
        /// </summary>
        ~Device() => Dispose();
    }
}
