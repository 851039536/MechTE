using System;
using System.Runtime.InteropServices;

namespace MechTE_480.FormCategory
{
    public partial class MFormUtil
    {
        /// <summary>
        /// 定义当前窗体的宽度
        /// </summary>
        public static float X;
        /// <summary>
        /// 定义当前窗体的高度
        /// </summary>
        public static float Y;
        
        
        /// <summary>
        /// 定义鼠标点击窗体移动
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
    }
}
