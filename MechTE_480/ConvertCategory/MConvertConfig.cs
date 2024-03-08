using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MechTE_480.ConvertCategory
{
    /// <summary>
    /// 处理数据类型转换，数制转换、编码转换相关的类
    /// </summary>    
    partial class MConvertUtil
    {
        /// <summary>
        /// 将字符转换HID指令格式 (name=0021032334 > 00 21 03 23 34)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string StringToHidFormat(string value)
        {
            var splitStrings = new string[value.Length / 2];
            for (int i = 0; i < splitStrings.Length; i++)
            {
                splitStrings[i] = value.Substring(i * 2, 2);
            }
            var formattedStringKey = string.Join(" ", splitStrings);
            return formattedStringKey;
        }
        
        /// <summary>
        /// ASCII字符转为16进制字符
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AsciiStringToHexString(string value)
        {
            var asciiBytes = Encoding.ASCII.GetBytes(value);
            var hexString = BitConverter.ToString(asciiBytes).Replace("-", "");
            return hexString;
        }
    }
}