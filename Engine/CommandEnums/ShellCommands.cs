using Meow.Util.ADB.Engine.Enums;

namespace Meow.Util.ADB.Engine.CommandEnums
{
    /// <summary>
    /// Shell命令类
    /// </summary>
    public static class ShellCommands
    {
        /// <summary>
        /// 一个按键事件
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>命令的表示串</returns>
        public static string Keyevent(Key key) => $"input keyevent {key}";
        /// <summary>
        /// 点击屏幕某处
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <returns>命令的表示串</returns>
        public static string Tap(long x, long y) => $"input tap {x} {y}";
        /// <summary>
        /// 点击屏幕某处
        /// </summary>
        /// <param name="a">坐标集合结构体</param>
        /// <returns></returns>
        public static string Tap((long x, long y) a) => $"input tap {a.x} {a.y}";
        /// <summary>
        /// 按住并拖拽屏幕
        /// </summary>
        /// <param name="x1">按下的x坐标</param>
        /// <param name="y1">按下的y坐标</param>
        /// <param name="x2">抬起的x坐标</param>
        /// <param name="y2">抬起的y坐标</param>
        /// <param name="duration">动作时长</param>
        /// <returns>命令的表示串</returns>
        public static string Swipe(long x1, long y1, long x2, long y2, long duration) => $"input swipe {x1} {y1} {x2} {y2} {duration}";
        /// <summary>
        /// 按住并拖拽屏幕
        /// </summary>
        /// <param name="x1">按下的x坐标</param>
        /// <param name="y1">按下的y坐标</param>
        /// <param name="x2">抬起的x坐标</param>
        /// <param name="y2">抬起的y坐标</param>
        /// <returns>命令的表示串</returns>
        public static string Swipe(long x1, long y1, long x2, long y2) => $"input swipe {x1} {y1} {x2} {y2}";
        /// <summary>
        /// 截图到某个路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>命令的表示串</returns>
        public static string Screencap(string path) => $"screencap {path}";
        /// <summary>
        /// 截图到 "/sdcard/.screenshot.png" (并覆盖)
        /// </summary>
        /// <returns>命令的表示串</returns>
        public static string Screencap() => $"screencap /sdcard/.screenshot.png";
        /// <summary>
        /// 控制Wifi行为
        /// </summary>
        /// <param name="on">是否开启</param>
        /// <returns>命令的表示串</returns>
        public static string WifiControl(bool on = true) => $"svc wifi {(on ? "enable" : "disable")}";
        /// <summary>
        /// 获得系统的连接性信息
        /// </summary>
        /// <returns>命令的表示串</returns>
        public static string DumpSysConnectivity() => $"dumpsys connectivity";
        /// <summary>
        /// 获得系统的Wifi连接信息
        /// </summary>
        /// <returns>命令的表示串</returns>
        public static string DumpSysConnectivity_WIFI() => $"dumpsys wifi";
        /// <summary>
        /// 获得系统的运营商注册信息
        /// </summary>
        /// <returns>命令的表示串</returns>
        public static string DumpSysConnectivity_TR() => $"dumpsys telephony.registry";
        /// <summary>
        /// 列出所有包
        /// </summary>
        /// <returns>命令的表示串</returns>
        public static string PackageList() => "pm list packages";
    }
    /// <summary>
    /// ADB的核心命令
    /// </summary>
    public static class AdbCommands
    {
        /// <summary>
        /// 拉取文件到本地某处
        /// </summary>
        /// <param name="path">远程文件位置</param>
        /// <param name="locpath">本地位置</param>
        /// <returns>命令的表示串</returns>
        public static string Pull(string path, string locpath) => $"pull {path} {locpath}";
        /// <summary>
        /// 安装某个APK包
        /// </summary>
        /// <param name="path">要安装的包在本地的位置</param>
        /// <returns>命令的表示串</returns>
        public static string InstallApk(string path) => $"install {path}";
    }
}
