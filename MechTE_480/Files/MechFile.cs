using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MechTE_480.Files
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class MechFile
    {

        #region 打开程序/文件夹
        /// <summary>
        /// 使用DllImport指定调用的Windows API函数及其相关信息
        /// </summary>
        /// <param name="hwnd">指定父窗口句柄:ntPtr.Zero</param>
        /// <param name="lpszOp">指定要进行的操作:Open</param>
        /// <param name="lpszFile">指定要打开的文件名|路径</param>
        /// <param name="lpszParams">指定命令行参数: 0 | ""</param>
        /// <param name="lpszDir">用于指定默认目录:0 | ""</param>
        /// <param name="fsShowCmd">显示模式: 0:隐藏 1~11</param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        private static extern int ShellExecute(IntPtr hwnd,StringBuilder lpszOp,StringBuilder lpszFile,StringBuilder lpszParams,StringBuilder lpszDir,int fsShowCmd);

        /// <summary>
        /// 开启程序/文件夹
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="fsShow">是否显示窗口 默认显示(1)</param>
        public static void OpenFile(string path,int fsShow = 1)
        {
            ShellExecute(IntPtr.Zero,
                new StringBuilder("Open"),// 打开方式为“Open”
                new StringBuilder(@path),// 文件路径
                new StringBuilder(""),// 命令行参数为空
                new StringBuilder(""),// 工作目录为空
                fsShow);// 是否显示窗口，默认显示
        }

        /// <summary>
        /// 使用系统进程打开程序/文件夹
        /// </summary>
        /// <param name="path">路径</param>
        public static void OpenFile(string path)
        {
            System.Diagnostics.Process.Start(@path);
        }
        #endregion

        #region 复制文件夹及文件

        /// <summary>
        /// 复制文件夹及文件
        /// </summary>
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        /// <returns>1 || -1</returns>
        public static int CopyFile(string sourceFolder,string destFolder)
        {
            try {
                //如果目标路径不存在,则创建目标路径
                if (!Directory.Exists(destFolder)) {
                    Directory.CreateDirectory(destFolder);
                }
                //得到原文件根目录下的所有文件
                string[] files = Directory.GetFiles(sourceFolder);
                foreach (string file in files) {
                    string name = Path.GetFileName(file);
                    string dest = Path.Combine(destFolder,name);
                    File.Copy(file,dest,true);//复制文件
                }
                //得到原文件根目录下的所有文件夹
                string[] folders = Directory.GetDirectories(sourceFolder);
                foreach (string folder in folders) {
                    string name = Path.GetFileName(folder);
                    string dest = Path.Combine(destFolder,name);
                    CopyFile(folder,dest);//构建目标路径,递归复制文件
                }
                return 1;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return -1;
            }
        }
        #endregion

        #region 浏览文件对话框
        /// <summary>
        /// 窗体浏览文件对话框
        /// </summary>
        /// <returns>string</returns>
        public static string FileDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog {
                //打开的文件夹浏览对话框上的描述
                Description = @"请选择文件夹",
                //是否显示对话框左下角 新建文件夹 按钮，默认为 true
                ShowNewFolderButton = false
            };
            if (dialog.ShowDialog() == DialogResult.OK) {
                if (!string.IsNullOrEmpty(dialog.SelectedPath)) {
                    return dialog.SelectedPath;
                }
            } else {
                return "false";
            }
            return "FileDialog false";
        }

        #endregion

        #region 获取指定目录文件夹
        /// <summary>
        /// 获取指定目录文件夹
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>文件名,文件路径</returns>
        public static List<string> QueryFile(string path)
        {
            var result = new List<string>();
            try {
                DirectoryInfo theFolder = new DirectoryInfo(path);
                DirectoryInfo[] dirInfo = theFolder.GetDirectories();//获取所在目录的文件夹
                //遍历文件夹
                foreach (DirectoryInfo nextFolder in dirInfo) {
                    if (!nextFolder.FullName.Contains("$RECYCLE")) {
                        result.Add(nextFolder.Name + "," + $"{path}\\{nextFolder.Name}");
                    }
                }
            } catch (Exception ex) {
                throw ex;
            }
            return result;
        }


        #endregion

        #region 文件删除
        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool DelFile(string path)
        {
            if (File.Exists(path)) {
                File.Delete(path);
                return true;
            }
            return false;
        }
        #endregion

        #region 检测指定目录是否存在
        /// <summary>
        /// 检测指定目录是否存在
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        /// <returns></returns>
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        #endregion

        #region 检测指定文件是否存在
        /// <summary>
        /// 检测指定文件是否存在
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
        #endregion

        #region 获取指定目录中的文件列表
        /// <summary>
        /// 获取指定目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath)) {
                throw new FileNotFoundException();
            }

            //获取文件列表
            return Directory.GetFiles(directoryPath);
        }
        #endregion

        #region 获取指定目录中所有子目录列表
        /// <summary>
        /// 获取指定目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static string[] GetDirectories(string directoryPath)
        {
            try {
                return Directory.GetDirectories(directoryPath);
            } 
            catch (IOException ex) {
                throw ex;
            }
        }
        #endregion

        #region 获取指定目录及子目录中所有文件列表
        /// <summary>
        /// 获取指定目录及子目录中所有文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetFileNames(string directoryPath,string searchPattern,bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath)) {
                throw new FileNotFoundException();
            }

            try {
                if (isSearchChild) {
                    return Directory.GetFiles(directoryPath,searchPattern,SearchOption.AllDirectories);
                } else {
                    return Directory.GetFiles(directoryPath,searchPattern,SearchOption.TopDirectoryOnly);
                }
            } catch (IOException ex) {
                throw ex;
            }
        }
        #endregion

        #region 检测指定目录是否为空
        /// <summary>
        /// 检测指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try {
                //判断是否存在文件
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0) {
                    return false;
                }

                //判断是否存在文件夹
                string[] directoryNames = GetDirectories(directoryPath);
                if (directoryNames.Length > 0) {
                    return false;
                }

                return true;
            } catch {
                //这里记录日志
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return true;
            }
        }
        #endregion

        #region 检测指定目录中是否存在指定的文件
        /// <summary>
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>        
        public static bool Contains(string directoryPath,string searchPattern)
        {
            try {
                //获取指定的文件列表
                string[] fileNames = GetFileNames(directoryPath,searchPattern,false);

                //判断指定文件是否存在
                if (fileNames.Length == 0) {
                    return false;
                } else {
                    return true;
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// 检测指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static bool Contains(string directoryPath,string searchPattern,bool isSearchChild)
        {
            try {
                //获取指定的文件列表
                string[] fileNames = GetFileNames(directoryPath,searchPattern,true);

                //判断指定文件是否存在
                if (fileNames.Length == 0) {
                    return false;
                } else {
                    return true;
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }
        #endregion

        #region 复制文件夹
        /// <summary>
        /// 复制文件夹(递归)
        /// </summary>
        /// <param name="varFromDirectory">源文件夹路径</param>
        /// <param name="varToDirectory">目标文件夹路径</param>
        public static void CopyFolder(string varFromDirectory,string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0) {
                foreach (string d in directories) {
                    CopyFolder(d,varToDirectory + d.Substring(d.LastIndexOf("\\", StringComparison.Ordinal)));
                }
            }
            string[] files = Directory.GetFiles(varFromDirectory);
            if (files.Length > 0) {
                foreach (string s in files) {
                    File.Copy(s,varToDirectory + s.Substring(s.LastIndexOf("\\", StringComparison.Ordinal)),true);
                }
            }
        }
        #endregion

        #region 检查文件,如果文件不存在则创建
        /// <summary>
        /// 检查文件,如果文件不存在则创建  
        /// </summary>
        /// <param name="filePath">路径,包括文件名</param>
        public static void ExistsFile(string filePath)
        {
            if (!File.Exists(filePath)) {
                FileStream fs = File.Create(filePath);
                fs.Close();
            }
        }
        #endregion

        #region 删除指定文件夹对应其他文件夹里的文件
        /// <summary>
        /// 删除指定文件夹对应其他文件夹里的文件
        /// </summary>
        /// <param name="varFromDirectory">指定文件夹路径</param>
        /// <param name="varToDirectory">对应其他文件夹路径</param>
        public static void DeleteFolderFiles(string varFromDirectory,string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);
            if (!Directory.Exists(varFromDirectory)) return;
            string[] directories = Directory.GetDirectories(varFromDirectory);
            if (directories.Length > 0) {
                foreach (string d in directories) {
                    DeleteFolderFiles(d,varToDirectory + d.Substring(d.LastIndexOf("\\", StringComparison.Ordinal)));
                }
            }
            string[] files = Directory.GetFiles(varFromDirectory);
            if (files.Length > 0) {
                foreach (string s in files) {
                    File.Delete(varToDirectory + s.Substring(s.LastIndexOf("\\", StringComparison.Ordinal)));
                }
            }
        }
        #endregion

        #region 从文件的绝对路径中获取文件名( 包含扩展名 )
        /// <summary>
        /// 从文件的绝对路径中获取文件名( 包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetFileName(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
        }
        #endregion

        #region 创建一个目录
        /// <summary>
        /// 创建一个目录
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!IsExistDirectory(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }
        }
        #endregion

        #region 创建一个文件
        /// <summary>
        /// 创建一个文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath)) {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //关闭文件流
                    fs.Close();
                }
            } catch (Exception ex) {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 创建一个文件,并将字节流写入文件。
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        public static void CreateFile(string filePath,byte[] buffer)
        {
            try {
                //如果文件不存在则创建该文件
                if (!IsExistFile(filePath)) {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);

                    //创建文件
                    FileStream fs = file.Create();

                    //写入二进制流
                    fs.Write(buffer,0,buffer.Length);

                    //关闭文件流
                    fs.Close();
                }
            } catch (Exception ex) {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }
        #endregion

        #region 获取文本文件的行数
        /// <summary>
        /// 获取文本文件的行数
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static int GetLineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中
            string[] rows = File.ReadAllLines(filePath);

            //返回行数
            return rows.Length;
        }
        #endregion

        #region 获取一个文件的长度
        /// <summary>
        /// 获取一个文件的长度,单位为Byte
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static int GetFileSize(string filePath)
        {
            //创建一个文件对象
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小
            return (int)fi.Length;
        }
        #endregion

        #region 获取指定目录中的子目录列表
        /// <summary>
        /// 获取指定目录及子目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectories(string directoryPath,string searchPattern,bool isSearchChild)
        {
            try {
                if (isSearchChild) {
                    return Directory.GetDirectories(directoryPath,searchPattern,SearchOption.AllDirectories);
                } else {
                    return Directory.GetDirectories(directoryPath,searchPattern,SearchOption.TopDirectoryOnly);
                }
            } catch (IOException ex) {
                throw ex;
            }
        }
        #endregion

        #region 向文本文件写入内容
        /// <summary>
        /// 向文本文件中写入内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="text">写入的内容</param>
        /// <param name="encoding">编码</param>
        public static void WriteText(string filePath,string text,Encoding encoding)
        {
            //向文件写入内容
            File.WriteAllText(filePath,text,encoding);
        }
        #endregion

        #region 向文本文件的尾部追加内容
        /// <summary>
        /// 向文本文件的尾部追加内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="content">写入的内容</param>
        public static void AppendText(string filePath,string content)
        {
            File.AppendAllText(filePath,content);
        }
        #endregion

        #region 将现有文件的内容复制到新文件中
        /// <summary>
        /// 将源文件的内容复制到目标文件中
        /// </summary>
        /// <param name="sourceFilePath">源文件的绝对路径</param>
        /// <param name="destFilePath">目标文件的绝对路径</param>
        public static void Copy(string sourceFilePath,string destFilePath)
        {
            File.Copy(sourceFilePath,destFilePath,true);
        }
        #endregion

        #region 从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// <summary>
        /// 从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetFileNameNoExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Name.Split('.')[0];
        }
        #endregion

        #region 从文件的绝对路径中获取扩展名
        /// <summary>
        /// 从文件的绝对路径中获取扩展名
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Extension;
        }
        #endregion

        #region 清空文件内容
        /// <summary>
        /// 清空文件内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void ClearFile(string filePath)
        {
            //删除文件
            File.Delete(filePath);
            //重新创建该文件
            CreateFile(filePath);
        }
        #endregion

        #region 删除指定目录
        /// <summary>
        /// 删除指定目录及其所有子目录
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void DeleteDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath)) {
                Directory.Delete(directoryPath,true);
            }
        }
        #endregion

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="strings">文件内容</param>
        public static void WriteFile(string path,string strings)
        {
            if (!File.Exists(path)) {
                var f = File.Create(path);
                f.Close();
                f.Dispose();
            }
            var f2 = new StreamWriter(path,true,Encoding.UTF8);
            f2.WriteLine(strings);
            f2.Close();
            f2.Dispose();
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string path)
        {
            string s;
            if (!File.Exists(path))
                s = "不存在相应的目录";
            else {
                StreamReader f2 = new StreamReader(path,Encoding.GetEncoding("gb2312"));
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }
            return s;
        }

        #region 追加文件
        /****************************************
         * 函数名称：FileAdd
         * 功能说明：追加文件内容
         * 参    数：Path:文件路径,strings:内容
         * 调用示列：
         *           string Path = Server.MapPath("Default2.aspx");     
         *           string Strings = "新追加内容";
         *           DotNet.Utilities.FileOperate.FileAdd(Path, Strings);
        *****************************************/
        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="strings">内容</param>
        public static void FileAdd(string path,string strings)
        {
            var sw = File.AppendText(path);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        #endregion

        #region 拷贝文件
        /****************************************
         * 函数名称：FileCopy
         * 功能说明：拷贝文件
         * 参    数：OrignFile:原始文件,NewFile:新文件路径
         * 调用示列：
         *           string OrignFile = Server.MapPath("Default2.aspx");     
         *           string NewFile = Server.MapPath("Default3.aspx");
         *           DotNet.Utilities.FileOperate.FileCopy(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="orignFile">原始文件</param>
        /// <param name="newFile">新文件路径</param>
        public static void FileCopy(string orignFile,string newFile)
        {
            File.Copy(orignFile,newFile,true);
        }

        #endregion

        #region 删除文件
        /****************************************
         * 函数名称：FileDel
         * 功能说明：删除文件
         * 参    数：Path:文件路径
         * 调用示列：
         *           string Path = Server.MapPath("Default3.aspx");    
         *           DotNet.Utilities.FileOperate.FileDel(Path);
        *****************************************/
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">路径</param>
        public static void FileDel(string path)
        {
            File.Delete(path);
        }
        #endregion

        #region 移动文件
        /****************************************
         * 函数名称：FileMove
         * 功能说明：移动文件
         * 参    数：OrignFile:原始路径,NewFile:新文件路径
         * 调用示列：
         *            string OrignFile = Server.MapPath("../说明.txt");    
         *            string NewFile = Server.MapPath("../../说明.txt");
         *            DotNet.Utilities.FileOperate.FileMove(OrignFile, NewFile);
        *****************************************/
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="orignFile">原始路径</param>
        /// <param name="newFile">新路径</param>
        public static void FileMove(string orignFile,string newFile)
        {
            File.Move(orignFile,newFile);
        }
        #endregion

        #region 在当前目录下创建目录
        /****************************************
         * 函数名称：FolderCreate
         * 功能说明：在当前目录下创建目录
         * 参    数：OrignFolder:当前目录,NewFolder:新目录
         * 调用示列：
         *           string OrignFolder = Server.MapPath("test/");    
         *           string NewFloder = "new";
         *           DotNet.Utilities.FileOperate.FolderCreate(OrignFolder, NewFolder); 
        *****************************************/
        /// <summary>
        /// 在当前目录下创建目录
        /// </summary>
        /// <param name="orignFolder">当前目录</param>
        /// <param name="newFloder">新目录</param>
        public static void FolderCreate(string orignFolder,string newFloder)
        {
            Directory.SetCurrentDirectory(orignFolder);
            Directory.CreateDirectory(newFloder);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void FolderCreate(string path)
        {
            // 判断目标目录是否存在如果不存在则新建之
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        #endregion

        #region 创建目录
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public static void FileCreate(string path)
        {
            var createFile = new FileInfo(path); //创建文件 
            if (!createFile.Exists) {
                var fs = createFile.Create();
                fs.Close();
            }
        }
        #endregion

        #region 递归删除文件夹目录及文件
        /****************************************
         * 函数名称：DeleteFolder
         * 功能说明：递归删除文件夹目录及文件
         * 参    数：dir:文件夹路径
         * 调用示列：
         *           string dir = Server.MapPath("test/");  
         *           DotNet.Utilities.FileOperate.DeleteFolder(dir);       
        *****************************************/
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir"></param>  
        /// <returns></returns>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir)) {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件                        
                    else
                        DeleteFolder(d); //递归删除子文件夹 
                }
                Directory.Delete(dir,true); //删除已空文件夹                 
            }
        }

        #endregion

        #region 将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
        /****************************************
         * 函数名称：CopyDir
         * 功能说明：将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
         * 参    数：srcPath:原始路径,aimPath:目标文件夹
         * 调用示列：
         *           string srcPath = Server.MapPath("test/");  
         *           string aimPath = Server.MapPath("test1/");
         *           DotNet.Utilities.FileOperate.CopyDir(srcPath,aimPath);   
        *****************************************/
        /// <summary>
        /// 指定文件夹下面的所有内容copy到目标文件夹下面
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void CopyDir(string srcPath,string aimPath)
        {
            try {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                //遍历所有的文件和目录
                foreach (string file in fileList) {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

                    if (Directory.Exists(file))
                        CopyDir(file,aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file,aimPath + Path.GetFileName(file),true);
                }
            } catch (Exception ee) {
                throw new Exception(ee.ToString());
            }
        }
        #endregion

        #region 获取指定文件夹下所有子目录及文件(树形)
        /****************************************
         * 函数名称：GetFoldAll(string Path)
         * 功能说明：获取指定文件夹下所有子目录及文件(树形)
         * 参    数：Path:详细路径
         * 调用示列：
         *           string strDiarist = Server.MapPath("templates");       
         *           this.Literal1.Text = DotNet.Utilities.FileOperate.GetFoldAll(strDiarist);  
        *****************************************/
        /// <summary>
        /// 获取指定文件夹下所有子目录及文件
        /// </summary>
        /// <param name="path">详细路径</param>
        public static string GetFoldAll(string path)
        {
            string str = "";
            DirectoryInfo thisOne = new DirectoryInfo(path);
            str = ListTreeShow(thisOne,0,str);
            return str;
        }
        #endregion
        /// <summary>
        /// 获取指定文件夹下所有子目录及文件函数
        /// </summary>
        /// <param name="theDir">指定目录</param>
        /// <param name="nLevel">默认起始值,调用时,一般为0</param>
        /// <param name="rn">用于迭加的传入值,一般为空</param>
        /// <returns></returns>
        public static string ListTreeShow(DirectoryInfo theDir,int nLevel,string rn)//递归目录 文件
        {
            DirectoryInfo[] subDirectories = theDir.GetDirectories();//获得目录
            foreach (DirectoryInfo dirInfo in subDirectories) {

                if (nLevel == 0) {
                    rn += "├";
                } else {
                    string s = "";
                    for (int i = 1 ; i <= nLevel ; i++) {
                        s += "│&nbsp;";
                    }
                    rn += s + "├";
                }
                rn += "<b>" + dirInfo.Name + "</b><br />";
                FileInfo[] fileInfo = dirInfo.GetFiles();   //目录下的文件
                foreach (FileInfo fInfo in fileInfo) {
                    if (nLevel == 0) {
                        rn += "│&nbsp;├";
                    } else {
                        string f = "";
                        for (int i = 1 ; i <= nLevel ; i++) {
                            f += "│&nbsp;";
                        }
                        rn += f + "│&nbsp;├";
                    }
                    rn += fInfo.Name + " <br />";
                }
                rn = ListTreeShow(dirInfo,nLevel + 1,rn);


            }
            return rn;
        }

        #region 获取文件夹大小
        /****************************************
         * 函数名称：GetDirectoryLength(string dirPath)
         * 功能说明：获取文件夹大小
         * 参    数：dirPath:文件夹详细路径
         * 调用示列：
         *           string Path = Server.MapPath("templates"); 
         *           Response.Write(DotNet.Utilities.FileOperate.GetDirectoryLength(Path));       
        *****************************************/
        /// <summary>
        /// 获取文件夹大小
        /// </summary>
        /// <param name="dirPath">文件夹路径</param>
        /// <returns></returns>
        public static long GetDirectoryLength(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles()) {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                foreach (var t in dis)
                {
                    len += GetDirectoryLength(t.FullName);
                }
            }
            return len;
        }
        #endregion

        #region 获取指定文件详细属性
        /****************************************
         * 函数名称：GetFileAttribute(string filePath)
         * 功能说明：获取指定文件详细属性
         * 参    数：filePath:文件详细路径
         * 调用示列：
         *           string file = Server.MapPath("robots.txt");  
         *            Response.Write(DotNet.Utilities.FileOperate.GetFileAttribute(file));         
        *****************************************/
        /// <summary>
        /// 获取指定文件详细属性
        /// </summary>
        /// <param name="filePath">文件详细路径</param>
        /// <returns></returns>
        public static string GetFileAttribute(string filePath)
        {
            var str = "";
            var objFi = new FileInfo(filePath);
            str += "详细路径:" + objFi.FullName 
                           + "<br>文件名称:" 
                           + objFi.Name 
                           + "<br>文件长度:" 
                           + objFi.Length 
                           + "字节<br>创建时间" 
                           + objFi.CreationTime 
                           + "<br>最后访问时间:" 
                           + objFi.LastAccessTime 
                           + "<br>修改时间:" 
                           + objFi.LastWriteTime 
                           + "<br>所在目录:" 
                           + objFi.DirectoryName 
                           + "<br>扩展名:" 
                           + objFi.Extension;
            return str;
        }
        #endregion


        /// <summary>
        /// 获取指定目录下所有符合指定模式的文件名。
        /// </summary>
        /// <param name="directory">指定目录。</param>
        /// <param name="pattern">指定模式。</param>
        /// <param name="fileList">符合指定模式的文件名列表。</param>
        public static void GetFiles(DirectoryInfo directory,string pattern,ref List<string> fileList) {
            fileList = new List<string>();
            if (directory.Exists && !string.IsNullOrWhiteSpace(pattern)) {
                try {
                    // 使用LINQ表达式筛选符合指定模式的文件，并将文件名添加到列表中。
                    fileList.AddRange(directory.GetFiles(pattern).Select(f => f.Name));
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// 获取指定目录下符合指定模式的文件名和文件夹名。
        /// </summary>
        /// <param name="directory">指定目录。</param>
        /// <param name="pattern">指定模式。</param>
        /// <param name="fileList">符合指定模式的文件和文件夹名称列表。</param>
        /// <param name="recursive">是否递归获取子文件夹中的文件和文件夹，true表示递归获取。</param>
        public static void GetFiles(DirectoryInfo directory,string pattern,ref List<string> fileList,bool recursive) {
            if (directory.Exists && !string.IsNullOrWhiteSpace(pattern)) {
                try {
                    // 获取目录下不包含"$RECYCLE"关键字的文件夹名，并添加到列表中。
                    foreach (DirectoryInfo info in directory.GetDirectories()) {
                        if (!info.FullName.Contains("$RECYCLE")) {
                            fileList.Add(info.Name);
                        }
                    }
                    // 获取目录下符合指定模式的文件名，并添加到列表中。
                    foreach (FileInfo info in directory.GetFiles(pattern)) {
                        fileList.Add(info.Name);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
                if (recursive) {
                    // 递归获取子文件夹中的文件和文件夹名称，并添加到列表中。
                    foreach (DirectoryInfo info in directory.GetDirectories()) {
                        GetFiles(info,pattern,ref fileList,true);
                    }
                }
            }
        }

        #region electron
        /// <summary>
        /// 读取桌面内容
        /// </summary>
        public Task<object> GetDesktop(object path) {
            // 创建目录信息对象
            DirectoryInfo dirInfo = new DirectoryInfo(path.ToString());
            // 存储搜索结果的列表
            List<string> searchResult = new List<string>();
            // 调用Common类中的GetFiles方法，将搜索结果存储在searchResult列表中
            GetFiles(dirInfo,"*.*",ref searchResult);
            return Task.FromResult<object>(searchResult);
        }
        #endregion

    }
}
