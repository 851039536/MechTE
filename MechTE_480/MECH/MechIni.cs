using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MechTE_480.MECH
{
    /// <summary>
    /// ini文件操作类
    /// </summary>
    public static class MechIni
    {
        /// <summary>
        /// 读取ini
        /// </summary>
        /// <param name="section">ni文件 [xxxx] 头部标识</param>
        /// <param name="key">key名</param>
        /// <param name="def">如果ini文件中没有前两个参数指定的字段名或键名,则将此值赋给变量</param>
        /// <param name="retVal">得到的值</param>
        /// <param name="size">大小</param>
        /// <param name="iniPath">完整的ini文件名路径</param>
        /// <returns></returns>
        /// 声明INI文件的读操作函数 GetPrivateProfileString()
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
            int size, string iniPath);

        /// <summary>
        /// 写入ini
        /// </summary>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">key名</param>
        /// <param name="val">写入的值</param>
        /// <param name="filePath">完整的ini文件名路径</param>
        /// <returns></returns>
        /// 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        /// <summary>
        /// 写入ini
        /// </summary>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">key名</param>
        /// <param name="value">写入的值</param>
        /// <param name="path">完整的ini文件名路径</param>
        public static void WriteIni(string section, string key, string value, string path)
        {
            // section=配置节点名称，key=键名，value=返回键值，path=路径
            WritePrivateProfileString(section, key, value, path);
        }

        /// <summary>
        /// 读取ini
        /// </summary>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">键名</param>
        /// <param name="path">文件路径</param>
        /// <returns>string</returns>
        public static string ReadIni(string section, string key, string path)
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
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">文件路径</param>
        /// <param name="path">Key</param>
        /// <returns>string[]</returns>
        public static string[] ReadIniArray(string section, string key, string path)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 500, path);
            return temp.ToString().Split(',');
        }

        /// <summary>
        /// 删除一个INI文件
        /// </summary>
        /// <param name="filePath"></param>
        public static void DeleteIni(string filePath)
        {
            File.Delete(filePath);
        }
    }
}