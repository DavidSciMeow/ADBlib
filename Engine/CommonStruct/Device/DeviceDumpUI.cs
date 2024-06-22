using Meow.Util.ADB.Engine.CommandEnums;
using Meow.Util.ADB.Engine.CommonStruct.UIStructs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Meow.Util.ADB.Engine.CommonStruct
{
    public partial class Device : IDisposable
    {
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
            }
            throw new Exception("Device Not Pullable");
        }
    }
}
