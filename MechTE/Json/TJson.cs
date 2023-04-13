using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;

namespace MechTE.TJson
{
    /// <summary>
    /// Newtonsoft.Json
    /// </summary>
    public class TJson
    {
        #region 将序列化的json字符串内容写入Json文件
        /// <summary>
        /// 将序列化的json字符串内容写入Json文件，并且保存
        /// </summary>
        /// <param name="path">将序列化的json字符串内容写入Json文件，并且保存</param>
        /// <param name="jsonConents">写入的数据</param>
        public static void WriteJsonStr(string path, string jsonConents)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(jsonConents);
                }
            }
        }
        #endregion

        /// <summary>
        /// 获取到本地的Json文件并且解析返回对应的json字符串
        /// </summary>
        /// <param name="filepath">路径:如@".\json\JProgramFile.json"</param>
        /// <returns>string</returns>
        public static string GetJsonStr(string filepath)
        {
            string json = string.Empty;
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }


        #region 读取数组Json数据
        /// <summary>
        /// 读取数组Json数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>JArray</returns>
        public static JArray GetJson(string path)
        {
            return JArray.Parse(GetJsonStr(@path));
        }
        #endregion


        /// <summary>
        /// 写入数组Json数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="j">JArray</param>
        public static void WriteJson(string path, JArray j)
        {
            string jsondata = JsonConvert.SerializeObject(j);
            WriteJsonStr(@path, jsondata);
        }
    }
}
