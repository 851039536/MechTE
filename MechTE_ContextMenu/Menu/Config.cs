using System.IO;
using System.Reflection;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace MechTE_ContextMenu.Menu
{
    public class Config
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
        /// 获取当前dll所在路径
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            // 获取当前程序集的代码基路径
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            // 创建一个UriBuilder对象，用于解析代码基路径
            var uri = new UriBuilder(codeBase);
            // 获取解析后的路径，并对路径中的特殊字符进行解码
            var path = Uri.UnescapeDataString(uri.Path);
            // 获取解析后的路径，并对路径中的特殊字符进行解码
            return Path.GetDirectoryName(path);
        }
    }
}