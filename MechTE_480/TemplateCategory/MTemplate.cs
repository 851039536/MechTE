namespace MechTE_480.TemplateCategory
{
    /// <summary>
    /// 模板程序定制的功能
    /// </summary>
    public static class MTemplate
    {
        /// <summary>
        /// 检查SN是否是标准品条码,自动转换大写
        /// </summary>
        /// <returns></returns>
        public static bool IsBzp(string sn)
        {
            return sn.ToUpper().Contains("BZP");
        }

        /// <summary>
        /// 检查SN是否是标准品条码,自动转换大写
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool MIsBzp(this string value)
        {
            return value.ToUpper().Contains("BZP");
        }

        /// <summary>
        /// 返回字符串True
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MStrTrue(this string value)
        {
            return "True";
        }
    }
}