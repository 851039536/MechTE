using System;
using System.Data;
using System.IO;
using System.Web;
using System.Xml;

namespace MechTE_480.MECH
{
    /// <summary>
    /// XML动态操作类
    /// </summary>
    public class MechXML
    {
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public MechXML()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strPath"></param>
        public MechXML(string strPath)
        {
            _xmlPath = strPath;
        }

        #endregion

        #region 公有属性

        private readonly string _xmlPath;

        /// <summary>
        /// xml路径
        /// </summary>
        public static string PathXml;

        /// <summary>
        /// 方法的节点名称
        /// </summary>
        public static string funName;

        /// <summary>
        /// 根目录路径
        /// </summary>
        private static string CurrenPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// xml路径
        /// </summary>
        private string XmlPath
        {
            get { return _xmlPath; }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 导入XML文件
        /// </summary>
        /// <returns></returns>
        private XmlDocument XmlLoad()
        {
            var xmlFile = XmlPath;
            var xmlDoc = new XmlDocument();
            try
            {
                var filename = AppDomain.CurrentDomain.BaseDirectory + xmlFile;
                if (File.Exists(filename)) xmlDoc.Load(filename);
            }
            catch (Exception)
            {
                // ignored
            }

            return xmlDoc;
        }

        /// <summary>
        /// 导入XML文件
        /// </summary>
        /// <param name="strPath">XML文件路径</param>
        private static XmlDocument XmlLoad(string strPath)
        {
            var xmlDoc = new XmlDocument();
            try
            {
                var filename = AppDomain.CurrentDomain.BaseDirectory + strPath;
                if (File.Exists(filename)) xmlDoc.Load(filename);
            }
            catch (Exception)
            {
                // ignored
            }

            return xmlDoc;
        }

        /// <summary>
        /// 返回完整路径
        /// </summary>
        /// <param name="strPath">Xml的路径</param>
        private static string GetXmlFullPath(string strPath)
        {
            if (strPath.IndexOf(":") > 0)
            {
                return strPath;
            }
            else
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
        }

        #endregion

        #region 读取数据

        /// <summary>
        /// 读取Request中node节点的值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string ReadRequest(string funName, string node, string attribute)
        {
            string value = "";
            try
            {
                var doc = XmlLoad(PathXml);
                var xn = doc.SelectSingleNode($"/CommandBook/{funName}/RequestFormat/{node}");
                value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
            }
            catch
            {
            }

            return value;
        }


        /// <summary>
        /// 获取某一节点的所有孩子节点的值
        /// </summary>
        /// <param name="node">要查询的节点</param>
        public XmlNodeList ReadAllChild(string node)
        {
            var doc = XmlLoad();
            var xn = doc.SelectSingleNode(node);
            var nodelist = xn.ChildNodes; //得到该节点的子节点
            return nodelist;
        }


        /// <summary>
        /// 读取XML返回DataSet
        /// </summary>
        /// <param name="strXmlPath">XML文件相对路径</param>
        public DataSet GetDataSetByXml(string strXmlPath)
        {
            try
            {
                var ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                return ds.Tables.Count > 0 ? ds : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region 插入数据

        /// <summary>
        /// 插入RequestFormat节点数据
        /// </summary>
        /// <param name="element">元素名</param>
        /// <param name="attribute">属性名,如果""则不插值</param>
        /// 使用示列:
        /// ("packet_type","0x05");
        public static string InsertRequestFormat(string element, string attribute)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(CurrenPath + PathXml);

                var xmlNode = doc.SelectSingleNode($"/CommandBook/{funName}/RequestFormat/{element}");
                if (xmlNode != null) return "节点已存在";

                // 选择要插入节点的位置
                var xn = doc.SelectSingleNode($"/CommandBook/{funName}/RequestFormat");
                var xe = doc.CreateElement(element);
                // 设置节点的属性和值

                switch (attribute.Length)
                {
                    case 0:
                        xe.SetAttribute("type", "uint8");
                        break;
                    case 4:
                        xe.SetAttribute("type", "uint8");
                        break;
                    case 6:
                        xe.SetAttribute("type", "uint16");
                        break;
                }

                // xe.SetAttribute("type", type == 1 ? "uint8" : "uint16");
                if (!attribute.Equals(""))
                {
                    xe.SetAttribute("value", attribute);
                }

                // 将新节点添加到指定位置的节点下
                xn.AppendChild(xe);

                //AppendChild指定位置插入
                //InsertBefore在某个节点之前插入
                //InsertAfter在某个节点之后插入
                //ReplaceChild替换某个节点
                //RemoveChild移除某个节点
                //RemoveAll移除所有节点
                //doc.DocumentElement.AppendChild(xe);
                //doc.DocumentElement.InsertBefore(xe, xn);
                doc.Save(CurrenPath + PathXml);
            }
            catch
            {
                // ignored
            }

            return "true";
        }

        /// <summary>
        /// 插入RequestFormat数组节点,
        /// 在需要传指令调用时使用,
        /// 如:传bd:112233445566 根据传入的值进行赋值传入
        /// </summary>
        /// <param name="element"></param>
        public static string InsertRequestFormatResult(string element)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(CurrenPath + PathXml);

                var xmlNode = doc.SelectSingleNode($"/CommandBook/{funName}/RequestFormat/{element}");
                if (xmlNode != null) return "节点已存在";

                // 选择要插入节点的位置
                var xn = doc.SelectSingleNode($"/CommandBook/{funName}/RequestFormat");
                var xe = doc.CreateElement(element);
                // 设置节点的属性和值
                xe.SetAttribute("type", "uint8_array");
                xe.SetAttribute("array_length", "*");

                // 将新节点添加到指定位置的节点下
                xn.AppendChild(xe);

                doc.Save(CurrenPath + PathXml);
            }
            catch
            {
                // ignored
            }

