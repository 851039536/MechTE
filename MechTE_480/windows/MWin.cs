using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;

namespace MechTE_480.Windows
{
    /// <summary>
    /// 系统相关API
    /// </summary>
    public partial class MWin
    {
        
        /// <summary>
        /// 用于检查网络是否可以连接互联网
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectInternet()
        {
            const int description = 0;
            return InternetGetConnectedState(description, 0);
        }

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
        public static void OpenA2Dp()
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
        /// 清除rundll32进程
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
        
        
        /// <summary>
        /// 将本程序设为开启自启
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <returns></returns>
        public static bool SetMeStart(bool onOff)
        {
            bool isOk = false;
            string appName = Process.GetCurrentProcess().MainModule.ModuleName;
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            isOk = SetAutoStart(onOff, appName, appPath);
            return isOk;
        }
 
        /// <summary>
        /// 将应用程序设为或不设为开机启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <param name="appName">应用程序名</param>
        /// <param name="appPath">应用程序完全路径</param>
        private static bool SetAutoStart(bool onOff, string appName, string appPath)
        {
            bool isOk = true;
            //如果从没有设为开机启动设置到要设为开机启动
            if (!IsExistKey(appName) && onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            //如果从设为开机启动设置到不要设为开机启动
            else if (IsExistKey(appName) && !onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            return isOk;
        }
 
        /// <summary>
        /// 判断注册键值对是否存在，即是否处于开机启动状态
        /// </summary>
        /// <param name="keyName">键值名</param>
        /// <returns></returns>
        public static bool IsExistKey(string keyName)
        {
            try
            {
                bool _exist = false;
                RegistryKey local = Registry.LocalMachine;
                RegistryKey runs = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null)
                {
                    RegistryKey key2 = local.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName)
                {
                    if (strName.ToUpper() == keyName.ToUpper())
                    {
                        _exist = true;
                        return _exist;
                    }
                }
                return _exist;
 
            }
            catch
            {
                return false;
            }
        }
 
        /// <summary>
        /// 写入或删除注册表键值对,即设为开机启动或开机不启动
        /// </summary>
        /// <param name="isStart">是否开机启动</param>
        /// <param name="exeName">应用程序名</param>
        /// <param name="path">应用程序路径带程序名</param>
        /// <returns></returns>
        private static bool SelfRunning(bool isStart, string exeName, string path)
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                //若开机自启动则添加键值对
                if (isStart)
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames)
                    {
                        if (keyName.ToUpper() == exeName.ToUpper())
                        {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                return false;
                //throw;
            }
 
            return true;
        }
    }
}