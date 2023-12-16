using System;
using System.Diagnostics;
using MechTE_480.util;

namespace MechTE_480.process
{
    /// <summary>
    /// 使用进程调用cmd命令或程序
    /// </summary>
    public partial class MProcess
    {
        #region Shell

        ///  <summary>
        /// 执行Shell
        ///  </summary>
        ///  <param name="cmd">Shell程序命令</param>
        public static void Shell(string cmd)
        {
            ExeCommand(new[] { cmd });
        }
        /// <summary>
        /// 执行bat文件
        /// </summary>
        /// <param name="cmd"></param>
        public static void Bat(string cmd)
        {
            ExeBat(cmd);
        }
        
        #endregion

        #region 启动应用网站
        /// <summary>
        /// 启动应用网站
        /// </summary>
        /// <param name="appName">/程序路径</param>
        /// <returns>bool</returns>
        public static bool StartApp(string appName)
        {
            return StartApp(appName,ProcessWindowStyle.Hidden);
        }

        /// <summary>
        /// 启动应用(管理员运行)
        /// </summary>
        /// <param name="appName"></param>
        public static void StartApps(string appName)
        {
            // 管理员启动并传值
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = MUtil.GetTheCurrentProgramAndDirectory() + appName,
                Verb = "runas" // 请求管理员权限
            };
            try
            {
                Process.Start(startInfo);
            } catch (Exception ex)
            {
                Console.WriteLine(@"无法以管理员权限重新启动应用程序：" + ex.Message);
            }
        }
        #endregion
    }
}