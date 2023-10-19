using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MechTE_480.MECH
{
    public class MechWin
    {
        /// <summary>
        /// Windows操作系统提供的一个函数，用于在应用程序中显示消息框。消息框可以用于显示警告、错误、提示等信息，并与用户进行交互。
        /// </summary>
        /// <param name="hWnd">窗口句柄设为0，表示使用默认窗口</param>
        /// <param name="text">提示描述</param>
        /// <param name="caption">标题</param>
        /// <param name="options">消息框的选项，例如按钮类型和图标类型</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int MessageBox(IntPtr hWnd, string text, string caption, int options);
        
        /// <summary>
        /// 弹出提示
        /// </summary>
        /// <param name="text">内容描述</param>
        /// <param name="caption">标题</param>
        public static void MesBoxs(string text, string caption)
        {
            //弹出提示
            MessageBox(IntPtr.Zero, text,  caption, 0);
        }
        
        /// <summary>
        /// 弹出提示,传参
        /// </summary>
        /// <param name="text">内容描述</param>
        /// <param name="caption">标题</param>
        /// <param name="options">1:确认/取消,2:终止/重试/忽略,3:是/否/取消,4:是/否,5:重试/取消,6:取消/重试/继续</param>
        /// <returns></returns>
        public static int MesBoxs(string text, string caption,int options)
        {
           var ret= MessageBox(IntPtr.Zero, text,  caption, options);
           return ret;
        }
        
        /// <summary>
        /// 开启音频内部装置窗体显示到桌面
        /// </summary>
        /// <returns></returns>
        public static bool EnterHfp()
        {
            using (Process.Start("rundll32.exe", @"C:\WINDOWS\system32\shell32.dll,Control_RunDLL mmsys.cpl,,1"))
                return true;
        }

        /// <summary>
        /// 检测进程关掉音频内部装置
        /// </summary>
        /// <param name="processName">rundll32</param>
        /// <returns>bool</returns>
        public static bool QuitHfp(string processName = "rundll32")
        {
            //得到所有打开的进程   
            foreach (var thisProc in Process.GetProcesses())
                if (thisProc.ProcessName.Contains("rundll32"))
                    thisProc.Kill();
            return true;
        }
        private  void test(string text, string caption)
        {
            //弹出提示
            MessageBox(IntPtr.Zero, text,  caption, 0);
            //确认/取消
            MessageBox(IntPtr.Zero, text,  caption, 1);
            //终止/重试/忽略
            MessageBox(IntPtr.Zero, text,  caption, 2);
            //是/否/取消
            MessageBox(IntPtr.Zero, text,  caption, 3);
            //是/否
            MessageBox(IntPtr.Zero, text,  caption, 4);
            //重试/取消
            MessageBox(IntPtr.Zero, text,  caption, 5);
            //取消/重试/继续
            MessageBox(IntPtr.Zero, text,  caption, 6);
        }
    }
}