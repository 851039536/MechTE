c#一些常用功能封装如弹窗, 文件操作, 数据转换, ini文件操作, 字符串操作等

# 第三方库合并

## 合并工具及脚本

merge.bat 文件中编写脚本 , 使用 ILMerge.exe进行合并

```
cmd /k "D:\sw\编程资料\Dll合并工具\ILMerge.exe /ndebug /target:dll /out:D:\sw\class_library\MechTE\MechTE_480\bin\Debug\MechTE_480_Merge.dll /log D:\sw\class_library\MechTE\MechTE_480\bin\Debug\merge\MySql.Data.dll D:\sw\class_library\MechTE\MechTE_480\bin\Debug\merge\RestSharp.dll D:\sw\class_library\MechTE\MechTE_480\bin\Debug\MechTE_480.dll"
```

### merge文件

merge文件中的库是等待合并的dll

- MechTE_480
- mysql - 6.9.12.0
- RestSharp

### 默认已合并的库

**已合并的dll**

- MechTE_480
- mysql - 6.9.12.0
- RestSharp

**合并后生成的dll**

- MechTE_480_Merge

## 如何合并?

在项目属性中构建事件填入

```bat
call $(TargetPath) merge.bat
```

每次生成项目时会调用项目下的merge.bat , 控制台输出ILMerge: Done.等于合并成功

# 功能实现

## MFile

文件操作类 

- 命名空间: namespace MechTE_480.Files
- 类名:MFile

### 打开文件&程序

#### OpenFile

开启程序/文件夹

```csharp
/// <summary>
/// 开启程序/文件夹
/// </summary>
/// <param name="path">路径</param>
/// <param name="fsShow">是否显示窗口 默认显示(1)</param>
public static void OpenFile(string path, int fsShow = 1)
```

**Fact**

```csharp
MFile.OpenFile("D:\\sw");
MFile.OpenFile("D:\\sw\\",1);
```

#### OpenProcessFile

使用Process类的Start方法启动外部程序/文件夹

```csharp
/// <summary>
/// 使用Process类的Start方法启动外部程序/文件夹
/// </summary>
/// <param name="path">路径</param>
public static void OpenProcessFile(string path)
```

**Fact**

```csharp
MFile.OpenProcessFile(@"D:\sw\winfrom\Merry-exeStartTool\bin\exeStartTool\dw");
```



### 文件复制

#### CopyFile

复制文件夹及文件

```csharp
/// <summary>
/// 复制文件夹及文件
/// </summary>
/// <param name="sourceFolder">原文件路径</param>
/// <param name="destFolder">目标文件路径</param>
/// <returns>1 || -1</returns>
public static int CopyFile(string sourceFolder, string destFolder)
```

**Fact**

```csharp
var ret= MFile.CopyFile(@"C:\Users\ch190006\Desktop\test",@"C:\Users\ch190006\Desktop\test2");
```



#### CopyFolder

复制文件夹(递归)

```csharp
/// <summary>
/// 复制文件夹(递归)
/// </summary>
/// <param name="varFromDirectory">源文件夹路径</param>
/// <param name="varToDirectory">目标文件夹路径</param>
public static void CopyFolder(string varFromDirectory, string varToDirectory)
```



#### Copy

将源文件的内容复制到目标文件中

```csharp
/// <summary>
/// 将源文件的内容复制到目标文件中
/// </summary>
/// <param name="sourceFilePath">源文件的绝对路径</param>
/// <param name="destFilePath">目标文件的绝对路径</param>
public static void Copy(string sourceFilePath, string destFilePath)
```

#### FileMove

移动文件

```csharp
/// <summary>
/// 移动文件
/// </summary>
/// <param name="orignFile">原始路径</param>
/// <param name="newFile">新路径</param>
public static void FileMove(string orignFile, string newFile)
```

#### CopyDir

指定文件夹下面的所有内容copy到目标文件夹下面

