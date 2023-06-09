using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MechTE_452.Systems
{
    /// <summary>
    /// 系统类
    /// </summary>
    
    public class TSystems
    {
        #region 读取当前系统信息
        /// <summary>
        /// 读取当前系统信息
        /// </summary>
        /// <returns></returns>
        public static string[] GetWindows()
        {
            string[] name = new string[8];
            //获取系统信息
            try
            {
                name[0] = "用户名：" + SystemInformation.UserName;
                name[1] = "计算机名：" + SystemInformation.ComputerName;
                name[2] = "操作系统：" + Environment.OSVersion.Platform;
                name[3] = "版本号：" + Environment.OSVersion.Version;
                if (SystemInformation.BootMode.ToString() == "Normal")
                    name[4] = "启动方式：正常启动";
                if (SystemInformation.BootMode.ToString() == "FailSafe")
                    name[4] = "启动方式：安全启动";
                if (SystemInformation.BootMode.ToString() == "FailSafeWithNetwork")
                    name[4] = "启动方式：通过网络服务启动";
                if (SystemInformation.Network)
                    name[5] = "网络连接：已连接";
                else
                    name[5] = "网络连接：未连接";
                name[6] = "显示器数量：" + SystemInformation.MonitorCount.ToString();
                name[7] = "显示器分辨率：" + SystemInformation.PrimaryMonitorMaximizedWindowSize.Width.ToString() + "X" +
                    SystemInformation.PrimaryMonitorMaximizedWindowSize.Height.ToString();
            }
            catch
            {
                MessageBox.Show("获取系统信息发生错误！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return name;
        }
        #endregion

        #region  获取当前进程信息
        /// <summary>
        /// 获取当前进程信息
        /// </summary>
        /// <returns></returns>
        public static Process GetCurrentProcess()
        {
            //string[] process = new string[10];
            Process cur = Process.GetCurrentProcess();
            //process[0] = "进程的id:" + cur.Id;
            //process[0] = "关联进程的终端服务会话标识符:" + cur.SessionId;
            //process[0] = "当前进程名称:" + cur.ProcessName;
            //process[0] = "当前进程启动时间:" + cur.StartTime;
            //process[0] = "当前机器名称:" + cur.MachineName;
            //process[0] = "主窗口标题:" + cur.MainWindowTitle;
            return cur;
        }
        #endregion

        #region 检查重复启动
        /// <summary>
        /// 检查重复启动
        /// </summary>
        /// <param name="name">进程名称</param>
        public static void CheckProcessesByName(string name)
        {
            Process[] pro = Process.GetProcessesByName(name);
            var process = pro.Where(p => p.ProcessName.Equals(Process.GetCurrentProcess().ProcessName));
            int n = process.Count();
            if (n > 1)
            {
                MessageBox.Show("线程已启动");
                Environment.Exit(0);
            }
        }
        #endregion

        #region 获得本机的进程
        /// <summary>
        /// 获得本机的进程
        /// </summary>
        /// <returns>Process[]</returns>
        public static Process[] GetProcesses()
        {
            Process[] proList = Process.GetProcesses(".");//获得本机的进程
            return proList;
        }
        #endregion

        #region 获取系统驱动器
        /// <summary>
        /// 获取系统驱动器
        /// </summary>
        /// <returns></returns>
        public static string[] GetLogicalDrives()
        {
            //获取系统驱动器
           return  Directory.GetLogicalDrives();
        }
        #endregion
    }
}
