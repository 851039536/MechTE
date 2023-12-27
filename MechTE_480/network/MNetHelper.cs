using System;
using System.Net;
using System.Net.Sockets;

namespace MechTE_480.network
{
    /// <summary>
    /// 网络工具类
    /// </summary>
    public class MNetHelper
    {
        /// <summary>
        /// 获取本地IP
        /// </summary>
        /// <returns></returns>
        public static string GetAddressIp()
        {
            string addressIp = string.Empty;
            foreach (var ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ipAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    addressIp = ipAddress.ToString();
                }
            }

            return addressIp;
        }

        /// <summary>
        /// 获取本机的局域网IP
        /// </summary>        
        public static string LANIP()
        {

            //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            //如果本机IP列表为空，则返回空字符串
            if (addressList.Length < 1)
            {
                return "";
            }

            //返回本机的局域网IP
            return addressList[0].ToString();
        }

        /// <summary>
        /// 获取本机在Internet网络的广域网IP
        /// </summary>        
        public static string WANIP()
        {
                //获取本机的IP列表,IP列表中的第一项是局域网IP，第二项是广域网IP
                IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

                //如果本机IP列表小于2，则返回空字符串
                if (addressList.Length < 2)
                {
                    return "";
                }

                //返回本机的广域网IP
                return addressList[1].ToString();
        }

        /// <summary>
        /// 检查设置的端口号是否正确，并返回正确的端口号,无效端口号返回-1。
        /// </summary>
        /// <param name="port">设置的端口号</param>        
        public static int GetValidPort(string port)
        {
            //声明返回的正确端口号
            int validPort = -1;
            //最小有效端口号
            const int minport = 0;
            //最大有效端口号
            const int maxport = 65535;

            //检测端口号
            try
            {
                //传入的端口号为空则抛出异常
                if (port == "")
                {
                    throw new Exception("端口号不能为空！");
                }

                //检测端口范围
                if ((Convert.ToInt32(port) < minport) || (Convert.ToInt32(port) > maxport))
                {
                    throw new Exception("端口号范围无效！");
                }

                //为端口号赋值
                validPort = Convert.ToInt32(port);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return validPort;
        }

        /// <summary>
        /// 将字符串形式的IP地址转换成IPAddress对象
        /// </summary>
        /// <param name="ip">字符串形式的IP地址</param>        
        public static IPAddress StringToIpAddress(string ip)
        {
            return IPAddress.Parse(ip);
        }


        /// <summary>
        /// 获取本机的计算机名
        /// </summary>
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

       
    }
}