```csharp
/// <summary>
/// 指定文件夹下面的所有内容copy到目标文件夹下面
/// </summary>
/// <param name="srcPath">原始路径</param>
/// <param name="aimPath">目标文件夹</param>
public static void CopyDir(string srcPath, string aimPath)
```



### 文件对话框

#### FileDialog

浏览文件对话框

```csharp
/// <summary>
/// 窗体浏览文件对话框
/// </summary>
/// <returns>string</returns>
public static string FileDialog()
```



### 文件获取

#### QueryFile&GetFile

获取指定目录文件夹

```csharp
 /// <summary>
 /// 获取指定目录文件夹
 /// </summary>
 /// <param name="path">文件路径</param>
 /// <returns>文件名,文件路径</returns>
 public static List<string> QueryFile(string path)
 //功能相同 , 后续删除QueryFile
 public static List<string> GetFile(string path)
```

#### GetFileNames

获取指定目录中所有文件列表

```csharp
/// <summary>
/// 获取指定目录中所有文件列表
/// </summary>
/// <param name="directoryPath">指定目录的绝对路径</param>        
public static string[] GetFileNames(string directoryPath)
```

#### GetDirectories

获取指定目录中所有子目录列表

```csharp
/// <summary>
/// 获取指定目录中所有子目录列表
/// </summary>
/// <param name="directoryPath">指定目录的绝对路径</param>        
public static string[] GetDirectories(string directoryPath)
```

#### GetFileNames

获取指定目录及子目录中所有文件列表

```csharp
 /// <summary>
 /// 获取指定目录及子目录中所有文件列表
 /// </summary>
 /// <param name="directoryPath">指定目录的绝对路径</param>
 /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
 /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
 /// <param name="isSearchChild">是否搜索子目录</param>
 public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
```

#### GetFileName

从文件的绝对路径中获取文件名( 包含扩展名 )

```csharp
/// <summary>
/// 从文件的绝对路径中获取文件名( 包含扩展名 )
/// </summary>
/// <param name="filePath">文件的绝对路径</param>        
public static string GetFileName(string filePath)
```



### 文件删除

#### DelFile

删除指定的文件

```csharp
/// <summary>
/// 删除指定的文件
/// </summary>
/// <param name="path">路径</param>
/// <returns></returns>
public static bool DelFile(string path)
```

#### DeleteFolderFiles

删除指定文件夹对应其他文件夹里的文件

```csharp
/// <summary>
/// 删除指定文件夹对应其他文件夹里的文件
/// </summary>
/// <param name="varFromDirectory">指定文件夹路径</param>
/// <param name="varToDirectory">对应其他文件夹路径</param>
public static void DeleteFolderFiles(string varFromDirectory, string varToDirectory)
```

#### DeleteDirectory

删除指定目录及其所有子目录

```csharp
/// <summary>
/// 删除指定目录及其所有子目录
/// </summary>
/// <param name="directoryPath">指定目录的绝对路径</param>
public static void DeleteDirectory(string directoryPath)
```

#### ClearFile

清空文件内容

```csharp
/// <summary>
/// 清空文件内容
/// </summary>
/// <param name="filePath">文件的绝对路径</param>
public static void ClearFile(string filePath)
```



### 文件检查

#### IsExistDirectory

检测指定目录是否存在

```csharp
/// <summary>
/// 检测指定目录是否存在
/// </summary>
/// <param name="directoryPath">目录的绝对路径</param>
/// <returns></returns>
public static bool IsExistDirectory(string directoryPath)
```

#### IsExistFile

检测指定文件是否存在

```csharp
/// <summary>
/// 检测指定文件是否存在
/// </summary>
/// <param name="filePath">文件的绝对路径</param>       
public static bool IsExistFile(string filePath)
```

#### IsEmptyDirectory

检测指定目录是否为空

```csharp
/// <summary>
/// 检测指定目录是否为空
/// </summary>
/// <param name="directoryPath">指定目录的绝对路径</param>        
public static bool IsEmptyDirectory(string directoryPath)
```