            return "true";
        }

        /// <summary>
        /// 插入RequestFormat数组节点
        /// </summary>
        /// <param name="element"></param>
        public static string InsertResponseFormatResult(string element)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(CurrenPath + PathXml);


                var xmlNode = doc.SelectSingleNode($"/CommandBook/{funName}/ResponseFormat/{element}");
                if (xmlNode != null) return "节点已存在";

                // 选择要插入节点的位置
                var xn = doc.SelectSingleNode($"/CommandBook/{funName}/ResponseFormat");
                var xe = doc.CreateElement(element);
                // 设置节点的属性和值
                xe.SetAttribute("type", "uint8_array");
                xe.SetAttribute("array_length", "*");

                // 将新节点添加到指定位置的节点下
                xn.AppendChild(xe);

                doc.Save(CurrenPath + PathXml);
            }
            catch
            {
            }

            return "true";
        }

        /// <summary>
        /// 插入ResponseFormat节点数据 ,  响应匹配到的节点值
        /// </summary>
        /// <param name="element">元素名</param>
        /// <param name="attribute">属性名,如果""则不插值</param>
        /// 使用示列:
        /// ("packet_type","0x05");
        public static string InsertResponseFormat(string element, string attribute)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(CurrenPath + PathXml);

                var xmlNode = doc.SelectSingleNode($"/CommandBook/{funName}/ResponseFormat/{element}");
                if (xmlNode != null) return "节点已存在";

                // 选择要插入节点的位置
                var xn = doc.SelectSingleNode($"/CommandBook/{funName}/ResponseFormat");
                var xe = doc.CreateElement(element);
                // 设置节点的属性和值
                switch (attribute.Length)
                {
                    case 0:
                        xe.SetAttribute("type", "uint8");
                        break;
                    case 4:
                        xe.SetAttribute("type", "uint8");
                        break;
                    case 6:
                        xe.SetAttribute("type", "uint16");
                        break;
                }

                if (!attribute.Equals(""))
                {
                    xe.SetAttribute("value", attribute);
                }

                // 将新节点添加到指定位置的节点下
                xn.AppendChild(xe);


                doc.Save(CurrenPath + PathXml);
            }
            catch
            {
            }

            return "true";
        }


        /// <summary>
        /// 初始化节点
        /// </summary>
        /// <returns></returns>
        public static string InitializeTheNode()
        {
            try
            {
                var doc = new XmlDocument();

                // doc.Load(AppDomain.CurrentDomain.BaseDirectory + path);
                // XmlNodeList commandBookNodes = doc.GetElementsByTagName("CommandBook");
                // var ret = doc.SelectSingleNode("/CommandBook");
                // if (commandBookNodes.Count > 0)
                // {
                //     return "节点已存在";
                // }

                var v = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                // 创建根节点 <CommandBook>
                var commandBookElement = doc.CreateElement("CommandBook");

                // 将根节点 <CommandBook> 添加到文档
                doc.AppendChild(v);
                doc.AppendChild(commandBookElement);

                doc.Save(CurrenPath + PathXml);
            }

            catch
            {
                return "异常";
            }

            return "true";
        }

        /// <summary>
        /// 设定方法属性,创建节点对应的方法
        /// RequestFormat,
        /// ResponseFormat
        /// </summary>
        public static string CreateChildNode()
        {
            try
            {
                //每次创建节点之前先删除对应的节点
                Delete(PathXml, funName);

                var doc = new XmlDocument();
                doc.Load(CurrenPath + PathXml);

                var xmlNode = doc.SelectSingleNode($"/CommandBook/{funName}");
                if (xmlNode != null) return "节点已存在";

                // 选择要插入节点的位置
                var xn = doc.SelectSingleNode("/CommandBook");
                var xe = doc.CreateElement(funName);

                // 创建子节点 <RequestFormat>
                var requestFormatElement = doc.CreateElement("RequestFormat");
                xe.AppendChild(requestFormatElement);

                // 创建子节点 <ResponseFormat>
                var responseFormatElement = doc.CreateElement("ResponseFormat");
                xe.AppendChild(responseFormatElement);

                xn.AppendChild(xe);
                doc.AppendChild(xn);
                doc.Save(CurrenPath + PathXml);
            }

            catch
            {
                // ignored
            }

            return "true";
        }

        #endregion

        #region 修改数据

        /// <summary>
        /// 修改Request指定节点的属性值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="node">节点,传入方法名</param>
        /// <param name="element">节点,传入属性名称</param>
        /// <param name="value">更新的值</param>
        public static void UpdateRequest(string path, string node, string element, string value)
        {
            try
            {
                var doc = XmlLoad(path);
                var xn = doc.SelectSingleNode("/CommandBook/" + node + "/RequestFormat/" + element);
                var xe = (XmlElement)xn;
                xe.SetAttribute("value", value);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
            }
            catch
            {
                // ignored
            }
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除节点值
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点,传入方法名</param>
        public static void Delete(string path, string node)
        {
            try
            {
                var doc = XmlLoad(path);
                var xn = doc.SelectSingleNode("/CommandBook/" + node);
                xn.ParentNode.RemoveChild(xn);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
            }
            catch
            {
                // ignored
            }
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="node">节点</param>
        /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
        /// 使用示列:
        /// XMLProsess.Delete(path, "/Node", "")
        /// XMLProsess.Delete(path, "/Node", "Attribute")
        public static void Delete(string path, string node, string attribute)
        {
            try
            {
                XmlDocument doc = XmlLoad(path);
                XmlNode xn = doc.SelectSingleNode(node);
                XmlElement xe = (XmlElement)xn;
                if (attribute.Equals(""))
                    xn.ParentNode.RemoveChild(xn);
                else
                    xe.RemoveAttribute(attribute);
                doc.Save(AppDomain.CurrentDomain.BaseDirectory + path);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 删除所有行
        /// </summary>
        /// <param name="strXmlPath">XML路径</param>
        public static bool DeleteXmlAllRows(string strXmlPath)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows.Clear();
                }

                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 通过删除DataSet中指定索引行，重写XML以实现删除指定行
        /// </summary>
        /// <param name="strXmlPath"></param>
        /// <param name="iDeleteRow">要删除的行在DataSet中的Index值</param>
        public static bool DeleteXmlRowByIndex(string strXmlPath, int iDeleteRow)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Rows[iDeleteRow].Delete();
                }

                ds.WriteXml(GetXmlFullPath(strXmlPath));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定列中指定值的行
        /// </summary>
        /// <param name="strXmlPath">XML相对路径</param>
        /// <param name="strColumn">列名</param>
        /// <param name="columnValue">指定值</param>
        public static bool DeleteXmlRows(string strXmlPath, string strColumn, string[] columnValue)
        {
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(GetXmlFullPath(strXmlPath));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //判断行多还是删除的值多，多的for循环放在里面
                    if (columnValue.Length > ds.Tables[0].Rows.Count)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            for (int j = 0; j < columnValue.Length; j++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(columnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int j = 0; j < columnValue.Length; j++)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(columnValue[j]))
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                }
                            }
                        }
                    }

                    ds.WriteXml(GetXmlFullPath(strXmlPath));
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}