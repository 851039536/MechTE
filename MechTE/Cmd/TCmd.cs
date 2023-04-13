using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MechTE.Cmd
{
    /// <summary>
    /// cmd命令
    /// </summary>
    public class TCmd
    {

        #region electron
        /// <summary>
        /// cmd命令 vue版本
        /// </summary>
        /// <param name="name">Shell程序命令</param>
        /// <returns>string</returns>
        public async Task<object> VExe(dynamic name) {
            return await ExeCommandAsync(new string[] { name });
        }

        #endregion

        #region cmd
        /// <summary>
        /// 执行多条cmd.exe命令
        /// </summary>
        ///命令文本数组
        /// 命令输出文本
        private static string ExeCommand(string[] commandTexts)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            string strOutput = null;
            try {
                p.Start();
                foreach (string item in commandTexts) {
                    p.StandardInput.WriteLine(item);
                }
                p.StandardInput.WriteLine("exit");
                strOutput = p.StandardOutput.ReadToEnd();
                //strOutput = Encoding.UTF8.GetString(Encoding.Default.GetBytes(strOutput));
                p.WaitForExit();
                p.Close();
            } catch (Exception e) {
                strOutput = e.Message;
            }
            return strOutput;
        }

        private static async Task<string> ExeCommandAsync(string[] commandTexts) {
            using (Process p = new Process()) {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;

                try {
                    p.Start();
                    foreach (string item in commandTexts) {
                        await p.StandardInput.WriteLineAsync(item);
                    }
                    await p.StandardInput.WriteLineAsync("exit");
                    string strOutput = await p.StandardOutput.ReadToEndAsync();
                    //strOutput = Encoding.UTF8.GetString(Encoding.Default.GetBytes(strOutput));
                     p.WaitForExit();

                    return strOutput;
                } catch (Exception e) {
                    return e.Message;
                }
            }
        }


        /// <summary>
        ///cmd.exe命令
        /// </summary>
        /// <param name="cmd">Shell程序命令</param>
        /// <returns>string</returns>
        public static string Exe(string cmd)
        {
            return ExeCommand(new string[] { cmd });
        }


        #endregion

        #region 启动外部Windows应用程序
        /// <summary>
        ///  启动外部Windows应用程序，隐藏程序界面
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
        /// 
        /// 启动外部应用程序，隐藏程序界面
        /// 
        ///应用程序路径名称
        ///启动参数
        /// true表示成功，false表示失败
        private static bool StartApp(string appName, string arguments)
        {
            return StartApp(appName, arguments, ProcessWindowStyle.Hidden);
        }
        /// 
        /// 启动外部应用程序
        /// 
        ///应用程序路径名称
        ///启动参数
        ///进程窗口模式
        /// true表示成功，false表示失败
        private static bool StartApp(string appName, string arguments, ProcessWindowStyle style)
        {
            bool blnRst = false;
            Process p = new Process();
            p.StartInfo.FileName = appName;//exe,bat and so on
            p.StartInfo.WindowStyle = style;
            p.StartInfo.Arguments = arguments;
            try
            {
                p.Start();
                p.WaitForExit();
                p.Close();
                blnRst = true;
            }
            catch
            {
            }
            return blnRst;
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
        public static bool LoginNetwork(string path,string userName,string passWord) {
            bool flag = false;
            Process proc = new Process();//实例启动一个独立进程
            try {
                proc.StartInfo.FileName = "cmd.exe";//设定程序名
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true; //重定向标准输入
                proc.StartInfo.RedirectStandardOutput = true;//重定向标准输出
                proc.StartInfo.RedirectStandardError = true;//重定向错误输出
                proc.StartInfo.CreateNoWindow = true; //设定不显示窗口
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine); //执行的命令
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited) {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg)) {
                    flag = true;
                } else {
                    throw new Exception(errormsg);
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            } finally {
                proc.Close();
                proc.Dispose();
            }
            return flag;
        }
        #endregion
    }
}
