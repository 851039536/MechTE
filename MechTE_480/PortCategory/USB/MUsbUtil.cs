using System.Management;
using System.Text.RegularExpressions;

namespace MechTE_480.PortCategory.usb
{
    /// <summary>
    /// USB端口类
    /// </summary>
    public class MUsbUtil
    {

        /// <summary>
        /// 装置USB装置名称
        /// </summary>
        /// <param name="vendorId">供应商标识VID</param>
        /// <param name="productId">产品编号PID</param>
        /// <param name="names">匹配装置名称</param>
        /// <returns></returns>
        public static string GetDeviceName(ushort vendorId, ushort productId, string names)
        {
            // 枚举即插即用设备实体
            string vpId;
            if (vendorId == ushort.MinValue)
            {
                if (productId == ushort.MinValue)
                    vpId = "'%VID[_]____&PID[_]____%'";
                else
                    vpId = "'%VID[_]____&PID[_]" + productId.ToString("X4") + "%'";
            }
            else
            {
                if (productId == ushort.MinValue)
                    vpId = "'%VID[_]" + vendorId.ToString("X4") + "&PID[_]____%'";
                else
                    vpId = "'%VID[_]" + vendorId.ToString("X4") + "&PID[_]" + productId.ToString("X4") + "%'";
            }

            string queryString = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE" + vpId;
            var collection = new ManagementObjectSearcher(queryString).Get();

            foreach (ManagementObject entity in collection)
            {
                string deviceId = entity["PNPDeviceID"] as string;
                // 过滤掉没有PID和VID的设备
                Match match = Regex.Match(deviceId, "VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                if (match.Success)
                {
                    string name = entity["Name"] as string;
                    if (name != null && name.Contains(names)) return name;
                }
            }
            return "False";
        }

        /// <summary>
        /// 检测USB播放装置是否存在
        /// </summary>
        /// <param name="deviceName">装置名称(DeviceID:PID_A527)</param>
        /// <returns></returns>
        public static bool IsDevice(string deviceName)
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select DeviceID From Win32_USBHub"))
                collection = searcher.Get();
            foreach (var device in collection)
            {
                if (device.ToString().Contains(deviceName))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}