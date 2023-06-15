namespace MechTE_452.MECH
{
    /// <summary>
    /// 模板操作
    /// </summary>
    public class MechTemplate
    {
        /// <summary>
        /// 检查SN是否是标准品条码
        /// 转换大写
        /// </summary>
        /// <returns></returns>
        public static bool IsBzpToUpper(string sn)
        {
            if (sn.ToUpper().Contains("_BZP")) return true;
            return false;
        }

        /// <summary>
        /// 检查SN是否是标准品条码
        /// 转换小写
        /// </summary>
        /// <returns></returns>
        public static bool IsBzpToLower(string sn)
        {
            if (sn.ToLower().Contains("_BZP")) return true;
            return false;
        }
    }
}