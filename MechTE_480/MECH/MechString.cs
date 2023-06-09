using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechTE_480.MECH
{
    /// <summary>
    /// 字符串操作类
    /// </summary>
    public static class MechString
    {
        
        /// <summary>
        /// 将16进制字符串转为ASCII16进制字符串
        /// </summary>
        /// <returns>示例：01 > 3031</returns>
        public static string HexStrings2AsciiHexStrings(string hexStrings)
        {
            var asciiBytes = Encoding.ASCII.GetBytes(hexStrings);
            return ByteArr2HexStrings(asciiBytes);
        }

        /// <summary>
        /// 示例：[ "AB", "CD", "EF" ] -> "AB{separator}CD{separator}EF"
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        private static string ByteArr2HexStrings(byte[] bytes, string separator = "")
        {
            return MechUtils.ByteArrayToHexStrings(bytes.ToList());
        }
        
        /// <summary>
        /// 将字符按2个长度为一组进行反序
        /// </summary>
        /// <param name="str">11223344</param>
        /// <returns>44332211->11223344</returns>
        public static string Reverse(string str)
        {
            //使用StringBuilder代替字符串拼接，避免了频繁的内存分配和拷贝，提高了代码的效率
            var newStr = new StringBuilder();
            // 从字符串的倒数第二个字符开始循环，每次减少2个字符
            for (var i = str.Length - 2; i >= 0; i -= 2)
            {
                // 将每两个字符添加到新字符串变量中
                newStr.Append(str.Substring(i, 2));
            }

            return newStr.ToString();
        }
        
        /// <summary>
        /// 清除字符串中的空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns>00 00 00 00 > 00000000</returns>
        public static string ClearStringSpaces(string str)
        {
            return str.Replace(" ", "");
        }
        
        /// <summary>
        /// 将16进制字符转为ASCII字符
        /// </summary>
        /// <param name="hex">16个数字（0-9和A-F）来表示</param>
        /// <returns></returns>
        public static string HexadecimalToASCII(string hex)
        {
            //判断是否是16进制字符
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("不是16进制字符");
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

        /// <summary>
        /// ASCII字符转为16进制字符
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ASCIIConvertsDecimal16(string name)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(name);
            string hexString = BitConverter.ToString(asciiBytes).Replace("-", "");
            return hexString;
        }
  
        
    }
}