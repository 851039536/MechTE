using MechTE_480.MECH;
using System;
using System.Diagnostics;

namespace MechTE_480.Order
{
    /// <summary>
    /// cmd命令
    /// </summary>
    public partial class MechProcess
    {
        #region cmd命令

        ///  <summary>
        /// 执行Shell命令
        ///  </summary>
        ///  <param name="cmd">Shell程序命令</param>
        public static void StartShell(string cmd)
        {
            ExeCommand(new[] { cmd });
        }
        /// <summary>
        /// 执行bat文件
        /// </summary>
        /// <param name="cmd"></param>
        public static void StartBat(string cmd)
        {
            ExeBat(cmd);
        }
        #endregion

        #region 启动Windows应用程序

        /// <summary>
        ///  启动Windows应用/网站
        /// </summary>
        /// <param name="appName">/程序路径</param>
        /// <returns>bool</returns>
        public static bool StartApp(string appName)
        {
            return StartApp(appName,ProcessWindowStyle.Hidden);
        }

        /// <summary>
        /// 管理员运行程序
        /// </summary>
        /// <param name="appName"></param>
        public static void StartApps(string appName)
        {
            // 管理员启动并传值
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = MechUtils.GetTheCurrentProgramAndDirectory() + appName,
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

        #endregion

    }
}