#### Contains

```csharp
/// <summary>
/// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
/// </summary>
/// <param name="directoryPath">指定目录的绝对路径</param>
/// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
/// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>        
public static bool Contains(string directoryPath, string searchPattern)
```

```csharp
Contains(@"D:\\sw\\","*");
```

检测指定目录中是否存在指定的文件

```csharp
/// <summary>
/// 检测指定目录中是否存在指定的文件
/// </summary>
/// <param name="directoryPath">指定目录的绝对路径</param>
/// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
/// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
/// <param name="isSearchChild">是否搜索子目录</param>
public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
```



### 文件创建

#### ExistsFile

 检查文件,如果文件不存在则创建  

```csharp
/// <summary>
/// 检查文件,如果文件不存在则创建  
/// </summary>
/// <param name="filePath">路径,包括文件名</param>
public static void ExistsFile(string filePath)
```



#### CreateDirectory

创建一个目录

```cs
/// <summary>
/// 创建一个目录
/// </summary>
/// <param name="directoryPath">目录的绝对路径</param>
public static void CreateDirectory(string directoryPath)
```

#### CreateFile

创建一个文件

```csharp
/// <summary>
/// 创建一个文件
/// </summary>
/// <param name="filePath">文件的绝对路径</param>
public static void CreateFile(string filePath)

/// <summary>
/// 创建一个文件,并将字节流写入文件。
/// </summary>
/// <param name="filePath">文件的绝对路径</param>
/// <param name="buffer">二进制流数据</param>
public static void CreateFile(string filePath, byte[] buffer)
```

#### FolderCreate

```csharp
/// <summary>
/// 在当前目录下创建目录
/// </summary>
/// <param name="orignFolder">当前目录</param>
/// <param name="newFloder">新目录</param>
public static void FolderCreate(string orignFolder, string newFloder)
    
/// <summary>
/// 创建文件夹
/// </summary>
/// <param name="path"></param>
public static void FolderCreate(string path)
```



### 文件信息

#### GetLineCount

获取文本文件的行数

```csharp
/// <summary>
/// 获取文本文件的行数
/// </summary>
/// <param name="filePath">文件的绝对路径</param>       
public static int GetLineCount(string filePath)
```

#### GetFileSize

获取一个文件的长度,单位为Byte

```csharp
/// <summary>
/// 获取一个文件的长度,单位为Byte
/// </summary>
/// <param name="filePath">文件的绝对路径</param>     
public static int GetFileSize(string filePath)
```



#### GetDirectories

获取指定目录及子目录中所有子目录列表

```csharp
/// <summary>
/// 获取指定目录及子目录中所有子目录列表
/// </summary>
/// <param name="directoryPath">指定目录的绝对路径</param>
/// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
/// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
/// <param name="isSearchChild">是否搜索子目录</param>
public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
```



#### GetFileNameNoExtension

从文件的绝对路径中获取文件名( 不包含扩展名 )

```csharp
/// <summary>
/// 从文件的绝对路径中获取文件名( 不包含扩展名 )
/// </summary>
/// <param name="filePath">文件的绝对路径</param>        
public static string GetFileNameNoExtension(string filePath)
```



#### GetExtension

从文件的绝对路径中获取扩展名

```csharp
/// <summary>
/// 从文件的绝对路径中获取扩展名
/// </summary>
/// <param name="filePath">文件的绝对路径</param>        
public static string GetExtension(string filePath)
```



#### ReadFile

读文件内容

```csharp
/// <summary>
/// 读文件内容
/// </summary>
/// <param name="path">文件路径</param>
/// <returns></returns>
public static string ReadFile(string path)
```

#### GetDirectoryLength

获取文件夹大小

```csharp
/// <summary>
/// 获取文件夹大小
/// </summary>
/// <param name="dirPath">文件夹路径</param>
/// <returns></returns>
public static long GetDirectoryLength(string dirPath)
```

#### GetFileAttribute

获取指定文件详细属性

