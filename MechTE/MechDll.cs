using MechTE.Cmd;

namespace MechTE {
    /// <summary>
    /// 调试模块
    /// </summary>
    public class MechDll
    {
        private string _strPara;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MechDll(string strPara)
        {
            _strPara = strPara;
        }
        /// <summary>
        /// 开始测试
        /// </summary>
        /// <returns></returns>
        public string Run(string name)
        {
            string[] str = name.Split('-');
            switch (str[0])
            {
                case"cmd" :
                    return TCmd.Exe(str[1]);
                default:
                    break;
            }
            return "Run False";
        }
    }
}
