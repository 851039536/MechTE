﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using MechTE_480.ConvertCategory;
using Microsoft.Win32.SafeHandles;

namespace MechTE_480.PortCategory.hid
{
    /// <summary>
    /// HID指令帮助类
    /// </summary>
    public partial class MHidUtil
    {
        /// <summary>
        /// 存储回传值
        /// </summary>
        public string ReturnValue;

        /// <summary>
        /// 存储全部回传值
        /// </summary>
        public string ReturnAllValue;


        #region WriteSend

        /// <summary>
        /// 使用write下下指令
        /// </summary>
        /// <param name="command">指令如: 06 00 05...</param>
        /// <param name="length">指令长度</param>
        /// <param name="intPtr">装置句柄(如果没有调用GetHandle获取)</param>
        /// <returns>bool</returns>
        public bool WriteSend(string command, int length, IntPtr intPtr)
        {
            Thread.Sleep(20);
            var comm = HexToByteArray(command, length);
            uint numberOfBytesWritten = 0;
            try
            {
                return WriteFile(intPtr, comm, (uint)length, ref numberOfBytesWritten, IntPtr.Zero);
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// 使用write下下指令(64长度)
        /// </summary>
        /// <param name="command">指令如: 06 00 05...</param>
        /// <param name="intPtr">装置句柄(如果没有调用GetHandle获取)</param>
        /// <returns>bool</returns>
        public bool WriteSend(string command, IntPtr intPtr)
        {
            Thread.Sleep(20);
            const int length = 64;
            var comm = HexToByteArray(command, length);
            uint numberOfBytesWritten = 0;
            try
            {
                return WriteFile(intPtr, comm, length, ref numberOfBytesWritten, IntPtr.Zero);
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion

        #region 使用 getReport下指令

        /// <summary>
        /// 使用 getReport下指令
        /// </summary>
        /// <param name="command">指令06 00 05...</param>
        /// <param name="length">指令长度</param>
        /// <param name="intPtr">句柄指针</param>
        /// <returns>指令是否下成功</returns>
        public bool GetReportSend(string command, int length, IntPtr intPtr)
        {
            Thread.Sleep(20);
            var comm = HexToByteArray(command, length);
            try
            {
                return HidD_GetInputReport(intPtr, comm, length);
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion

        #region 使用setReport下指令

        /// <summary>
        /// 使用setReport下指令
        /// </summary>
        /// <param name="command">指令06 00 05...</param>
        /// <param name="length">指令长度</param>
        /// <param name="intPtr">句柄指针</param>
        /// <returns></returns>
        public bool SetReportSend(string command, int length, IntPtr intPtr)
        {
            Thread.Sleep(20);
            var comm = HexToByteArray(command, length);
            try
            {
                return HidD_SetOutputReport(intPtr, comm, length);
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion

        #region 使用SetFeature下指令

        /// <summary>
        /// 使用SetFeature下指令
        /// </summary>
        /// <param name="command">指令06 00 05...</param>
        /// <param name="length">指令长度</param>
        /// <param name="intPtr">句柄指针</param>
        /// <returns>指令是否下成功</returns>
        public bool SetFeatureSend(string command, int length, IntPtr intPtr)
        {
            Thread.Sleep(20);
            var comm = HexToByteArray(command, length);
            try
            {
                //              通道    指令   指令長度               
                return HidD_SetFeature(intPtr, comm, length);
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion

        #region WriteReturn

        /// <summary>
        /// 使用write下指令,返回值存储到ReturnValue
        /// </summary>
        /// <param name="command">指令06 00 05...</param>
        /// <param name="length">指令长度</param>
        /// <param name="readData">指定匹配的值</param>
        /// <param name="index">指令返回值索引0 1 2 3...</param>
        /// <param name="handle">句柄路径</param>
        /// <param name="intPtr">句柄指针</param>
        /// <returns>指令是否下成功</returns>
        public bool WriteReturn(string command, int length, string readData, string index, string handle, IntPtr intPtr)
        {
            ReturnValue = "False";
            ReturnAllValue = "False";

            var createFileHandle = CreateFile(handle, //文件位置
                0x40000000 | 0x80000000, //允许对设备进行读写访问
                0x1 | 0x2, //允许对设备进行共享访问
                IntPtr.Zero, //指向空指针（SECURITY_ATTRIBUTES定义文件的安全特性）
                3, //文件必须已存在
                0x40000000, //允许对文件进行重叠操作
                IntPtr.Zero); //指向空指针（如果不为零，则指定一个文件句柄。新文件将从这个文件中复制扩展属性）
            var readFlag = true;
            var values = "";
            var arrInputReport = new byte[length];
            var endFlag = false;


            var fs = new FileStream(new SafeFileHandle(createFileHandle, false), FileAccess.Read | FileAccess.Write, length, true);
            try
            {
                #region 监听通道回传值

                void AsyRead(IAsyncResult iResult)
                {
                    byte[] arrBuff = (byte[])iResult.AsyncState;
                    try
                    {
                        fs.EndRead(iResult);
                    }
                    catch
                    {
                        fs.Close();
                    }

                    var arrReadData = readData.Split(' ');
                    for (var i = 0; i < arrReadData.Length; i++)
                    {
                        if (arrBuff[i] != Convert.ToInt32(arrReadData[i], 16))
                        {
                            readFlag = false;
                            return;
                        }
                    }

                    var arrIndexes = index.Split(' ');
                    foreach (var a in arrIndexes)
                    {
                        values = values + $"{arrBuff[Convert.ToInt32(a)]:X2}" + " ";
                    }

                    var valueAll = "";
                    for (var i = 0; i < length; i++)
                    {
                        valueAll = values + $"{arrBuff[Convert.ToInt32(i)]:X2}" + " ";
                    }

                    ReturnAllValue = valueAll;
                    values = values.Substring(0, values.Length - 1);
                    endFlag = true;
                }

                #endregion

                fs.BeginRead(arrInputReport, 0, length, AsyRead, arrInputReport);
                Thread.Sleep(20);
                var nums = 0;
                while (true)
                {
                    WriteSend(command, length, intPtr);
                    if (endFlag)
                    {
                        if (readFlag)
                        {
                            ReturnValue = values.Trim();
                            return true;
                        }

                        return false;
                    }

                    Thread.Sleep(100);
                    nums++;
                    if (nums > 40)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                fs.Close();
            }
        }

        #endregion

        /// <summary>
        /// 使用setReport下下指令并且存储回传值到ReturnValue
        /// </summary>
        /// <param name="command">指令06 00 05...</param>
        /// <param name="length">指令长度</param>
        /// <param name="readData">指定匹配的值</param>
        /// <param name="index">指令返回值索引</param>
        /// <param name="handle">句柄地址</param>
        /// <param name="intPtr">句柄指针</param>
        /// <returns></returns>
        public bool SetReportReturn(string command, int length, string readData, string index, string handle, IntPtr intPtr)
        {
            ReturnValue = "False";
            ReturnAllValue = "False";
            var createFileHandle = MHidUtil.CreateFile(handle, //文件位置
                0x40000000 | 0x80000000, //允许对设备进行读写访问
                0x1 | 0x2, //允许对设备进行共享访问
                IntPtr.Zero, //指向空指针（SECURITY_ATTRIBUTES定义文件的安全特性）
                3, //文件必须已存在
                0x40000000, //允许对文件进行重叠操作
                IntPtr.Zero); //指向空指针（如果不为零，则指定一个文件句柄。新文件将从这个文件中复制扩展属性）
            var readFlag = true;
            var values = "";
            var arrInputReport = new byte[length];
            var endFlag = false;
            var fs = new FileStream(new SafeFileHandle(createFileHandle, false), FileAccess.Read | FileAccess.Write, length, true);
            try
            {
                // if (fs == null)
                // {
                //     readFlag = false;
                //     return false;
                // }

                #region 监听通道回传值

                void AsyRead(IAsyncResult iResult)
                {
                    byte[] arrBuff = (byte[])iResult.AsyncState;
                    if (fs != null)
                    {
                        try
                        {
                            fs.EndRead(iResult);
                        }
                        catch
                        {
                            fs.Close();
                        }
                    }
                    else
                    {
                        readFlag = false;
                        return;
                    }

                    var arrData = readData.Split(' ');
                    for (var i = 0; i < arrData.Length; i++)
                    {
                        if (arrBuff[i] != Convert.ToInt32(arrData[i], 16))
                        {
                            readFlag = false;
                            return;
                        }
                    }

                    var arrIndexes = index.Split(' ');
                    foreach (var a in arrIndexes)
                    {
                        values = values + $"{arrBuff[Convert.ToInt32(a)]:X2}" + " ";
                    }

                    var valueAll = "";
                    for (var i = 0; i < length; i++)
                    {
                        valueAll = values + $"{arrBuff[Convert.ToInt32(i)]:X2}" + " ";
                    }

                    ReturnAllValue = valueAll;
                    values = values.Substring(0, values.Length - 1);
                    endFlag = true;
                }

                #endregion

                fs.BeginRead(arrInputReport, 0, length, AsyRead, arrInputReport);
                Thread.Sleep(20);
                var nums = 0;
                while (true)
                {
                    SetReportSend(command, length, intPtr);
                    if (endFlag)
                    {
                        if (readFlag)
                        {
                            ReturnValue = values.Trim();

                            return true;
                        }

                        return false;
                    }

                    Thread.Sleep(100);
                    nums++;
                    if (nums > 40)
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 使用GetFeature下下指令對外方法并且存储回传值到ReturnValue
        /// </summary>
        /// <param name="command">指令</param>
        /// <param name="length">指令長度</param>
        /// <param name="intPtr">通道</param>
        /// <param name="indexes">返回下標值</param>
        /// <returns>返回读取的值</returns>
        public bool GetFeatureReturn(string command, int length, IntPtr intPtr, string indexes)
        {
            ReturnValue = "False";
            ReturnAllValue = "False";
            Thread.Sleep(20);
            var arr = MConvertUtil.NumberStrToIntArray(indexes);
            Thread.Sleep(20);
            var comm = HexToByteArray(command, length);
            try
            {
                if (HidD_GetFeature(intPtr, comm, length))
                {
                    string ver = "";
                    foreach (int a in arr)
                    {
                        ver = ver + $"{comm[a]:X2}" + " ";
                    }

                    var values = "";
                    for (var i = 0; i < length; i++)
                    {
                        values = values + $"{comm[i]:X2}" + " ";
                    }

                    ReturnValue = ver.Trim();
                    ReturnAllValue = values.Trim();
                    return true;
                }

                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// 使用getReport下指令并且存储回传值到 ReturnValue
        /// </summary>
        /// <param name="command">指令</param>
        /// <param name="length">指令長度</param>
        /// <param name="intPtr">通道</param>
        /// <param name="indexes">返回下標值</param>
        /// <returns></returns>
        public bool GetReportReturn(string command, int length, IntPtr intPtr, string indexes)
        {
            ReturnValue = "False";
            ReturnAllValue = "False";
            Thread.Sleep(20);
            var arr = MConvertUtil.NumberStrToIntArray(indexes);
            Thread.Sleep(20);
            var comm = HexToByteArray(command, length);
            try
            {
                if (HidD_GetInputReport(intPtr, comm, length))
                {
                    string ver = "";
                    foreach (var a in arr)
                    {
                        ver = ver + $"{comm[a]:X2}" + " ";
                    }

                    var values = "";
                    for (var i = 0; i < length; i++)
                    {
                        values = values + $"{comm[i]:X2}" + " ";
                    }

                    ReturnValue = ver.Trim();
                    ReturnAllValue = values.Trim();
                    return true;
                }

                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }


        #region 直接获取指定句柄回传值
        
        /// <summary>
        /// 直接获取指定句柄回传值
        /// </summary>
        /// <param name="handle">句柄地址</param>
        /// <param name="index"></param>
        /// <param name="length">句柄流数组长度</param>
        /// <returns></returns>
        public string IsReturnValue(string handle, string index, int length = 1000)
        {
            IntPtr createFileHandle = MHidUtil.CreateFile(handle, //文件位置
                0x40000000 | 0x80000000, //允许对设备进行读写访问
                0x1 | 0x2, //允许对设备进行共享访问
                IntPtr.Zero, //指向空指针（SECURITY_ATTRIBUTES定义文件的安全特性）
                3, //文件必须已存在
                0x40000000, //允许对文件进行重叠操作
                IntPtr.Zero); //指向空指针（如果不为零，则指定一个文件句柄。新文件将从这个文件中复制扩展属性）

            var deviceRead = new FileStream(new SafeFileHandle(createFileHandle, false), FileAccess.Read | FileAccess.Write, 36, true);
            var result = "False";
            {
                AsyncCallback asyRead = (IAsyncResult iResult) =>
                {
                    byte[] arrBuff = (byte[])iResult.AsyncState;
                    deviceRead.EndRead(iResult);
                    result = MConvertUtil.ByteToHex(arrBuff, index);
                };
                byte[] arrInputReport = new byte[length];
                deviceRead.BeginRead(arrInputReport, 0, length, asyRead, arrInputReport);
                //等待句柄获取回传值
                for (var i = 0; i < 300; i++)
                {
                    if (result != "False") continue;
                    Thread.Sleep(50);
                }
            }
            return result;
        }
        #endregion

        #region 删除驱动(未使用,未测试是否有用)

        /// <summary>
        /// 删除驱动
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private static int DeleteDriver(List<string> pid)
        {
            var hidGuid = Guid.Empty;
            bool result = true;
            uint deviceSerialNumber = 0;
            HidD_GetHidGuid(ref hidGuid);
            int s = 0;
            var hDevInfo = SetupDiGetClassDevs(ref hidGuid, null, IntPtr.Zero, Digcf.DigcfAllclasses | Digcf.DigcfDeviceinterface);
            try
            {
                // 设备信息
                var devData = new SpDeviceInfoData();
                devData.Size = Marshal.SizeOf(devData);
                // 驱动程序信息
                StringBuilder by = new StringBuilder();

                const uint zzz = 0;
                // 遍历设备信息列表
                while (result)
                {
                    // 获取设备信息
                    result = SetupDiEnumDeviceInfo(hDevInfo, deviceSerialNumber, ref devData);
                    if (result)
                    {
                        // 获取设备驱动程序信息
                        SetupDiGetDeviceRegistryProperty(hDevInfo, ref devData, SPDRP.SPDRP_DRIVER,
                            0, by, 2048, zzz);
                        if (!pid.Contains(by.ToString()))
                        {
                            // 删除设备
                            SetupDiRemoveDevice(hDevInfo, ref devData);
                            s++;
                        }
                    }
                    deviceSerialNumber++;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                SetupDiDestroyDeviceInfoList(hDevInfo);
            }

            return s;
        }

        #endregion
    }
}