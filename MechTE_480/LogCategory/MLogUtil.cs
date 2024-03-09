using System;
using System.IO;
using MechTE_480.DateTimeCategory;

namespace MechTE_480.LogCategory
{
    /// <summary>
    /// 写本地log文本数据
    /// </summary>
    public static class MLogUtil
    {
        /// <summary>
        /// 写入本地log,自动生成当前时间
        /// </summary>
        /// <param name="paths">写入log路径</param>
        /// <param name="name">log文件名称</param>
        /// <param name="value">写入内容</param>
        public static void LogWrite(string paths, string name, string value)
        {
            string dataTime = DateTime.Now.ToString("yyyy-MM-dd");
            //项目根目录
            string path = $@"{paths}\{dataTime}_{name}.txt";
            if (!Directory.Exists($@"{paths}"))
                Directory.CreateDirectory($@"{paths}");
            var writer = !File.Exists(path) ? File.CreateText(path) : File.AppendText(path);
            writer.WriteLine(value);
            writer.Flush();
            writer.Close();
        }
        
        /// <summary>
        /// 写入本地log,自动生成前一天时间
        /// </summary>
        /// <param name="paths"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void LogWriteYesterdayTime(string paths, string name, string value)
        {
            //项目根目录
            string path = $@"{paths}\{MDateTimeUtil.GetYesterdayTime()}_{name}.txt";
            if (!Directory.Exists($@"{paths}"))
                Directory.CreateDirectory($@"{paths}");
            var writer = !File.Exists(path) ? File.CreateText(path) : File.AppendText(path);
            writer.WriteLine(value);
            writer.Flush();
            writer.Close();
        }
    }
}