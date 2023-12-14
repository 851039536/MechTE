using RestSharp;

namespace MechTE_480.Files
{
    /// <summary>
    /// 定制化文件传输(上传,下载)不通用
    /// </summary>
    public partial class MFileTransfer
    {

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
            if (MFile.IsExistDirectory(zipPath + @"\" + downloadName))
            {
                //删除
                MFile.DeleteDirectory(zipPath + @"\" + downloadName);
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

            if (MFile.IsExistFile(zipPath))
            {
                MFile.DelFile(zipPath);
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