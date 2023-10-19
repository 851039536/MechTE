﻿using System;

namespace MechTE_480.Hid
{
    /// <summary>
    /// 获取句柄类
    /// </summary>
    public partial class MechHID
    {
        #region 对外方法

        /// <summary>
        /// 获取双通道装置路径,传入参数不能为空
        /// </summary>
        /// <param name="pid01"></param>
        /// <param name="vid01"></param>
        /// <param name="pid02"></param>
        /// <param name="vid02"></param>
        /// <returns></returns>
        public bool GetHandle(string pid01, string vid01, string pid02, string vid02)
        {
            bool flag;
            try
            {
                for (int i = 0; i < intLen; i++)
                {
                    SetPath1[i] = "";
                    SetPath2[i] = "";
                }

                flag = GetHidDevicePath(pid01, vid01, pid02, vid02);
                for (int i = 0; i < intLen; i++)
                {
                    SetHandle1[i] = GetHidDeviceHandle(SetPath1[i]);
                    SetHandle2[i] = GetHidDeviceHandle(SetPath2[i]);
                }
            }
            catch (Exception)
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// 获取单通道装置路径
        /// </summary>
        /// <param name="pid01"></param>
        /// <param name="vid01"></param>
        /// <returns></returns>
        public bool GetHandle(string pid01, string vid01)
        {
            bool flag;
            try
            {
                for (int i = 0; i < intLen; i++)
                {
                    SetPath1[i] = "";
                }

                flag = GetHidDevicePath(pid01, vid01);
                for (int i = 0; i < intLen; i++)
                {
                    SetHandle1[i] = GetHidDeviceHandle(SetPath1[i]);
                }
            }
            catch (Exception)
            {
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// 获取单通道装置路径
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="vid"></param>
        /// <param name="col">指定col通道 , 特殊情况匹配pid</param>
        /// <returns></returns>
        public bool GetHandle(string pid, string vid, string col)
        {
            bool flag;
            try
            {
                flag = GetHidDevicePath(pid, vid, col);
                // 获取到通道句柄
                Handle = GetHidDeviceHandle(Path);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        #endregion

        #region 释放所有通道句柄和路径

        public void CloseHandle()
        {
            try
            {
                foreach (IntPtr handle in SetHandle1)
                {
                    if (handle != IntPtr.Zero) CloseHandle(handle);
                }

                foreach (IntPtr handle in SetHandle2)
                {
                    if (handle != IntPtr.Zero) CloseHandle(handle);
                }

                for (int i = 0; i < intLen; i++)
                {
                    SetPath1[i] = "";
                    SetPath2[i] = "";
                }
            }
            catch
            {
            }
        }

        #endregion
       
    }
}