namespace MechTE_480.MECH
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
        public static bool IsBzpToUpper(string SN)
        {
            if (SN.ToUpper().Contains("_BZP")) return true;
            return false;
        }
    }
}