using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MechTE.TWinForm
{
    /// <summary>
    /// winfrom窗体api
    /// </summary>
    public class TForm
    {
        /// <summary>
        /// 弹框提示
        /// </summary>
        /// <param name="name">描述</param>
        /// <param name="title">标题</param>
        /// <returns>bool</returns>
        public static bool TMessageBox(string name, string title)
        {
            DialogResult result = MessageBox.Show(@name, @title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return false;
        }



        /// <summary>
        /// 获取当前进程信息
        /// </summary>
        /// <returns></returns>
        public static Process TGetCurrentProcess()
        {
            Process cur = Process.GetCurrentProcess();
            //当前进程的id
            Console.WriteLine(cur.Id);
            //获取关联的进程的终端服务会话标识符。
            Console.WriteLine(cur.SessionId);
            //当前进程的名称
            Console.WriteLine(cur.ProcessName);
            //当前进程的启动时间
            Console.WriteLine(cur.StartTime);
            //获取关联进程终止时指定的值,在退出事件中使用
            //Console.WriteLine(cur.ExitCode);
            //获取进程的当前机器名称
            Console.WriteLine(cur.MachineName); //.代表本地
            Console.WriteLine(cur.MainWindowTitle);

            return cur;
        }
        /// <summary>
        /// 检查重复启动
        /// </summary>
        /// <param name="name"></param>
        public static void TGetProcessesByName(string name)
        {
            Process[] pro = Process.GetProcessesByName(name);
            var process = pro.Where(p => p.ProcessName.Equals(Process.GetCurrentProcess().ProcessName));
            int n = process.Count();
            if (n > 1)
            {
                MessageBox.Show("线程已启动");
                System.Environment.Exit(0);
            }
        }

        /// <summary>
        /// 获得本机的进程
        /// </summary>
        /// <returns>Process[]</returns>
        public static Process[] TGetProcesses()
        {
            Process[] proList = Process.GetProcesses(".");//获得本机的进程
            return proList;
        }

        /// <summary>
        /// 网盘登录
        /// </summary>
        /// <param name="path">网盘路径:\10.xx.xx</param>
        /// <param name="userName">用户</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static bool TLoginNetwork(string path, string userName, string passWord)
        {
            bool flag = false;
            Process proc = new Process();//实例启动一个独立进程
            try
            {
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
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
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
            return flag;
        }

    }
}
