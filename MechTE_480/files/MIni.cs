using System.IO;
using System.Text;

namespace MechTE_480.Files
{
    /// <summary>
    /// ini文件操作类
    /// </summary>
    public static partial class MIni
    {
        /// <summary>
        /// 读取ini
        /// </summary>
        /// <param name="section">ini文件[xxxx]头部标识</param>
        /// <param name="key">键名</param>
        /// <param name="path">文件路径</param>
        /// <returns>string</returns>
        public static string Read(string section, string key, string path)
        {
            // 每次从ini中读取多少字节
            StringBuilder temp = new StringBuilder(255);
            // section=配置节点名称，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString();
        }

        /// <summary>
        /// 读取ini string[]
        /// </summary>
        /// <param name="section">ini文件[xxxx]头部标识</param>
        /// <param name="key">文件路径</param>
        /// <param name="path">Key</param>
        /// <returns>string[]</returns>
        public static string[] ReadArray(string section, string key, string path)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 500, path);
            return temp.ToString().Split(',');
        }

        /// <summary>
        /// 更新ini
        /// </summary>
        /// <param name="section">ini文件[xxxx]头部标识</param>
        /// <param name="key">key名</param>
        /// <param name="value">写入的值</param>
        /// <param name="path">完整的ini文件名路径</param>
        public static void Update(string section, string key, string value, string path)
        {
            // section=配置节点名称，key=键名，value=返回键值，path=路径
            WritePrivateProfileString(section, key, value, path);
        }


        /// <summary>
        /// 删除一个INI文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void Delete(string filePath)
        {
            File.Delete(filePath);
        }

        /// <summary>
        /// 删除ini文件下所有段落
        /// </summary>
        public static void ClearAllSection(string filePath)
        {
            Update(null,null,null,filePath);
        }

        /// <summary>
        /// 删除ini文件下personal段落下的所有键
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="filePath"></param>
        public static void ClearSection(string Section,string filePath)
        {
            Update(Section,null,null,filePath);
        }
    }
}