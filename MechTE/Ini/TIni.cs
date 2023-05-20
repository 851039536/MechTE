using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using IniParser;
using IniParser.Model;

namespace MechTE.Ini {
    /// <summary>
    /// ini文件操作类
    /// </summary>
    public class TIni {
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
        private static extern int GetPrivateProfileString(string section,string key,string def,StringBuilder retVal,int size,string iniPath);

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
        private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);


        /// <summary>
        /// 写入ini
        /// </summary>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">key名</param>
        /// <param name="value">写入的值</param>
        /// <param name="path">完整的ini文件名路径</param>
        public void SetIni(string section,string key,string value,string path) {
            // section=配置节点名称，key=键名，value=返回键值，path=路径
            WritePrivateProfileString(section,key,value,path);
        }
        /// <summary>
        /// 读取ini
        /// </summary>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">键名</param>
        /// <param name="path">文件路径</param>
        /// <returns>string</returns>
        public string GetIni(string section,string key,string path) {
            // 每次从ini中读取多少字节
            StringBuilder temp = new StringBuilder(255);
            // section=配置节点名称，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section,key,"",temp,255,path);
            return temp.ToString();
        }
        /// <summary>
        /// 读取ini string[]
        /// </summary>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">文件路径</param>
        /// <param name="path">Key</param>
        /// <returns>string[]</returns>
        public static string[] GetIniArray(string section,string key,string path) {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section,key,"",temp,500,path);
            return temp.ToString().Split(',');
        }

        /// <summary>
        /// 删除一个INI文件
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteIni(string filePath) {
            File.Delete(filePath);
        }

        #region  查询INI文件所有内容
        /// <summary>
        /// 查询INI文件所有内容
        /// </summary>
        /// <param name="path"></param>
        public static void GetIniAll(string path = @".\CONFIG.ini") {
            var parser = new FileIniDataParser();
            // 这将加载INI文件，读取失败中包含的数据，并解析该数据
            IniData data = parser.ReadFile(path);
            //通过所有的段迭代
            foreach (SectionData section in data.Sections) {
                Console.WriteLine("[" + section.SectionName + "]");
                //遍历当前节中的所有键以打印值
                foreach (KeyData key in section.Keys)
                    Console.WriteLine(key.KeyName + " = " + key.Value);
            }
        }
        #endregion

        #region 读取INI指定值
        /// <summary>
        /// 读取INI指定值 (@".\CONFIG.ini","CONFIG","Delay");
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">Key</param>
        /// <returns>string[]</returns>
        public static string GetIniValue(string path,string section,string key) {
            var parser = new FileIniDataParser();
            // 这将加载INI文件，读取失败中包含的数据，并解析该数据
            IniData data = parser.ReadFile(path);
            return data[section][key];
        }
        #endregion

        #region 更改指定INI值
        /// <summary>
        /// 更改指定INI值
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="section">ini文件 [xxxx] 头部标识</param>
        /// <param name="key">Key</param>
        /// <param name="name">name</param>
        /// <returns></returns>
        public static void UpIniValue(string path,string section,string key,string name) {
            var parser = new FileIniDataParser();
            // 这将加载INI文件，读取失败中包含的数据，并解析该数据
            IniData data = parser.ReadFile(path);
            data[section][key] = name;
            //  parser.SaveFile(path,data);
            parser.WriteFile(path,data,Encoding.UTF8);
        }
        #endregion

        #region 新增INI值
        /// <summary>
        /// 添加section下的键值,如果已存在section则在已存在的section下添加键值
        /// </summary>
        /// <param name="path">ini路径</param>
        /// <param name="section">section</param>
        /// <param name="key">键</param>
        /// <param name="name">值</param>
        public static void AddIniValue(string path,string section,string key,string name) {
            var parser = new FileIniDataParser();
            // 这将加载INI文件，读取失败中包含的数据，并解析该数据
            IniData data = parser.ReadFile(path);
            //添加一个新部分和一些键
            data.Sections.AddSection(section);
            data[section].AddKey(key,name);
            parser.WriteFile(path,data,Encoding.UTF8);

        }
        #endregion

        #region 删除一个键
        /// <summary>
        /// 删除一个键
        /// </summary>
        /// <param name="path">ini路径</param>
        /// <param name="section">section</param>
        /// <param name="key">键</param>
        public static void DelIniValue(string path,string section,string key) {
            var parser = new FileIniDataParser();
            // 这将加载INI文件，读取失败中包含的数据，并解析该数据
            IniData data = parser.ReadFile(path);
            //删除一个键
            data[section].RemoveKey(key);
            parser.WriteFile(path,data,Encoding.UTF8);

        }
        #endregion

        #region 删除“section”部分以及与之关联的所有键和注释
        /// <summary>
        /// 从文件中删除“section”部分以及与之关联的所有键和注释
        /// </summary>
        /// <param name="path">ini路径</param>
        /// <param name="section">section</param>
        public static void DelIniSection(string path,string section) {
            var parser = new FileIniDataParser();
            // 这将加载INI文件，读取失败中包含的数据，并解析该数据
            IniData data = parser.ReadFile(path);
            //从文件中删除“Users”部分以及与之关联的所有键和注释
            data.Sections.RemoveSection(section);
            parser.WriteFile(path,data,Encoding.UTF8);
        }
        #endregion
    }
}
