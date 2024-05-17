using Meow.Util.ADB.Engine.CommandEnums;
using Meow.Util.ADB.Engine.CommonStruct.UIStructs;
using Meow.Util.ADB.Engine.CommonStruct.WindowStruct;
using Meow.Util.ADB.Engine.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Meow.Util.ADB.Engine.CommonStruct
{
    public partial class Device : IDisposable
    {
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
                //Task.Delay(500).Wait();
            }
        }

        /// <summary>
        /// 获取当前设备的UI信息类(解析)
        /// </summary>
        /// <returns></returns>
        public UIRoot GetUI(bool Compressed = false) => new UIRoot(JObject.Parse(GetUIp(Compressed)), this);
        /// <summary>
        /// 获取当前设备的UI信息(原始)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetUIp(bool Compressed = false)
        {
            var lines = ExecuteProcess($"-s {DeviceID} shell uiautomator dump {(Compressed ? "--compressed" : "")}").ToLowerInvariant();
            while (true)
            {
                if (lines.Contains("UI hierchary dumped to".ToLowerInvariant()))
                {
                    var linex = ExecuteProcess($"-s {DeviceID} pull /sdcard/window_dump.xml ./temp.xml").ToLowerInvariant();
                    if (linex.Contains("pulled".ToLowerInvariant()))
                    {
                        var doc = new XmlDocument();
                        doc.Load("./temp.xml");
                        string jsonText = JsonConvert.SerializeXmlNode(doc);
                        File.Delete("./temp.xml");
                        return jsonText;
                    }
                    else
                    {
                        throw new Exception("Device Not Pullable");
                    }
                }
                else
                {
                    lines = ExecuteProcess($"-s {DeviceID} shell uiautomator dump {(Compressed ? "--compressed" : "")}").ToLowerInvariant();
                }
            }
            
        }

        /// <summary>
        /// 获取系统窗体信息 原始
        /// </summary>
        /// <returns></returns>
        public List<Dictionary<string, string>> GetWindowp()
        {
            var lines = ExecuteProcess($"-s {DeviceID} shell dumpsys window windows");
            var wi = WindowInfo.Parse(lines);
            return wi;
        }
        /// <summary>
        /// 读取CPU信息
        /// </summary>
        /// <returns></returns>
        public CPUInfo DumpCPU()
        {
            var lines = ExecuteProcess($"-s {DeviceID} shell dumpsys cpuinfo").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var cpuload = lines[0].Replace("Load:", "").Split('/');
            string pattern = @"(\d+(?:\.\d+)?)% TOTAL: (\d+(?:\.\d+)?)% user \+ (\d+(?:\.\d+)?)% kernel \+ (\d+(?:\.\d+)?)% iowait \+ (\d+(?:\.\d+)?)% irq \+ (\d+(?:\.\d+)?)% softirq";
            Match match = Regex.Match(lines[lines.Length - 1], pattern);
            if (match.Success)
            {
                return new CPUInfo
                {
                    AvgL1min = double.TryParse(cpuload[0].Trim(), out var _avgl1) ? _avgl1 : -1,
                    AvgL5min = double.TryParse(cpuload[1].Trim(), out var _avgl5) ? _avgl5 : -1,
                    AvgL15min = double.TryParse(cpuload[2].Trim(), out var _avgl15) ? _avgl15 : -1,
                    Total = double.TryParse(match.Groups[1].Value.Trim(), out var _a1) ? _a1 : -1,
                    User = double.TryParse(match.Groups[2].Value.Trim(), out var _a2) ? _a2 : -1,
                    Kernel = double.TryParse(match.Groups[3].Value.Trim(), out var _a3) ? _a3 : -1,
                    IOWait = double.TryParse(match.Groups[4].Value.Trim(), out var _a4) ? _a4 : -1,
                    IRQ = double.TryParse(match.Groups[5].Value.Trim(), out var _a5) ? _a5 : -1,
                    SoftIRQ = double.TryParse(match.Groups[6].Value.Trim(), out var _a6) ? _a6 : -1
                };
            }
            else
            {
                return new CPUInfo()
                {
                    AvgL1min = -1,
                    AvgL5min = -1,
                    AvgL15min = -1,
                    Total = -1,
                    User = -1,
                    Kernel = -1,
                    IOWait = -1,
                    IRQ = -1,
                    SoftIRQ = -1
                };
            }
        }
        /// <summary>
        /// 缓存系统窗体信息
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> DumpSysWindow()
        {
            var lines = ExecuteProcess($"-s {DeviceID} shell dumpsys window").Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var result = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (line.StartsWith("mCurrentFocus"))
                {
                    result.Add("CurrentFocus", line);
                }
                else if (line.StartsWith("mFocusedApp"))
                {
                    result.Add("FocusedApp", line);
                }
                else if (line.StartsWith("mInputMethodTarget:"))
                {
                    result.Add("InputMethodTarget", line);
                }
                else if (line.StartsWith("Display:"))
                {
                    result.Add("Display", line);
                }
                else if (line.StartsWith("Window:"))
                {
                    result.Add("Window", line);
                }
                else if (line.StartsWith("AppWindowToken:"))
                {
                    result.Add("AppWindowToken", line);
                }
                else if (line.StartsWith("mInputMethodWindow:"))
                {
                    result.Add("InputMethodWindow", line);
                }
                else if (line.StartsWith("mWallpaperTarget:"))
                {
                    result.Add("WallpaperTarget", line);
                }
            }
            return result;
        }

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
    }
}
