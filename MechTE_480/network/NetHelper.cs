using System;
using System.Net;

namespace MechTE_480.network
{
    public class NetHelper
    {
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public string GetAddressIP()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }

            return AddressIP;
        }

        #region 检查设置的端口号是否正确，返回正确的端口号

        /// <summary>
        /// 检查设置的端口号是否正确，并返回正确的端口号,无效端口号返回-1。
        /// </summary>
        /// <param name="port">设置的端口号</param>        
        public static int GetValidPort(string port)
        {
            //声明返回的正确端口号
            int validPort = -1;
            //最小有效端口号
            const int MINPORT = 0;
            //最大有效端口号
            const int MAXPORT = 65535;

            //检测端口号
            try
            {
                //传入的端口号为空则抛出异常
                if (port == "")
                {
                    throw new Exception("端口号不能为空！");
                }

                //检测端口范围
                if ((Convert.ToInt32(port) < MINPORT) || (Convert.ToInt32(port) > MAXPORT))
                {
                    throw new Exception("端口号范围无效！");
                }

                //为端口号赋值
                validPort = Convert.ToInt32(port);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
            }

            return validPort;
        }

        #endregion

        #region 将字符串形式的IP地址转换成IPAddress对象

        /// <summary>
        /// 将字符串形式的IP地址转换成IPAddress对象
        /// </summary>
        /// <param name="ip">字符串形式的IP地址</param>        
        public static IPAddress StringToIpAddress(string ip)
        {
            return IPAddress.Parse(ip);
        }

        #endregion

        #region 获取本机的计算机名

        /// <summary>
        /// 获取本机的计算机名
        /// </summary>
        public static string LocalHostName()
        {
            return Dns.GetHostName();
        }

        #endregion
    }
}