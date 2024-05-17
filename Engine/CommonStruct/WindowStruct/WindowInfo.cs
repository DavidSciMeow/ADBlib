using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Meow.Util.ADB.Engine.CommonStruct.WindowStruct
{
    /// <summary>
    /// 窗体信息基类
    /// </summary>
    public readonly struct WindowInfo
    {
        /// <summary>
        /// 显示设备的ID
        /// </summary>
        public int DisplayId { get; }
        /// <summary>
        /// 根任务的ID
        /// </summary>
        public int RootTaskId { get; }
        /// <summary>
        /// 会话信息
        /// </summary>
        public string Session { get; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public string Client { get; }
        /// <summary>
        /// 拥有者的用户ID
        /// </summary>
        public int OwnerUid { get; }
        /// <summary>
        /// 应用程序的包名
        /// </summary>
        public string Package { get; }
        /// <summary>
        /// 窗口的属性，包括位置、大小、格式、动画等
        /// </summary>
        public string Attrs { get; }
        /// <summary>
        /// 是否是输入法窗口
        /// </summary>
        public bool IsImWindow { get; }
        /// <summary>
        /// 是否是壁纸窗口
        /// </summary>
        public bool IsWallpaper { get; }
        /// <summary>
        /// 是否是浮动层
        /// </summary>
        public bool IsFloatingLayer { get; }
        /// <summary>
        /// 基础层级
        /// </summary>
        public int BaseLayer { get; }
        /// <summary>
        /// 子层级
        /// </summary>
        public int SubLayer { get; }
        /// <summary>
        /// 窗口的标识符
        /// </summary>
        public string Token { get; }
        /// <summary>
        /// 视图的可见性
        /// </summary>
        public int ViewVisibility { get; }
        /// <summary>
        /// 是否有框架
        /// </summary>
        public bool HaveFrame { get; }
        /// <summary>
        /// 是否被遮挡
        /// </summary>
        public bool Obscured { get; }
        /// <summary>
        /// 内容插入的边距
        /// </summary>
        public string GivenContentInsets { get; }
        /// <summary>
        /// 可触摸的插入的边距
        /// </summary>
        public int TouchableInsets { get; }
        /// <summary>
        /// 完整的配置信息，包括屏幕密度、屏幕尺寸、屏幕方向等
        /// </summary>
        public string FullConfiguration { get; }
        /// <summary>
        /// 最后报告的配置信息
        /// </summary>
        public string LastReportedConfiguration { get; }
        /// <summary>
        /// 是否有表面
        /// </summary>
        public bool HasSurface { get; }
        /// <summary>
        /// 是否准备好显示
        /// </summary>
        public bool IsReadyForDisplay { get; }
        /// <summary>
        /// 是否允许移除窗口
        /// </summary>
        public bool WindowRemovalAllowed { get; }
        /// <summary>
        /// 窗口的边框信息
        /// </summary>
        public string Frames { get; }
        /// <summary>
        /// 容器的动画信息
        /// </summary>
        public string ContainerAnimator { get; }
        /// <summary>
        /// 窗口状态的动画信息
        /// </summary>
        public string WindowStateAnimator { get; }
        /// <summary>
        /// 绘制状态
        /// </summary>
        public string DrawState { get; }
        /// <summary>
        /// 是否有待处理的进入动画
        /// </summary>
        public bool EnterAnimationPending { get; }
        /// <summary>
        /// 系统装饰的矩形
        /// </summary>
        public string SystemDecorRect { get; }
        /// <summary>
        /// 是否强制无缝旋转
        /// </summary>
        public bool ForceSeamlesslyRotate { get; }
        /// <summary>
        /// 是否在屏幕上
        /// </summary>
        public bool IsOnScreen { get; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; }

        /// <summary>
        /// 尝试生成一个窗体监控信息类
        /// </summary>
        /// <param name="data">信息原始字段</param>
        /// <returns></returns>
        public static List<Dictionary<string, string>> Parse(string data)
        {
            var result = new List<Dictionary<string, string>>();
            var windowMatches = Regex.Matches(data, @"Window #\d+ Window\{.*?\}:.*?(?=Window #\d+ Window\{|$)", RegexOptions.Singleline);

            foreach (Match windowMatch in windowMatches)
            {
                var windowInfo = new Dictionary<string, string>();
                var propertyMatches = Regex.Matches(windowMatch.Value, @"(\w+)=(\S+)");

                foreach (Match propertyMatch in propertyMatches)
                {
                    var key = propertyMatch.Groups[1].Value;
                    var value = propertyMatch.Groups[2].Value;
                    windowInfo[key] = value;
                }

                result.Add(windowInfo);
            }

            return result;
        }
    }
}