```csharp
/// <summary>
/// 获取指定文件详细属性
/// </summary>
/// <param name="filePath">文件详细路径</param>
/// <returns></returns>
public static string GetFileAttribute(string filePath)
```

#### GetFiles

```csharp
/// <summary>
/// 获取指定目录下符合指定模式的文件名和文件夹名。
/// </summary>
/// <param name="directory">指定目录。</param>
/// <param name="pattern">指定模式。</param>
/// <param name="fileList">符合指定模式的文件和文件夹名称列表。</param>
/// <param name="recursive">是否递归获取子文件夹中的文件和文件夹，true表示递归获取。</param>
public static void GetFiles(DirectoryInfo directory, string pattern, ref List<string> fileList, bool recursive)

/// <summary>
/// 获取指定目录下所有符合指定模式的文件名..
/// </summary>
/// <param name="directory">指定目录。</param>
/// <param name="pattern">指定模式。</param>
/// <param name="fileList">符合指定模式的文件名列表。</param>
public static void GetFiles(DirectoryInfo directory, string pattern, ref List<string> fileList)
```

#### GetDesktop

读取桌面内容(electron)

```csharp
/// <summary>
/// 读取桌面内容(electron)
/// </summary>
public Task<object> GetDesktop(object path)
```



### 文本写入

#### WriteText

向文本文件中写入内容

```csharp
 /// <summary>
 /// 向文本文件中写入内容
 /// </summary>
 /// <param name="filePath">文件的绝对路径</param>
 /// <param name="text">写入的内容</param>
 /// <param name="encoding">编码</param>
 public static void WriteText(string filePath, string text, Encoding encoding)
```

#### AppendText

向文本文件的尾部追加内容

```csharp
/// <summary>
/// 向文本文件的尾部追加内容
/// </summary>
/// <param name="filePath">文件的绝对路径</param>
/// <param name="content">写入的内容</param>
public static void AppendText(string filePath, string content)
```



#### WriteFile

写文件

```csharp
/// <summary>
/// 写文件
/// </summary>
/// <param name="path">文件路径</param>
/// <param name="strings">文件内容</param>
public static void WriteFile(string path, string strings)
```

#### FileAdd

追加文件内容

```csharp
 /// <summary>
 /// 追加文件内容
 /// </summary>
 /// <param name="path">文件路径</param>
 /// <param name="strings">内容</param>
 public static void FileAdd(string path, string strings)
```



## MButton

弹窗提示类 (定制类不通用)

- 命名空间: namespace MechTE_480.btnForm
- 类名:MButton

### ButtonTest

弹框按键测试

```csharp
/// <summary>
/// 按键测试
/// </summary>
/// <param name="func">传入方法, _button.ButtonTest(() =
/// <param name="name">窗口名</param>
/// <returns></returns>
public bool ButtonTest(Func<bool> func,string name)
    
/// <summary>
/// 按键测试
/// </summary>
/// <param name="command">command对象</param>
/// <param name="action">下指令并且获取回传值的整个动作（下指令并且获取回传值事件）例：()=>{ command.WriteSendRetur
/// <param name="readData">按键操作对应指令返回值</param>
/// <param name="name">按键操作对应窗口名</param>
/// <returns></returns>
public bool ButtonTest(MechHID command,Action action,string readData,string name)
```

简单使用

```csharp
//创建一个按键测试对象对象
private readonly MButton _button = new MButton();

//传入方法和窗体名称
_button.ButtonTest(() => HeadsetBtn("0x01"), "请按Teams键")
```



## MForm

常用窗体

- 命名空间: namespace MechTE_480.Form
- 类名:MForm



### MesBox

默认弹框提示(确认/取消)

```csharp
/// <summary>
/// 默认弹框提示(确认/取消)
/// </summary>
/// <param name="name">描述</param>
/// <param name="title">标题</param>
/// <returns>bool</returns>
public static bool MesBox(string name, string title)
```

### ShowErr

错误提示(确认)

