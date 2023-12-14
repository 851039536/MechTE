using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace MechTE_480.Windows
{
    /// <summary>
    /// 系统相关API
    /// </summary>
    public partial class MechWin
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(int description, int reservedValue);
        #region 方法一

        /// <summary>
        /// 用于检查网络是否可以连接互联网,true表示连接成功,false表示连接失败
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            const int description = 0;
            return InternetGetConnectedState(description, 0);
        }
        #endregion 方法一
        #region 方法二

        /// <summary>
        /// 用于检查IP地址或域名(www.cnblogs.com)是否可以使用TCP/IP协议访问(使用Ping命令),true表示Ping成功,false表示Ping失败
        /// </summary>
        /// <param name="strIpOrDName">输入参数,表示IP地址或域名</param>
        /// <returns></returns>
        public static bool PingIpOrDomainName(string strIpOrDName)
        {
            try
            {
                var objPingSender = new Ping();
                var objPinOptions = new PingOptions();
                objPinOptions.DontFragment = true;
                const string data = "";
                var buffer = Encoding.UTF8.GetBytes(data);
                const int intTimeout = 120;
                var objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                var strInfo = objPinReply?.Status.ToString();
                return strInfo == "Success";
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion 方法二

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
        /// 启动播放装置
        /// </summary>
        /// <returns></returns>
        public static void OpenA2DP()
        {
            RunDll("shell32.dll,Control_RunDLL mmsys.cpl @1");
        }

        /// <summary>
        /// 开启音频设置显示到桌面
        /// </summary>
        /// <returns></returns>
        public static bool EnterHfp()
        {
            RunDll("shell32.dll,Control_RunDLL mmsys.cpl,,1");
            return true;
        }

        /// <summary>
        /// 开启系统设备管理器
        /// </summary>
        public static void OpenDevice()
        {
            RunDll("shell32.dll,Control_RunDLL sysdm.cpl,,2");
        }

        /// <summary>
        /// 检测进程
        /// </summary>
        /// <param name="processName">rundll32</param>
        /// <returns>bool</returns>
        public static bool CloseRunDll(string processName = "rundll32")
        {
            //得到所有打开的进程   
            foreach (var thisProc in Process.GetProcesses())
                if (thisProc.ProcessName.Contains("rundll32"))
                    thisProc.Kill();
            return true;
        }
        
        /// <summary>
        /// 判断当前程序是否是管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsUserAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
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