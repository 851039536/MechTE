using System;
using System.Linq;

namespace MechTE_452.Data
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
        /// <returns>生成-> 0 01 02 03...</returns>  
        public static string GenerateNumberStringSequence(int startNumber, int sequenceLength)
        {
            // 生成一个包含数字字符串的序列                                                                                                                                 
            var strLen = Enumerable.Range(startNumber, sequenceLength).Select(i => i.ToString())
                .Aggregate((a, b) => a + " " + b);
            return strLen;
        }
        
        /// <summary>
        ///  判断字符串是否为空,空等于true，抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <param name="errMsg"></param>
        public static void IsEmptyAssert(string str, string errMsg) {
            
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
        public static void Assert(bool result, string errMsg) {
            if (result) throw new Exception(errMsg);
        }
        
        /// <summary>
        /// 自定义断言方法， func() == true 抛出异常    
        /// </summary>
        /// <param name="func"></param>
        /// <param name="errMsg"></param>
        /// <remarks>系统断言不能在 Release 版保留，用这个方法替代</remarks>
        public static void Assert(Func<bool> func, string errMsg) {
            Assert(func(), errMsg);
        }
    }
}