namespace MechTE_452.MECH
{
    /// <summary>
    /// 模板操作
    /// </summary>
    public class MechTemplate
    {
        /// <summary>
        /// 检查SN是否是标准品条码
        /// </summary>
        /// <returns></returns>
        private bool IsBZP(string SN)
        {
            if (SN.ToUpper().Contains("_BZP")) return true;
            return false;
        }
    }
}