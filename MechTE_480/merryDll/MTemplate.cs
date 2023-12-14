namespace MechTE_480.merryDll
{
    /// <summary>
    /// 模板程序定制的功能
    /// </summary>
    public class MTemplate
    {
        /// <summary>
        /// 检查SN是否是标准品条码,自动转换大写
        /// </summary>
        /// <returns></returns>
        public static bool IsBzp(string sn)
        {
            return sn.ToUpper().Contains("_BZP");
        }
    }
}