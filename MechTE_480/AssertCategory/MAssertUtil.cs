using System;

namespace MechTE_480.AssertCategory
{
    /// <summary>
    /// 自定义断言类 , throw new Exception 异常
    /// </summary>
    public static class MAssertUtil
    {
        /// <summary>
        /// 判断字符串是否为空，如为空则抛出异常
        /// </summary>
        /// <param name="value">需要判断的值</param>
        /// <param name="errMsg">异常提示</param>
        public static void IsEmpty(string value, string errMsg)
        {
            //如果为空，则等于true，抛出异常
            Assert(string.IsNullOrEmpty(value), errMsg);
        }

        /// <summary>
        /// result == true抛出异常
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

        /// <summary>
        /// 直接报错误提示
        /// </summary>
        /// <param name="errMsg"></param>
        /// <exception cref="Exception"></exception>
        public static void Assert(string errMsg)
        {
            throw new Exception(errMsg);
        }
    }
}