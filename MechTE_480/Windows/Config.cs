﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MechTE_480.Windows
{
    public partial class MechWin
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
        
        public static void RunDll(string arguments)
        {
            // using (Process.Start("rundll32.exe", arguments))
            // {
            // }
            
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "rundll32.exe",
                    Arguments = arguments,
                    UseShellExecute = true
                };

                using (Process.Start(startInfo))
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法打开音频设置面板：" + ex.Message);
            }
        }
        
    }
}