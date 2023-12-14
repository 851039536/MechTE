using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MechTE_452.MECH
{
    /// <summary>
    /// 字符串操作类
    /// </summary>
    public class MString
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
        /// 清除字符串中的空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns>00 00 00 00 > 00000000</returns>
        public static string ClearStringSpaces(string str)
        {
            return str.Replace(" ", "");
        }
        

  
        
    }
}