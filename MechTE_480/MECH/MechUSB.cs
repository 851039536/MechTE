using System.Management;

namespace MechTE_480.MECH
{
    public class MechUSB
    {
        /// <summary>
        /// 判断USB指定装置是否存在
        /// </summary>
        /// <param name="deviceName">装置名称(DeviceID)</param>
        /// <returns></returns>
        public static bool IsUSBDevice(string deviceName)
        {
            ManagementObjectCollection collection;
            using(var searcher = new ManagementObjectSearcher(@"Select DeviceID From Win32_USBHub"))
                collection = searcher.Get();
            foreach(var device in collection)
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