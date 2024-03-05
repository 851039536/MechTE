using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace MechTE_480.PortCategory.hid
{
    public partial class MHidUtil
    {
        /// <summary>
        /// 定义数组长度,句柄对象,循环次数
        /// 如有增加长度,同步新增col01
        /// </summary>
        private const int IntLen = 10;

        /// <summary>
        /// 存储的句柄对象数组通道1
        /// </summary>
        public readonly IntPtr[] SetHandle1 = new IntPtr[IntLen];

        /// <summary>
        ///  存储的句柄对象数组通道2
        /// </summary>
        public readonly IntPtr[] SetHandle2 = new IntPtr[IntLen];

        /// <summary>
        ///  存储的句柄地址通道1
        /// </summary>
        public readonly string[] SetPath1 = new string[IntLen];

        /// <summary>
        ///  存储的句柄地址通道2
        /// </summary>
        public readonly string[] SetPath2 = new string[IntLen];

        #region 参数及引用区

        /// <summary>
        /// 关闭访问设备句柄，结束进程的时候把这个加上保险点
        /// </summary>
        /// <param name="hFile"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll",SetLastError = true)]
        private static extern int CloseHandle(IntPtr hFile);

        /// <summary>
        /// 获得GUID
        /// </summary>
        /// <param name="hidGuid"></param>
        [DllImport("hid.dll")]
        private static extern void HidD_GetHidGuid(ref Guid hidGuid);

        [DllImport("setupapi.dll",SetLastError = true)] //过滤设备，获取需要的设备
        private static extern IntPtr SetupDiGetClassDevs(ref Guid gClass,[MarshalAs(UnmanagedType.LPStr)] string strEnumerator,IntPtr hParent,
            Digcf nFlags);

        private enum Digcf //3
        {
            DigcfDefault = 0x1, //返回与系统默认设备相关的设备
            DigcfPresent = 0x2, //返回当前存在的设备
            DigcfAllclasses = 0x4, //返回所有安装的设备
            DigcfProfile = 0x8, //只返回当前硬件配置文件的设备
            DigcfDeviceinterface = 0x10 //返回所有支持的设备
        }

        internal struct SpDeviceInterfaceData
        {
            internal int Size;
            internal Guid InterfaceClassGuid;
            internal int Flags;
            internal int Reserved;
        }

        [DllImport("setupapi.dll",CharSet = CharSet.Auto,SetLastError = true)]
        private static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr hDevInfo,uint devInfo,ref Guid interfaceClassGuid,uint memberIndex,
            ref SpDeviceInterfaceData deviceInterfaceData);

        [DllImport("setupapi.dll",SetLastError = true,CharSet = CharSet.Auto)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet,ref SpDeviceInterfaceData deviceInterfaceData,
            IntPtr deviceInterfaceDetailData,
            uint deviceInterfaceDetailDataSize,ref uint requiredSize,IntPtr deviceInfoData);

        [StructLayout(LayoutKind.Sequential,Pack = 2)] //2
        internal struct SpDeviceInterfaceDetailData
        {
            internal int Size;

            [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 256)]
            internal string DevicePath;
        }

        private const uint GENERIC_READ = 0x80000000;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint FILE_SHARE_WRITE = 0x2;
        private const uint FILE_SHARE_READ = 0x1;
        private const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        private const uint OPEN_EXISTING = 3;

        /// <summary>
        /// 句柄
        /// </summary>
        public IntPtr Handle = IntPtr.Zero;
        /// <summary>
        /// Path
        /// </summary>
        public string Path = "";

        [DllImport("kernel32.dll",SetLastError = true)]
        private static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPStr)] string strName,uint nAccess,uint nShareMode,IntPtr lpSecurity,
            uint nCreationFlags,uint nAttributes,IntPtr lpTemplate);

        [DllImport("setupapi.dll",SetLastError = true)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr lpDeviceInfoSet,ref SpDeviceInterfaceData oInterfaceData,
            ref SpDeviceInterfaceDetailData oDetailData,uint nDeviceInterfaceDetailDataSize,ref uint nRequiredSize,IntPtr lpDeviceInfoData);

        [DllImport("setupapi.dll",SetLastError = true)]
        private static extern IntPtr SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

        [DllImport("hid.dll",SetLastError = true)]
        static extern Boolean HidD_GetFeature(IntPtr HidDeviceObject,Byte[] lpReportBuffer,Int32 ReportBufferLength);
        [DllImport("hid.dll",SetLastError = true)]
        static extern Boolean HidD_SetFeature(IntPtr HidDeviceObject,Byte[] lpReportBuffer,Int32 ReportBufferLength);
        [DllImport("hid.dll",SetLastError = true)]
        private static extern bool HidD_SetOutputReport(IntPtr hDev, //设备句柄，即CreateFile的返回值
            byte[] reportBuf,//存有待发送数据的buffer
            int OUT_REPORT_LEN); //buffer的长度
        [DllImport("Kernel32.dll",SetLastError = true)]
        private static extern bool WriteFile(
            IntPtr hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            ref uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped
        );

        [DllImport("hid.dll",SetLastError = true)]
        private static extern bool HidD_GetInputReport(
            IntPtr HidDeviceObject,
            byte[] lpReportBuffer,
            int ReportBufferLength);


        #endregion

        #region 获取装置路径

        /// <summary>
        /// 获取双通道装置路径
        /// </summary>
        /// <param name="pid01"></param>
        /// <param name="vid01"></param>
        /// <param name="pid02"></param>
        /// <param name="vid02"></param>
        /// <returns></returns>
        private bool GetHidDevicePath(string pid01,string vid01,string pid02,string vid02)
        {
            var hidGuid = Guid.Empty;
            var Dpid01 = new Regex(pid01.ToLower());
            var Dvid01 = new Regex(vid01.ToLower());
            var Dpid02 = new Regex(pid02.ToLower());
            var Dvid02 = new Regex(vid02.ToLower());

            Regex[] ExternAgs =
            {
                new Regex("col01"),
                new Regex("col02"),
                new Regex("col03"),
                new Regex("col04"),
                new Regex("col05"),
                new Regex("col06"),
                new Regex("col07"),
                new Regex("col08"),
                new Regex("col09"),
                new Regex("col10")
            };
            var ret = true;
            var retFlag = false;
            uint deviceSerialNumber = 0;

            HidD_GetHidGuid(ref hidGuid);
            //过滤设备，获取需要的设备
            var hDevInfo = SetupDiGetClassDevs(ref hidGuid,null,IntPtr.Zero,Digcf.DigcfPresent | Digcf.DigcfDeviceinterface);
            try {
                var deviceInterfaceData = new SpDeviceInterfaceData();
                deviceInterfaceData.Size = Marshal.SizeOf(deviceInterfaceData);
                while (ret) {
                    ret = SetupDiEnumDeviceInterfaces(hDevInfo,0,ref hidGuid,deviceSerialNumber,
                        ref deviceInterfaceData); //获取设备，true获取到                   
                    if (ret) {
                        uint nRequiredSize = 0;
                        SetupDiGetDeviceInterfaceDetail(hDevInfo,ref deviceInterfaceData,IntPtr.Zero,0,ref nRequiredSize,IntPtr.Zero);

                        var detailData = new SpDeviceInterfaceDetailData {
                            Size = 5 // hardcoded to 5! Sorry, but this works and trying more future proof versions by setting the size to the struct sizeof failed miserably. If you manage to sort it, mail me! Thx
                        };
                        ret = SetupDiGetDeviceInterfaceDetail(hDevInfo,ref deviceInterfaceData,ref detailData,nRequiredSize,ref nRequiredSize,
                            IntPtr.Zero); //获取接口的详细信息，必须调用两次，一次返回长度，二次获取数据

                        if (ret) {
                            if (detailData.DevicePath != null) {
                                var matchPid01 = Dpid01.Match(detailData.DevicePath);
                                var matchVid01 = Dvid01.Match(detailData.DevicePath);

                                var matchPid02 = Dpid02.Match(detailData.DevicePath);
                                var matchVid02 = Dvid02.Match(detailData.DevicePath);

                                var mathExternAgs = new Match[IntLen];

                                for (int i = 0 ; i < IntLen ; i++) {
                                    mathExternAgs[i] = ExternAgs[i].Match(detailData.DevicePath);
                                }

                                if (matchPid02.Success && matchVid02.Success) {
                                    for (int i = 0 ; i < IntLen ; i++) {
                                        if (mathExternAgs[i].Success) {
                                            SetPath2[i] = detailData.DevicePath;
                                            retFlag = true;
                                        }
                                    }
                                } else if (matchPid01.Success && matchVid01.Success) {
                                    for (int i = 0 ; i < IntLen ; i++) {
                                        if (mathExternAgs[i].Success) {
                                            SetPath1[i] = detailData.DevicePath;
                                            retFlag = true;
                                        }
                                    }
                                }
                            }

                            deviceSerialNumber++;
                        }
                    }
                }
            } catch (Exception) {
                retFlag = false;
            } finally {
                SetupDiDestroyDeviceInfoList(hDevInfo);
            }

            return retFlag;
        }


        /// <summary>
        /// 获取单通道装置路径
        /// </summary>
        /// <param name="pid01"></param>
        /// <param name="vid01"></param>
        /// <returns></returns>
        private bool GetHidDevicePath(string pid01,string vid01)
        {
            var hidGuid = Guid.Empty;
            var Dpid01 = new Regex(pid01.ToLower());
            var Dvid01 = new Regex(vid01.ToLower());

            Regex[] ExternAgs =
            {
                new Regex("col01"),
                new Regex("col02"),
                new Regex("col03"),
                new Regex("col04"),
                new Regex("col05"),
                new Regex("col06"),
                new Regex("col07"),
                new Regex("col08"),
                new Regex("col09"),
                new Regex("col10")
            };
            var ret = true;
            var retFlag = false;
            uint deviceSerialNumber = 0;

            HidD_GetHidGuid(ref hidGuid);
            //过滤设备，获取需要的设备
            var hDevInfo = SetupDiGetClassDevs(ref hidGuid,null,IntPtr.Zero,Digcf.DigcfPresent | Digcf.DigcfDeviceinterface);
            try {
                var deviceInterfaceData = new SpDeviceInterfaceData();
                deviceInterfaceData.Size = Marshal.SizeOf(deviceInterfaceData);
                while (ret) {
                    ret = SetupDiEnumDeviceInterfaces(hDevInfo,0,ref hidGuid,deviceSerialNumber,
                        ref deviceInterfaceData); //获取设备，true获取到                   
                    if (ret) {
                        uint nRequiredSize = 0;
                        SetupDiGetDeviceInterfaceDetail(hDevInfo,ref deviceInterfaceData,IntPtr.Zero,0,ref nRequiredSize,IntPtr.Zero);

                        var detailData = new SpDeviceInterfaceDetailData {
                            Size = 5 // hardcoded to 5! Sorry, but this works and trying more future proof versions by setting the size to the struct sizeof failed miserably. If you manage to sort it, mail me! Thx
                        };
                        ret = SetupDiGetDeviceInterfaceDetail(hDevInfo,ref deviceInterfaceData,ref detailData,nRequiredSize,ref nRequiredSize,
                            IntPtr.Zero); //获取接口的详细信息，必须调用两次，一次返回长度，二次获取数据

                        if (ret) {
                            if (detailData.DevicePath != null) {
                                var matchPid01 = Dpid01.Match(detailData.DevicePath);
                                var matchVid01 = Dvid01.Match(detailData.DevicePath);


                                var mathExternAgs = new Match[IntLen];

                                for (int i = 0 ; i < IntLen ; i++) {
                                    mathExternAgs[i] = ExternAgs[i].Match(detailData.DevicePath);
                                }

                                if (matchPid01.Success && matchVid01.Success) {
                                    for (int i = 0 ; i < IntLen ; i++) {
                                        if (mathExternAgs[i].Success) {
                                            SetPath1[i] = detailData.DevicePath;
                                            retFlag = true;
                                        }
                                    }
                                }
                            }

                            deviceSerialNumber++;
                        }
                    }
                }
            } catch (Exception) {
                retFlag = false;
            } finally {
                SetupDiDestroyDeviceInfoList(hDevInfo);
            }

            return retFlag;
        }

        /// <summary>
        ///获取单通道装置路径
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="vid"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private bool GetHidDevicePath(string pid,string vid,string col)
        {
            Guid hidGuid = Guid.Empty;
            Regex TXregPid = new Regex(pid.ToLower());
            Regex TXregVid = new Regex(vid.ToLower());
            Regex ExternAgs = new Regex(col.ToLower());
            bool result = true;
            IntPtr hidHandle = IntPtr.Zero;
            UInt32 deviceSerialNumber = 0;
            HidD_GetHidGuid(ref hidGuid);

            IntPtr hDevInfo = SetupDiGetClassDevs(ref hidGuid,null,IntPtr.Zero,Digcf.DigcfPresent | Digcf.DigcfDeviceinterface); //过滤设备，获取需要的设备
            try {
                SpDeviceInterfaceData deviceInterfaceData = new SpDeviceInterfaceData();
                deviceInterfaceData.Size = Marshal.SizeOf(deviceInterfaceData);
                while (result) {
                    result = SetupDiEnumDeviceInterfaces(hDevInfo,0,ref hidGuid,deviceSerialNumber,
                        ref deviceInterfaceData); //获取设备，true获取到                   
                    if (result) {
                        uint nRequiredSize = 0;
                        SetupDiGetDeviceInterfaceDetail(hDevInfo,ref deviceInterfaceData,IntPtr.Zero,0,ref nRequiredSize,IntPtr.Zero);

                        SpDeviceInterfaceDetailData detailData = new SpDeviceInterfaceDetailData();
                        detailData.Size =
                            5; // hardcoded to 5! Sorry, but this works and trying more future proof versions by setting the size to the struct sizeof failed miserably. If you manage to sort it, mail me! Thx
                        result = SetupDiGetDeviceInterfaceDetail(hDevInfo,ref deviceInterfaceData,ref detailData,nRequiredSize,ref nRequiredSize,
                            IntPtr.Zero); //获取接口的详细信息，必须调用两次，一次返回长度，二次获取数据

                        if (result) {
                            if (detailData.DevicePath != null) {
                                Match MatchPid = TXregPid.Match(detailData.DevicePath);
                                Match MatchVID = TXregVid.Match(detailData.DevicePath);
                                Match mathExternAgs = ExternAgs.Match(detailData.DevicePath);

                                if (MatchPid.Success && MatchVID.Success && mathExternAgs.Success) {
                                    Path = detailData.DevicePath;
                                    return true;
                                }
                            }

                            deviceSerialNumber++;
                        }
                    }
                }
            } catch (Exception) {
                return false;
            } finally {
                SetupDiDestroyDeviceInfoList(hDevInfo);
            }

            return false;
        }

        #endregion

        #region 将路径转换成句柄

        private static IntPtr GetHidDeviceHandle(string HidDevicePath)
        {
            var _HIDWriteHandle = IntPtr.Zero;
            if (!string.IsNullOrEmpty(HidDevicePath)) {
                _HIDWriteHandle = CreateFile(HidDevicePath,GENERIC_WRITE | GENERIC_READ,FILE_SHARE_READ | FILE_SHARE_WRITE,IntPtr.Zero,
                    OPEN_EXISTING,0,IntPtr.Zero);
            }
            return _HIDWriteHandle;
        }
        #endregion

        #region 删除驱动

        /// <summary>
        /// SpDeviceInfoData
        /// </summary>
        protected struct SpDeviceInfoData
        {
            public int Size;
            public Guid InterfaceClassGuid;
            public int Flags;
            public int Reserved;
        }

        [DllImport("setupapi.dll",SetLastError = true)]
        private static extern bool SetupDiEnumDeviceInfo(IntPtr hDevInfo,uint Widx,ref SpDeviceInfoData deviceInterfaceData);

        [DllImport("setupapi.dll",SetLastError = true)]
        private static extern bool SetupDiGetDeviceRegistryProperty(IntPtr hDevInfo,ref SpDeviceInfoData deviceInterfaceData,SPDRP OPTIONAL,uint PropertyRegDataType,StringBuilder PropertyBuffer,uint PropertyBufferSize,uint RequiredSize);
        [DllImport("setupapi.dll",SetLastError = true)]
        private static extern bool SetupDiRemoveDevice(IntPtr hDevInfo,ref SpDeviceInfoData deviceInterfaceData);
        public enum SPDRP
        {
            SPDRP_DEVICEDESC = 0,
            SPDRP_HARDWAREID = 0x1,
            SPDRP_COMPATIBLEIDS = 0x2,
            SPDRP_UNUSED0 = 0x3,
            SPDRP_SERVICE = 0x4,
            SPDRP_UNUSED1 = 0x5,
            SPDRP_UNUSED2 = 0x6,
            SPDRP_CLASS = 0x7,
            SPDRP_CLASSGUID = 0x8,
            SPDRP_DRIVER = 0x9,
            SPDRP_CONFIGFLAGS = 0xA,
            SPDRP_MFG = 0xB,
            SPDRP_FRIENDLYNAME = 0xC,
            SPDRP_LOCATION_INFORMATION = 0xD,
            SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0xE,
            SPDRP_CAPABILITIES = 0xF,
            SPDRP_UI_NUMBER = 0x10,
            SPDRP_UPPERFILTERS = 0x11,
            SPDRP_LOWERFILTERS = 0x12,
            SPDRP_BUSTYPEGUID = 0x13,
            SPDRP_LEGACYBUSTYPE = 0x14,
            SPDRP_BUSNUMBER = 0x15,
            SPDRP_ENUMERATOR_NAME = 0x16,
            SPDRP_SECURITY = 0x17,
            SPDRP_SECURITY_SDS = 0x18,
            SPDRP_DEVTYPE = 0x19,
            SPDRP_EXCLUSIVE = 0x1A,
            SPDRP_CHARACTERISTICS = 0x1B,
            SPDRP_ADDRESS = 0x1C,
            SPDRP_UI_NUMBER_DESC_FORMAT = 0x1E,
            SPDRP_MAXIMUM_PROPERTY = 0x1F
        }
        
 

        #endregion 
        
        /// <summary>
        /// 將16進制字符串轉換為16进制byte數組并且根据数组长度自动补0
        /// </summary>
        /// <param name="shex">要转换的16进制字符串</param>
        /// <param name="length">要转换的Byte数组长度</param>
        /// <returns>转换后的Byte数组，自动补0</returns>
        public static byte[] HexToByteArray(string shex, int length)
        {
            // 将输入的十六进制字符串按空格分割成字符串数组
            string[] ssArray = shex.Split(' ');
            // 创建一个空的字节数组列表
            var bytList = new List<byte>();
            int i = 0;
            foreach (var s in ssArray)
            {  
                //将十六进制的字符串转换成数值
                bytList.Add(Convert.ToByte(s, 16));
                i++;
            }
            for (int j = i; j < length; j++)
            {
                bytList.Add(Convert.ToByte("0"));
            }
            return bytList.ToArray();
        }
       
    }
}