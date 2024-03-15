using System.Text.RegularExpressions;
using MechTE_480.util;

namespace MechTE_480.RegexsCategory
{
    /// <summary>
    /// 操作正则表达式的常用类
    /// </summary>
    public class MRegexUtil
    {
        #region 验证输入字符串是否与模式字符串匹配
        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件:可选</param>
        public static bool IsMatch(string input,string pattern,RegexOptions options = RegexOptions.IgnoreCase)
        {
            return Regex.IsMatch(input,pattern,options);
        }
        #endregion
        
        /// <summary>
        /// 验证EMail是否合法
        /// </summary>
        /// <param name="email">要验证的Email</param>
        public static bool IsEmail(string email)
        {
            //如果为空，认为验证不合格
            if (MStringUtil.IsNullOrEmpty(email))
            {
                return false;
            }
            //清除要验证字符串中的空格
            email = email.Trim();
            //模式字符串
            string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            //验证
            return MRegexUtil.IsMatch(email,pattern);
        }
        
    }
}
