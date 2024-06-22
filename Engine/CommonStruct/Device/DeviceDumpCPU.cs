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
    }
}
