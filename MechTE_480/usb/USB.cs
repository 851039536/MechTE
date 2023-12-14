using System.Collections.Generic;
using System.Management;
using System.Text.RegularExpressions;

namespace MechTE_480.usb
{
    public partial class MechHID
    {
        private struct PnPEntityInfo
        {
            public string PNPDeviceID;      // 设备ID
            public string Name;             // 设备名称
        }

        /// <summary>
        /// 根据VID和PID及设备匹配获取装置名称
        /// </summary>
        /// <param name="vendorId">供应商标识</param>
        /// <param name="productId">产品编号</param>
        /// <param name="names">装置名称</param>
        /// <returns></returns>
        public static string GetUSB_Name(ushort vendorId,ushort productId,string names)
        {
            var pnPEntities = new List<PnPEntityInfo>();
            // 枚举即插即用设备实体
            string vidpid;
            if (vendorId == ushort.MinValue) {
                if (productId == ushort.MinValue)
                    vidpid = "'%VID[_]____&PID[_]____%'";
                else
                    vidpid = "'%VID[_]____&PID[_]" + productId.ToString("X4") + "%'";
            } else {
                if (productId == ushort.MinValue)
                    vidpid = "'%VID[_]" + vendorId.ToString("X4") + "&PID[_]____%'";
                else
                    vidpid = "'%VID[_]" + vendorId.ToString("X4") + "&PID[_]" + productId.ToString("X4") + "%'";
            }

            string QueryString = "SELECT * FROM Win32_PnPEntity WHERE PNPDeviceID LIKE" + vidpid;
            ManagementObjectCollection PnPEntityCollection = new ManagementObjectSearcher(QueryString).Get();

            if (PnPEntityCollection != null) {
                foreach (ManagementObject Entity in PnPEntityCollection) {
                    string PNPDeviceID = Entity["PNPDeviceID"] as string;
                    // 过滤掉没有PID和VID的设备
                    Match match = Regex.Match(PNPDeviceID,"VID_[0-9|A-F]{4}&PID_[0-9|A-F]{4}");
                    if (match.Success) {
                        PnPEntityInfo Element;
                        //Element.PNPDeviceID = PNPDeviceID;                      // 设备ID
                        //Element.Name = Entity["Name"] as String;                // 设备名称
                        string name = Entity["Name"] as string;
                        if (name.Contains(names)) return name;
                        //Element.VendorID = Convert.ToUInt16(match.Value.Substring(4, 4), 16);   // 供应商标识
                        //Element.ProductID = Convert.ToUInt16(match.Value.Substring(13, 4), 16); // 产品编号
                        //PnPEntities.Add(Element);
                    }
                }
            }
            return "False";
            //if (PnPEntities.Count == 0) return null; else return PnPEntities;
        }


        /// <summary>
        /// 判断USB指定装置是否存在
        /// </summary>
        /// <param name="deviceName">DeviceID : 如PID VID</param>
        /// <returns></returns>
        public static bool IsUSBDevice(string deviceName)
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select DeviceID From Win32_USBHub"))
                collection = searcher.Get();
            foreach (var device in collection) {
                if (device.ToString().Contains(deviceName)) {
                    return true;
                }
            }
            return false;
        }
    }
}
