using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace MechTE_480.windows
{
    /// <summary>
    /// 控制鼠标类
    /// </summary>
    public class MMouse
    {
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="location">窗体坐标（this.Location）</param>
        /// <param name="offSetValueX">X坐标偏移值</param>
        /// <param name="offSetValueY">Y坐标偏移值（this.Location）</param>
        public static void MouseMove(Point location, int offSetValueX, int offSetValueY)
        {
            SetCursorPos(location.X + offSetValueX, location.Y + offSetValueY);
        }
        
        /// <summary>
        /// 鼠标左键点击
        /// </summary>
        public static void MouseLeftClick()
        {
            mouse_event(LeftDown, 0, 0, 0, 0);
            Thread.Sleep(50);
            mouse_event(LeftUp, 0, 0, 0, 0);
        }
        /// <summary>
        /// 鼠标右键点击
        /// </summary>
        public static void MouseRightClick()
        {
            mouse_event(RightDown, 0, 0, 0, 0);
            Thread.Sleep(50);
            mouse_event(RightUp, 0, 0, 0, 0);
        }
        /// <summary>
        /// 鼠标中键点击
        /// </summary>
        public static void MouseMiddleClick()
        {
            mouse_event(MiddleDown, 0, 0, 0, 0);
            Thread.Sleep(50);
            mouse_event(MiddleUp, 0, 0, 0, 0);
        }

        #region 参数，引用
        private const int Move = 0x0001;     // 移动鼠标           (十):1
        private const int LeftDown = 0x0002; //模仿鼠标左键按下    (十):2
        private const int LeftUp = 0x0004; //模仿鼠标左键抬起    (十):4
        private const int RightDown = 0x0008; //模仿鼠标右键按下    (十):8
        private const int RightUp = 0x0010; //模仿鼠标右键抬起    (十):16
        private const int MiddleDown = 0x0020;// 模仿鼠标中键按下    (十):32
        private const int MiddleUp = 0x0040;// 模仿鼠标中键抬起    (十):64
        private const int Absolute = 0x8000; //标示是否采取绝对坐标    (十):32768
        private struct WindowRect
        {
            int Left;
            int Top;
            int Right;
            int Bottom;
        }
        [DllImport("User32.dll")]
        private static extern Int32 GetCursorPos(out Point point);
        [DllImport("User32.dll")]
        private static extern Boolean SetCursorPos(Int32 X, Int32 Y);
        [DllImport("User32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, UInt32 dwExtraInfo);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        private static extern bool GetWindowRect(IntPtr lpClassName, out WindowRect iWindow);
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowPos(IntPtr hwnd, IntPtr hwndaftef, int x, int y, int cx, int cy, uint wflags);
        #endregion
    }
}
