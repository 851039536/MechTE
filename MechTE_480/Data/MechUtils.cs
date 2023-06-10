﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MechTE_480.Data
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class MechUtils
    {
        /// <summary>
        /// 生成数字字符串序列
        /// </summary>
        /// <param name="startNumber">序列中第一个整数的值</param>
        /// <param name="sequenceLength">要生成的顺序总条数</param>
        /// <returns>生成-> 0 1 2 3...</returns>  
        public static string GenerateNumberStringSequence(int startNumber, int sequenceLength)
        {
            // 生成一个包含数字字符串的序列                                                                                                                                 
            var strLen = Enumerable.Range(startNumber, sequenceLength).Select(i => i.ToString())
                .Aggregate((a, b) => a + " " + b);
            return strLen;
        }

        /// <summary>
        /// 将字符串转换为字节数组
        /// 示例："ABCDEF" -> [ 0xAB, 0xCD, 0xEF ]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<byte> StringToByteArray(string str)
        {
            var bytes = new List<byte>();
            for (int i = 0; i < str.Length - 1; i += 2)
            {
                // 将字符串中的每两个字符转换为一个字节
                var byteStr = $"0x{str[i]}{str[i + 1]}";
                bytes.Add(Convert.ToByte(byteStr, 16));
            }

            return bytes;
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// 示例：[ "AB", "CD", "EF" ] -> "AB{separator}CD{separator}EF"
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ByteArr2HexStrings(List<byte> bytes, string separator = "")
        {
            var result = "";
            foreach (var item in bytes)
            {
                // 将字节转换为十六进制字符串，并添加分隔符
                result += $"{item:X2}{separator}";
            }

            // 去除最后一个分隔符
            result = result.Substring(0, result.Length - separator.Length);
            // 返回十六进制字符串
            return result;
        }
        
        /// <summary>
        /// 字符转换HID指令格式 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns>"keyValue=0021032334"->"00 21 03 23 34"</returns>
        public static string CharacterConversionHidFormat(string keyValue)
        {
            string[] splitStrings = new string[keyValue.Length / 2];
            for (int i = 0; i < splitStrings.Length; i++)
            {
                splitStrings[i] = keyValue.Substring(i * 2, 2);
            }

            string formattedStringKey = string.Join(" ", splitStrings);
            return formattedStringKey;
        }

        /// <summary>
        ///  判断字符串是否为空,空等于true，抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <param name="errMsg"></param>
        public static void IsEmptyAssert(string str, string errMsg)
        {
            //如果为空，则等于true，抛出异常
            Assert(string.IsNullOrEmpty(str), errMsg);
        }

        /// <summary>
        /// 自定义断言方法， result == true 抛出异常
        /// </summary>
        /// <param name="result">bool</param>
        /// <param name="errMsg">错误信息</param>
        /// <remarks>系统断言不能在 Release 版保留，用这个方法替代</remarks>
        // ReSharper disable once MemberCanBePrivate.Global
        public static void Assert(bool result, string errMsg)
        {
            if (result) throw new Exception(errMsg);
        }

        /// <summary>
        /// 自定义断言方法， func() == true 抛出异常    
        /// </summary>
        /// <param name="func"></param>
        /// <param name="errMsg"></param>
        /// <remarks>系统断言不能在 Release 版保留，用这个方法替代</remarks>
        public static void Assert(Func<bool> func, string errMsg)
        {
            Assert(func(), errMsg);
        }
    }
}