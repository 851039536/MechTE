using System;
using System.Diagnostics;

namespace MechTE_480.Order
{
    /// <summary>
    /// cmd命令
    /// </summary>
    public static class MechCmd
    {
        #region cmd命令
        
        ///  <summary>
        /// 执行Shell命令
        ///  </summary>
        ///  <param name="cmd">Shell程序命令</param>
        public static void StartShell(string cmd)
        {
            CmdPack.ExeCommand(new[] { cmd });
        }      
        
        
        /// <summary>
        /// 执行bat文件
        /// </summary>
        /// <param name="cmd"></param>
        public static void StartBat(string cmd)
        {
            CmdPack.ExeBat(cmd);
        }

        #endregion
        

        #region 启动Windows应用程序

        /// <summary>
        ///  启动Windows应用程序，隐藏程序界面
        /// </summary>
        /// <param name="appName">/应用程序路径名称</param>
        /// <returns>bool</returns>
        public static bool StartApp(string appName)
        {
            return StartApp(appName, ProcessWindowStyle.Hidden);
        }

        /// <summary>
        /// 进程窗口模式
        /// </summary>
        /// <param name="appName">应用程序路径名称</param>
        /// <param name="style">显示模式</param>
        /// <returns>bool</returns>
        private static bool StartApp(string appName, ProcessWindowStyle style)
        {
            return StartApp(appName, null, style);
        }


        /// <summary>
        /// 启动外部应用程序
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="arguments"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        private static bool StartApp(string appName, string arguments, ProcessWindowStyle style)
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
            }
            catch
            {
                return false;
            }
            finally
            {
                process.Dispose();
            }
        }

        #endregion

        #region 网盘登录

        /// <summary>
        /// 网盘登录
        /// </summary>
        /// <param name="path">网盘路径:\10.xx.xx</param>
        /// <param name="userName">用户</param>
        /// <param name="passWord">密码</param>
        /// <returns>bool</returns>
        public static bool LoginNetwork(string path, string userName, string passWord)
        {
            var proc = new Process(); //实例启动一个独立进程
            try
            {
                proc.StartInfo.FileName = "cmd.exe"; //设定程序名
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true; //重定向标准输入
                proc.StartInfo.RedirectStandardOutput = true; //重定向标准输出
                proc.StartInfo.RedirectStandardError = true; //重定向错误输出
                proc.StartInfo.CreateNoWindow = true; //设定不显示窗口
                proc.Start();
                var dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine); //执行的命令
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }

                var errors = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errors))
                {
                }
                else
                {
                    throw new Exception(errors);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }

            return true;
        }

        #endregion
    }
}