using System.Diagnostics;

namespace MechTE_480.ProcessCategory
{
    /// <summary>
    /// cmd包装类
    /// </summary>
    public partial class MProcessUtil
    {
        
        /// <summary>
        /// 执行Bat
        /// </summary>
        /// <param name="name"></param>
        private static void ExeBat(string name)
        {
            var processInfo = new ProcessStartInfo();
            // 设置要执行的bat文件路径
            processInfo.FileName = name;
            // 设置以管理员权限运行
            // processInfo.Verb = "runas";
            // 创建一个Process对象
            Process process = new Process();
            process.StartInfo = processInfo;
            // 启动进程
            process.Start();
        }

        

        /// <summary>
        /// 进程窗口模式
        /// </summary>
        /// <param name="appName">应用程序路径名称</param>
        /// <param name="style">显示模式</param>
        /// <returns>bool</returns>
        private static bool StartApp(string appName,ProcessWindowStyle style)
        {
            return StartApp(appName,null,style);
        }
        
        /// <summary>
        /// 启动外部应用程序
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="arguments"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        private static bool StartApp(string appName,string arguments,ProcessWindowStyle style)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = appName,
                    Arguments = arguments,
                    WindowStyle = style
                }
            };

            try
            {
                process.Start();
                process.WaitForExit();
                return true;
            } catch
            {
                return false;
            } finally
            {
                process.Dispose();
            }
        }
        
    }
}