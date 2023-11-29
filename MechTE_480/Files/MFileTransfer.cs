using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using RestSharp;

namespace MechTE_480.Files
{
    /// <summary>
    /// 文件传输(上传,下载)
    /// </summary>
    public class MFileTransfer
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
            catch (Exception ex)
            {
                File.AppendAllText($@".\Log\错误信息{DateTime.Now:MM_dd}.txt", $"{DateTime.Now}\r\n{ex}\r\n\r\n",
                    Encoding.UTF8);
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

        /// <summary>
        /// zip压缩包下载
        /// 下载完成后自动执行解压动作,需传入解压路径unPath
        /// 解压完成自动根据downloadName字段删除zip包
        /// </summary>
        /// <param name="httpPath">HTTP POST请求路径</param>
        /// <param name="mode">模式:EngineeringMode(工程),TestItem(量产) </param>
        /// <param name="zipPath">下载文件到指定路径,不允许为空</param>
        /// <param name="unPath">解压到指定路径,如空则解压到当前程序集的执行路径(根目录)</param>
        /// <param name="downloadName">文件名称(必须跟后台上传文件名匹配)</param>
        /// <returns>bool</returns>
        public static bool DownloadZip(string httpPath, string mode, string zipPath, string unPath, string downloadName)
        {
            //检测下载解压的文件是否存在
            if (MechFile.IsExistDirectory(zipPath + @"\" + downloadName))
            {
                //删除
                MechFile.DeleteDirectory(zipPath + @"\" + downloadName);
            }

            //下载指定路径
            zipPath += @"\" + downloadName + ".zip";

            // 定义一个字符串变量，用于存储 JSON 格式的数据
            var strContent = "{\"TestName\":\"" + mode + "\",\"DownloadName\":\"" + downloadName + "\"}";

            // 发送HTTP POST请求，下载ZIP文件
            var data = HttpPost(httpPath,
                strContent, "POST", zipPath);
            //下载成功
            if (data)
            {
                // 解压文件
                ExtractZipFile(zipPath, unPath);
            }
            else
            {
                return false;
            }

            if (MechFile.IsExistFile(zipPath))
            {
                MechFile.DelFile(zipPath);
            }

            return true;
        }

        /// <summary>
        /// zip压缩包下载
        /// </summary>
        /// <param name="httpPath">HTTP POST请求路径</param>
        /// <param name="mode">模式:EngineeringMode(工程),TestItem(量产) </param>
        /// <param name="zipPath">下载文件到指定路径,不允许为空</param>
        /// <param name="downloadName">文件名称(必须跟后台上传文件名匹配)</param>
        /// <returns>bool</returns>
        public static bool DownloadZip(string httpPath, string mode, string zipPath, string downloadName)
        {
            //下载指定路径
            zipPath += @"\" + downloadName + ".zip";

            // 定义一个字符串变量，用于存储 JSON 格式的数据
            var strContent = "{\"TestName\":\"" + mode + "\",\"DownloadName\":\"" + downloadName + "\"}";

            // 发送HTTP POST请求，下载ZIP文件
            var data = HttpPost(httpPath,
                strContent, "POST", zipPath);
            return data;
        }
        
        /// <summary>
        /// zip文件上传1
        /// </summary>
        /// <param name="httpPath">HTTP POST请求路径</param>
        /// <param name="zipPath">指定zip文件上传的路径</param>
        /// <returns>bool</returns>
        public static bool UploadZip(string httpPath, string zipPath)
        {
            //指定上传文件的API地址
            var client = new RestClient(httpPath);
            //指定请求方式为POST
            var request = new RestRequest(Method.POST)
            {
                // 指定请求格式为Json
                RequestFormat = DataFormat.Json
            };
            // 添加上传的文件
            request.AddFile("file", zipPath);
            // 添加请求参数
            request.AddParameter("pictureName", "false");
            // 执行请求并获取响应内容
            var data = client.Execute(request);
            return data.StatusCode.ToString() == "OK";
        }
    }
}