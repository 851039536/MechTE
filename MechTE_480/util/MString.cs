using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechTE_480.util
{
    /// <summary>
    /// 字符串操作类
    /// </summary>
    public static class MString
    {

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
        /// 清除字符串中的空格(00 00 00 00 > 00000000)
        /// </summary>
        /// <param name="str"></param>
        /// <returns>string</returns>
        public static string ClearSpaces(string str)
        {
            return str.Replace(" ", "");
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
        /// 将字符转换HID指令格式 (name=0021032334 > 00 21 03 23 34)
        /// </summary>
        /// <param name="name"></param>
        /// <returns>string</returns>
        public static string StringToHidFormat(string name)
        {
            string[] splitStrings = new string[name.Length / 2];
            for (int i = 0; i < splitStrings.Length; i++)
            {
                splitStrings[i] = name.Substring(i * 2, 2);
            }

            string formattedStringKey = string.Join(" ", splitStrings);
            return formattedStringKey;
        }
        
        
        /// <summary>
        /// 生成数字字符串序列(传0,6 生成 0 1 2 3 4 5)
        /// </summary>
        /// <param name="startNumber">序列中第一个整数的值</param>
        /// <param name="sequenceLength">生成的顺序总条数</param>
        /// <returns>string</returns>  
        public static string GenerateNumberSequence(int startNumber, int sequenceLength)
        {
            // 生成一个包含数字字符串的序列                                                                                                                                 
            var strLen = Enumerable.Range(startNumber, sequenceLength).Select(i => i.ToString())
                .Aggregate((a, b) => a + " " + b);
            return strLen;
        }
    }
}