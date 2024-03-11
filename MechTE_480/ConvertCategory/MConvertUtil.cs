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
    public static partial class MConvertUtil
    {
        #region 将字符串转换为整型

        /// <summary>
        /// 将字符串转换为整型，转换失败返回0
        /// </summary>
        public static int ToInt32(string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        /// <summary>
        /// 将字符串转换为整型，转换失败返回0
        /// </summary>
        public static int MToInt32(this string value)
        {
            return int.TryParse(value, out var result) ? result : 0;
        }

        #endregion


        /// <summary>
        /// 将字符串转换为长整型，转换失败返回0
        /// </summary>
        public static long ToInt64(string value)
        {
            return long.TryParse(value, out var result) ? result : 0;
        }

        /// <summary>
        /// 将字符串转换为布尔型，转换失败返回默认值，默认值false
        /// </summary>
        public static bool ToBoolean(string data, bool defValue = false)
        {
            //如果为空则返回默认值
            if (string.IsNullOrEmpty(data))
            {
                return defValue;
            }

            return bool.TryParse(data, out var temp) ? temp : defValue;
        }

        /// <summary>
        /// 将对象转换为布尔型，转换失败返回默认值，默认值false
        /// </summary>
        public static bool ToBoolean(object data, bool defValue = false)
        {
            //如果为空则返回默认值
            if (data == null || Convert.IsDBNull(data))
            {
                return defValue;
            }

            try
            {
                return Convert.ToBoolean(data);
            }
            catch
            {
                return defValue;
            }
        }

        #region 进制转换

        /// <summary>
        /// 实现2,8,10,16进制数间的转换
        /// </summary>
        /// <param name="value">原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                //先转成10进制
                var intValue = Convert.ToInt32(value, from);
                //再转成目标进制
                var ret = Convert.ToString(intValue, to);
                if (to != 2) return ret;
                //获取二进制的长度
                var resultLength = ret.Length;
                ret = resultLength switch
                {
                    7 => "0" + ret,
                    6 => "00" + ret,
                    5 => "000" + ret,
                    4 => "0000" + ret,
                    3 => "00000" + ret,
                    _ => ret
                };
                return ret;
            }
            catch
            {
                return "[False]";
            }
        }

        /// <summary>
        /// 将16进制字符转为ASCII字符
        /// </summary>
        /// <param name="hex">16个数字（0-9和A-F）来表示</param>
        /// <returns></returns>
        public static string HexToAscii(string hex)
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

        /// <summary>
        /// 将16进制字符转为ASCII字符
        /// </summary>
        /// <param name="hex">16个数字（0-9和A-F）来表示</param>
        /// <returns></returns>
        public static string MHexToAscii(this string hex)
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

        # region ASCII字符转为16进制字符

        /// <summary>
        /// ASCII字符转为16进制字符
        /// </summary>
        /// <param name="value">转换值</param>
        /// <returns></returns>
        public static string AsciiStrToHexStr(string value)
        {
            return AsciiStringToHexString(value);
        }

        /// <summary>
        /// ASCII字符转为16进制字符
        /// </summary>
        /// <param name="value">转换值</param>
        /// <returns></returns>
        public static string MAsciiStrToHexStr(this string value)
        {
            return AsciiStringToHexString(value);
        }

        #endregion


        /// <summary>
        /// 将16进制字符转为ASCII 16进制字符
        /// </summary>
        /// <param name="hexStrings">16进制字符</param>
        /// <returns>示例：01 &gt; 30 31</returns>
        public static string HexStrToAsciiHexStr(string hexStrings)
        {
            var asciiBytes = Encoding.ASCII.GetBytes(hexStrings);
            return ByteArr2HexStrings(asciiBytes);
        }

        #endregion

        #region 字符串转换

        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string value, Encoding encoding)
        {
            return encoding.GetBytes(value);
        }

        #endregion

        #region byte[]转换

        /// <summary>
        /// 将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;
            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];
                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);
                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
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

        #endregion

        /// <summary>
        /// 图片转base64
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static string ImgToBase64(string imagePath)
        {
            var imageBytes = File.ReadAllBytes(imagePath);
            var base64String = Convert.ToBase64String(imageBytes);
            Console.WriteLine(base64String);
            return base64String;
        }

        /// <summary>
        /// 示例：[ "AB", "CD", "EF" ] -> "AB{separator}CD{separator}EF"
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        private static string ByteArr2HexStrings(byte[] bytes, string separator = "")
        {
            return ByteArrayToHexStrings(bytes.ToList());
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// 示例：[ "AB", "CD", "EF" ] -> "AB{separator}CD{separator}EF"
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ByteArrayToHexStrings(List<byte> bytes, string separator = "")
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
        /// 将数字类型字符串转int数组
        /// </summary>
        /// <param name="ar">数字字符用空格分割</param>
        /// <returns></returns>
        public static int[] NumberStrToIntArray(string ar)
        {
            // 将输入的字符串按空格分割成一个字符串数组
            var strArray = ar.Split(' ');
            // 将字符串数组转换为整数数组
            var intArray = Array.ConvertAll(strArray, int.Parse);
            // 返回转换后的整数数组
            return intArray;
        }

        /// <summary>
        /// Byte数组转16进制字符串
        /// </summary>
        /// <param name="bytes">Byte数组</param>
        /// <returns>16进制字符串</returns>
        public static string ByteToHex(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                foreach (var t in bytes)
                {
                    returnStr += t.ToString("X2") + " ";
                }
            }

            return returnStr.Trim();
        }

        /// <summary>
        /// 通过给定的索引，从字节数组中提取特定位置的字节，并将其转换为十六进制字符串
        /// </summary>
        /// <param name="bytes">Byte数组</param>
        /// <param name="index">Byte数组索引数组</param>
        /// <returns>16进制字符串</returns>
        public static string ByteToHex(byte[] bytes, string index)
        {
            // 创建一个空字符串，用于存储最终的结果
            string returnStr = "";
            // 检查字节数组是否为空
            if (bytes != null)
            {
                // 将索引字符串按空格分割成一个字符串数组
                var arr = index.Split(' ');

                foreach (var item in arr)
                {
                    // 将当前索引对应的字节转换为十六进制字符串，并添加到结果字符串中
                    returnStr += bytes[Convert.ToInt32(item)].ToString("X2") + " ";
                }
            }

            return returnStr.Trim();
        }


        #region 将字符转换HID指令格式

        /// <summary>
        /// 将字符转换HID指令格式 (name=0021032334 > 00 21 03 23 34)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string ToHidFormat(string value)
        {
            return StringToHidFormat(value);
        }

        /// <summary>
        /// 将字符转换HID指令格式 (name=0021032334 > 00 21 03 23 34)
        /// </summary>
        /// <param name="value"></param>
        /// <returns>string</returns>
        public static string MToHidFormat(this string value)
        {
            return StringToHidFormat(value);
        }

        #endregion
    }
}