```csharp
/// <summary>
/// 错误提示(确认)
/// </summary>
/// <param name="title">标题</param>
/// <param name="prompt">描述</param>
public static void ShowErr(string title, string prompt)
```

### ShowInputDialog

弹窗接收参数(确认/取消)..

```csharp
/// <summary>
/// 弹窗接收参数(确认/取消)..
/// </summary>
/// <param name="title">标题</param>
/// <param name="prompt">描述</param>
/// <returns></returns>
public static string ShowInputDialog(string title, string prompt)
```



### 窗体比例缩放

```csharp
/// <summary>
/// 控件大小随窗体大小等比例缩放,
/// 在窗体重载中使用 >>  MechForm.X = this.Width;
/// </summary>
/// <param name="cons"></param>
public static void SetTag(Control cons)
    

/// <summary>
/// 设置缩放,在Resize事件中使用 >>
/// float newX = this.Width / MechForm.X;
/// float newY = this.Height / MechForm.Y;
/// MechForm.SetControls(newX,newY,this);
/// </summary>
/// <param name="newX">X轴</param>
/// <param name="newY">Y轴</param>
/// <param name="cons"></param>
public static void SetControls(float newX,float newY,Control cons)
```



## MTemplate

模板程序定制的功能

- 命名空间: MechTE_480.merryDll
- 类名:MTemplate



### IsBzp

检查SN是否是标准品条码,自动转换大写

```csharp
/// <summary>
/// 检查SN是否是标准品条码,自动转换大写
/// </summary>
/// <returns></returns>
public static bool IsBzp(string sn)
```



## MIni

ini文件操作类

- 命名空间: MechTE_480.MIni
- 类名:MIni

### Read

读取ini

```csharp
/// <summary>
/// 读取ini
/// </summary>
/// <param name="section">ini文件[xxxx]头部标识</param>
/// <param name="key">键名</param>
/// <param name="path">文件路径</param>
/// <returns>string</returns>
public static string Read(string section, string key, string path)
```

### ReadArray

```csharp
/// <summary>
/// 读取ini string[]
/// </summary>
/// <param name="section">ini文件[xxxx]头部标识</param>
/// <param name="key">文件路径</param>
/// <param name="path">Key</param>
/// <returns>string[]</returns>
public static string[] ReadArray(string section, string key, string path)
```

### Update

```csharp
/// <summary>
/// 更新ini
/// </summary>
/// <param name="section">ini文件[xxxx]头部标识</param>
/// <param name="key">key名</param>
/// <param name="value">写入的值</param>
/// <param name="path">完整的ini文件名路径</param>
public static void Update(string section, string key, string value, string path)
```

### Delete

```
/// <summary>
/// 删除一个INI文件
/// </summary>
/// <param name="filePath"></param>
public static void Delete(string filePath)
```



## MString

字符串操作类

- 命名空间: MechTE_480.util
- 类名:MString



### Reverse

```csharp
/// <summary>
/// 将字符按2个长度为一组进行反序
/// </summary>
/// <param name="str">11223344</param>
/// <returns>44332211->11223344</returns>
public static string Reverse(string str)
```

### ClearSpaces

清除字符串中的空格

```csharp
/// <summary>
/// 清除字符串中的空格(00 00 00 00 > 00000000)
/// </summary>
/// <param name="str"></param>
/// <returns>string</returns>
public static string ClearSpaces(string str)
```

### StringToByteArray

将字符串转换为字节数组

```csharp
/// <summary>
/// 将字符串转换为字节数组
/// 示例："ABCDEF" -> [ 0xAB, 0xCD, 0xEF ]
/// </summary>
/// <param name="str"></param>
/// <returns></returns>
public static List<byte> StringToByteArray(string str)
```

### StringToHidFormat

