using System;
using System.Collections.Generic;
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
        private static string StringToHidFormat(string value)
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
        private static string AsciiStringToHexString(string value)
        {
            var asciiBytes = Encoding.ASCII.GetBytes(value);
            var hexString = BitConverter.ToString(asciiBytes).Replace("-", "");
            return hexString;
        }
        
        /// <summary>
        /// 将16进制字符转为ASCII字符
        /// </summary>
        /// <param name="hex">16个数字（0-9和A-F）来表示</param>
        /// <returns>ASCII字符</returns>
        private static string HexStringToAsciiString(string hex)
        {
            //判断是否是16进制字符
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("[False]:转换失败不是16进制的字符串");
            }
            var asciiChars = new List<char>();
            for (var i = 0; i < hex.Length; i += 2)
            {
                var hexPair = hex.Substring(i, 2);
                var b = Convert.ToByte(hexPair, 16);
                asciiChars.Add((char)b);
            }
            return new string(asciiChars.ToArray());
        }
    }
}