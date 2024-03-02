using System;
using System.IO;

namespace MechTE_480.LogCategory
{
    /// <summary>
    /// 写本地log数据
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
    }
}