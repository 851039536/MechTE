using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace MechTE_480.Files
{
    /// <summary>
    /// 定制化文件传输(上传,下载)不通用
    /// </summary>
    public partial class MFileTransfer
    {
        /// <summary>
        /// 通过HTTP下载
        /// </summary>
        /// <param name="httpUrl">HTTP请求路径</param>
        /// <param name="writeData">JSON格式属性</param>
        /// <param name="method">请求</param>
        /// <param name="path">下载到指定路径</param>
        /// <returns>bool</returns>
        private static bool HttpPost(string httpUrl, string writeData, string method, string path)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
                //字符串转换为字节码
                var bs = Encoding.UTF8.GetBytes(writeData);
                //参数类型，这里是json类型
                //还有别的类型如"application/x-www-form-urlencoded"，不过我没用过(逃
                httpWebRequest.ContentType = "application/json";
                //参数数据长度
                httpWebRequest.ContentLength = bs.Length;
                //设置请求类型
                httpWebRequest.Method = method;
                //设置超时时间
                httpWebRequest.Timeout = 20000;
                //将参数写入请求地址中
                httpWebRequest.GetRequestStream().Write(bs, 0, bs.Length);
                //发送请求
                var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //流对象使用完后自动关闭
                using var stream = httpWebResponse.GetResponseStream();
                //文件流，流信息读到文件流中，读完关闭
                using var fs = File.Create(path);
                //建立字节组，并设置它的大小是多少字节
                var bytes = new byte[102400];
                var n = 1;
                while (n > 0)
                {
                    //一次从流中读多少字节，并把值赋给Ｎ，当读完后，Ｎ为０,并退出循环
                    if (stream != null) n = stream.Read(bytes, 0, 10240);
                    fs.Write(bytes, 0, n); //将指定字节的流信息写入文件流中
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        /// <summary>
        /// 解压文件.
        /// </summary>
        /// <param name="zipFilePath">要解压的zip</param>
        /// <param name="extractPath">解压到指定路径</param>
        private static void ExtractZipFile(string zipFilePath, string extractPath)
        {
            ZipFile.ExtractToDirectory(zipFilePath, extractPath);
        }
    }
}