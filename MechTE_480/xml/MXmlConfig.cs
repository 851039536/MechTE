using System;

namespace MechTE_480.xml
{
    public partial class MXml
    {
        #region 公有属性

        private readonly string _xmlPath;

        /// <summary>
        /// xml路径
        /// </summary>
        public static string PathXml;

        /// <summary>
        /// 方法的节点名称
        /// </summary>
        public static string FunName;

        /// <summary>
        /// 根目录路径
        /// </summary>
        private static readonly string SCurrenPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// xml路径
        /// </summary>
        private string XmlPath
        {
            get { return _xmlPath; }
        }

        #endregion
    }
}