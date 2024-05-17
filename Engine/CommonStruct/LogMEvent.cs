using System;
using System.Text;

namespace Meow.Util.ADB.Engine.CommonStruct
{
    /// <summary>
    /// 描述日志事件的结构体。
    /// </summary>
    public struct LogMEvent
    {
        /// <summary>
        /// 时间戳。
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 设备名称。
        /// </summary>
        public string Device { get; set; }

        /// <summary>
        /// 事件类型。
        /// </summary>
        public string EventType { get; set; }

        /// <summary>
        /// 事件代码。
        /// </summary>
        public string EventCode { get; set; }

        /// <summary>
        /// 事件值。
        /// </summary>
        public string EventValue { get; set; }

        /// <summary>
        /// 从日志行创建 LogMEvent 的构造函数。
        /// </summary>
        /// <param name="line">日志行。</param>
        public LogMEvent(string line)
        {
            // 初始化字段
            TimeStamp = null; Device = null; EventType = null; EventCode = null; EventValue = null;

            // 分割日志行
            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // 根据分割结果设置字段值
            switch (parts.Length)
            {
                case 1:
                    TimeStamp = parts[0].Trim('[', ']');
                    break;
                case 2:
                    Device = parts[1].TrimEnd(':');
                    break;
                case 3:
                    EventType = parts[2];
                    break;
                case 4:
                    EventCode = parts[3];
                    break;
                case 5:
                    EventValue = parts[4];
                    break;
            }
        }

        /// <summary>
        /// 重写 ToString 方法，返回事件的详细信息。
        /// </summary>
        /// <returns>事件的详细信息。</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Timestamp: {TimeStamp}");
            sb.Append($"Device: {Device}");
            sb.Append($"Event Type: {EventType}");
            sb.Append($"Event Code: {EventCode}");
            sb.Append($"Event Value: {EventValue}");
            return sb.ToString();
        }
    }
}
