using Meow.Util.ADB.Engine.CommandEnums;
using Meow.Util.ADB.Engine.CommonStruct.UIStructs;
using Meow.Util.ADB.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
        /// 保证所有子程序均退出的PID列表
        /// </summary>
        private readonly List<int> PidList = new List<int>();
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
        /// 构造方法
        /// </summary>
        /// <param name="deviceID">设备ID</param>
        /// <param name="state">设备状态</param>
        /// <param name="deviceUIStateMonitor">UI的状态监控</param>
        public Device(string deviceID, DeviceState state, bool deviceUIStateMonitor = true)
        {
            DeviceID = deviceID;
            State = state;
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
        }


        /// <summary>
        /// 在主线程执行命令并等待(默认等待1秒)
        /// </summary>
        /// <param name="command">命令字符串</param>
        /// <param name="milisecWait">等待间隔</param>
        public void ExecuteShellCommand(string command, int milisecWait = 1000)
        {
            var p = Process.Start(new ProcessStartInfo
            {
                FileName = "./Tools/adb.exe",
                Arguments = $"-s {DeviceID} shell {command}",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            });
            PidList.Add(p.Id);
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
                PidList.Add(proc.Id);
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
                PidList.Add(proc.Id);
                var read = proc.StandardOutput.ReadToEnd();
                proc.WaitForExit();
                return read;
            }
        }

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
        /// <summary>
        /// 安装apk包
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool InstallApk(string path) => ExecuteProcess($"-s {DeviceID} {AdbCommands.InstallApk(path)}").ToLowerInvariant().Contains("success");
        /// <summary>
        /// 模拟按键
        /// </summary>
        /// <param name="key"></param>
        public void PressKey(Key key) => ADBEngine.DeviceExecute(DeviceID, ShellCommands.Keyevent(key));
        /// <summary>
        /// 模拟滑动屏幕
        /// </summary>
        /// <param name="x1">起始点x坐标</param>
        /// <param name="y1">起始点y坐标</param>
        /// <param name="x2">终止点x坐标</param>
        /// <param name="y2">终止点y坐标</param>
        /// <param name="duration">滑动时长</param>
        public void Swipe(int x1, int y1, int x2, int y2, int duration) => ADBEngine.DeviceExecute(DeviceID, ShellCommands.Swipe(x1, y1, x2, y2, duration));
        /// <summary>
        /// 截屏到某个具体路径
        /// </summary>
        /// <param name="path">路径位置</param>
        public void Screencap(string path) => ADBEngine.DeviceExecute(DeviceID, ShellCommands.Screencap(path));
        /// <summary>
        /// 拉取对应设备的文件
        /// </summary>
        /// <param name="path">远端路径</param>
        /// <param name="locpath">本地存储路径</param>
        public void Pull(string path, string locpath) => ADBEngine.DeviceExecute(DeviceID, AdbCommands.Pull(path, locpath));


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
            foreach(var i in PidList)
            {
                try
                {
                    Process.GetProcessById(i)?.Kill();
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// 析构方法
        /// </summary>
        ~Device() => Dispose();
    }
}
