using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MechTE_480.FileCategory
{
    public partial class MFileUtil
    {
        /// <summary>
        /// 使用DllImport指定调用的Windows API函数及其相关信息
        /// </summary>
        /// <param name="hwnd">指定父窗口句柄:ntPtr.Zero</param>
        /// <param name="lpszOp">指定要进行的操作:Open</param>
        /// <param name="lpszFile">指定要打开的文件名|路径</param>
        /// <param name="lpszParams">指定命令行参数: 0 | ""</param>
        /// <param name="lpszDir">用于指定默认目录:0 | ""</param>
        /// <param name="fsShowCmd">显示模式: 0:隐藏 1~11</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        private static extern int ShellExecute(IntPtr hwnd,StringBuilder lpszOp,StringBuilder lpszFile,StringBuilder lpszParams,StringBuilder lpszDir,int fsShowCmd);
    }
}