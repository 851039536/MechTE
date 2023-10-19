using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MechTE_480.Windows
{
    /// <summary>
    /// 系统相关API
    /// </summary>
    public partial class MechWin
    {
        /// <summary>
        /// 设置系统音量
        /// </summary>
        public static void SetMasterVolume(float newLevel)
        {
            IAudioEndpointVolume masterVol = null;
            try
            {
                masterVol = GetMasterVolumeObject();
                if (masterVol == null)
                    return;

                masterVol.SetMasterVolumeLevelScalar(newLevel / 100, Guid.Empty);
            }
            finally
            {
                if (masterVol != null)
                    Marshal.ReleaseComObject(masterVol);
            }
        }
        /// <summary>
        /// 返回系统音量(1~100)
        /// </summary>
        public static float GetMasterVolume()
        {
            IAudioEndpointVolume masterVol = null;
            try
            {
                masterVol = GetMasterVolumeObject();
                if (masterVol == null)
                    return -1;

                masterVol.GetMasterVolumeLevelScalar(out var volumeLevel);
                return volumeLevel * 100;
            }
            finally
            {
                if (masterVol != null)
                    Marshal.ReleaseComObject(masterVol);
            }
        }
        
        /// <summary>
        /// 设置系统静音
        /// </summary>
        /// <param name="isMuted"></param>
        public static void SetMasterVolumeMute(bool isMuted)
        {
            IAudioEndpointVolume masterVol = null;
            try
            {
                masterVol = GetMasterVolumeObject();
                if (masterVol == null)
                    return;

                masterVol.SetMute(isMuted, Guid.Empty);
            }
            finally
            {
                if (masterVol != null)
                    Marshal.ReleaseComObject(masterVol);
            }
        }
        
        
        /// <summary>
        /// 弹出提示
        /// </summary>
        /// <param name="text">内容描述</param>
        /// <param name="caption">标题</param>
        public static void MesBoxs(string text, string caption)
        {
            //弹出提示
            MessageBox(IntPtr.Zero, text, caption, 0);
        }


        /// <summary>
        /// 弹出提示,传参
        /// </summary>
        /// <param name="text">内容描述</param>
        /// <param name="caption">标题</param>
        /// <param name="options">1:确认/取消,2:终止/重试/忽略,3:是/否/取消,4:是/否,5:重试/取消,6:取消/重试/继续</param>
        /// <returns></returns>
        public static int MesBoxs(string text, string caption, int options)
        {
            var ret = MessageBox(IntPtr.Zero, text, caption, options);
            return ret;
        }


        /// <summary>
        /// 开启音频设置显示到桌面
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

        private void test(string text, string caption)
        {
            //弹出提示
            MessageBox(IntPtr.Zero, text, caption, 0);
            //确认/取消
            MessageBox(IntPtr.Zero, text, caption, 1);
            //终止/重试/忽略
            MessageBox(IntPtr.Zero, text, caption, 2);
            //是/否/取消
            MessageBox(IntPtr.Zero, text, caption, 3);
            //是/否
            MessageBox(IntPtr.Zero, text, caption, 4);
            //重试/取消
            MessageBox(IntPtr.Zero, text, caption, 5);
            //取消/重试/继续
            MessageBox(IntPtr.Zero, text, caption, 6);
        }
    }
}