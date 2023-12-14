using System.Runtime.InteropServices;
using System.Text;

namespace MechTE_480.ini
{
    public static partial class MIni
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
        /// 写入ini.
        /// </summary>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">key名</param>
        /// <param name="val">写入的值</param>
        /// <param name="filePath">完整的ini文件名路径</param>
        /// <returns></returns>
        /// 声明INI文件的写操作函数 WritePrivateProfileString()
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    }
}