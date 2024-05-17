using System;

namespace Meow.Util.ADB.Engine.CommonStruct.Enums
{
    /// <summary>
    /// UI语音事件类型
    /// </summary>
    [Flags]
    public enum UISpeechStateEventType
    {
        /// <summary>
        /// 表示语音开始讲话的状态。
        /// </summary>
        SPEECH_STATE_SPEAKING_START = 1,

        /// <summary>
        /// 表示语音结束讲话的状态。
        /// </summary>
        SPEECH_STATE_SPEAKING_END = 2,

        /// <summary>
        /// 表示语音开始监听的状态。
        /// </summary>
        SPEECH_STATE_LISTENING_START = 4,

        /// <summary>
        /// 表示语音结束监听的状态。
        /// </summary>
        SPEECH_STATE_LISTENING_END = 8,
    }
}