将字符转换HID指令格式 (name=0021032334 > 00 21 03 23 3

```csharp
/// <summary>
/// 将字符转换HID指令格式 (name=0021032334 > 00 21 03 23 3
/// </summary>
/// <param name="name"></param>
/// <returns>string</returns>
public static string StringToHidFormat(string name)
```

### GenerateNumberSequence

生成数字字符串序列(传0,6 生成 0 1 2 3 4 5)

```csharp
/// <summary>
/// 生成数字字符串序列(传0,6 生成 0 1 2 3 4 5)
/// </summary>
/// <param name="startNumber">序列中第一个整数的值</param>
/// <param name="sequenceLength">生成的顺序总条数</param>
/// <returns>string</returns>  
public static string GenerateNumberSequence(int startNumber, int sequenceLength)
```



### 启动文件/程序

类名 **MechCmd**

执行cmd 程序 网站等...

```csharp
//执行Shell命令
StartShell(string cmd)  >> StartShell("notepad");
//执行bat文件
StartBat(string cmd) >> StartBat(_currentPath + @"\unload.bat");
//启动Windows应用/网站
StartApp(string appName) >> StartApp(@"D:\\software\Code.exe");
```



### 数据转换/处理

类名 **MechConvert**

处理数据类型转换，数制转换、编码转换相关的类

```csharp
//补足位数:指定字符串的固定长度，如果字符串小于固定长度， 则在字符串的前面补足零，可设置的固定长度最大为9位
RepairZero(string text, int totalLength) >> RepairZero("test",9);

//进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
ConvertBase(string value, int from, int to) >> ConvertBase("15",10,16)
    
//将string转换成byte[]
StringToBytes(string text, Encoding encoding) 
    
//将byte[]转换成string
BytesToString(byte[] bytes, Encoding encoding)z

//将byte[]转换成int
BytesToInt32(byte[] data)   
    
//图片转base64
ImgToBase64(string imagePath)
    
//将16进制字符转为ASCII字符
HexadecimalToASCII(string hex)

//ASCII字符转为16进制字符
ASCIIConvertsDecimal16(string name)
    
    
//将16进制字符转为ASCII 16进制字符
HexStrings2AsciiHexStrings(string hexStrings)
```



### TSystems

系统类

- 类名： TSystems

- 调用方式：TSystems.方法

- 支持版本: **MechTE_452**

| 描述             | 函数                 | 调用示例             |
| ---------------- | -------------------- | -------------------- |
| 读取当前系统信息 | GetWindows           | GetWindows();        |
| 获得本机的进程   | GetProcesses         | GetProcesses();      |
| 获取当前进程信息 | GetCurrentProcess    | GetCurrentProcess(); |
| 检查重复启动     | CheckProcessesByName |                      |
| 获取系统驱动器   | GetLogicalDrivess    |                      |



### MechUtils

通用工具类

- 类名： MechUtils

- 支持版本: **MechTE_452,480**

| 描述                                     | 函数                                   | 调用示例                    |
| ---------------------------------------- | -------------------------------------- | --------------------------- |
| 生成数字字符串序列                       | GenerateNumberStringSequence           | 生成-> 0 01 02 03...        |
| 判断字符串是否为空,空等于true，抛出异常  | IsEmptyAssert                          |                             |
| 自定义断言方法， result == true 抛出异常 | Assert(bool result, string errMsg)     |                             |
| 自定义断言方法， func() == true 抛出异常 | void CheckProcessesByName(string name) |                             |
| 获取系统驱动器                           | Assert(Func<bool> func, string errMsg) |                             |
| 开启音频内部装置窗体显示到桌面           | EnterHfp                               |                             |
| 检测进程关掉音频内部装置                 | QuitHfp                                |                             |
| 将字符转换HID指令格式                    | CharacterConversionHidFormat           | 0021032334 > 00 21 03 23 34 |

### ProgressBars

进度条

- 类名： ProgressBars
- 支持版本: **MechTE_480**

基础调用

```csharp
        private static ProgressBars msgbox;


            msgbox = new ProgressBars("name", 50);
            msgbox.ExecuteTest(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine(msgbox.Ide = true);
            });
```









