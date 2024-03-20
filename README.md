

<h1 align="center"> MechTE </h1>

MechTE一个功能丰富且易用的 .NET 工具库



# 第三方库合并

## 合并工具及脚本

**ILMerge.exe** : 支持将EXE依赖的DLL合并到EXE中，也支持将主DLL依赖的其他DLL合并到一个DLL中

在bat文件中编写脚本 , 使用 ILMerge.exe进行合并

### 配置bat命令

**merge.bat**

```bat
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

### 合并后生成的dll

- MechTE_480_Merge

## 项目中构建

在项目属性中构建事件填入

```bat
call $(TargetPath) merge.bat
```

每次生成项目时会调用项目下的merge.bat , 控制台输出 **ILMerge: Done.** 等于合并成功

# MechTE_480

基于 **NET Framework**  4.8.0 框架编写 , .NET Framework 仅适用于 Windows 系统。

使用c#编写一些常用功能封装如弹窗, 文件操作, 数据转换, ini文件操作, 字符串操作等

扩展方法 : 都是以Ex字符后缀结束

## MAssertUtil

自定义断言类

- 命名空间: MechTE_480.AssertCategory
- 类名:MAssertUtil

### 判断字符是否为空

判断字符串是否为空，如为空则抛出异常

```csharp
public static void IsEmpty(string value, string errMsg)
```

**参数**

`value`：需要判断的值
`errMsg`：异常提示

### 自定义断言方法

```csharp
/// <summary>
/// 自定义断言方法， result == true 抛出异常
/// </summary>
/// <param name="result">bool</param>
/// <param name="errMsg">错误信息</param>
/// <remarks>系统断言不能在 Release 版保留，用这个方法替代</remarks>
// ReSharper disable once MemberCanBePrivate.Global
public static void Assert(bool result, string errMsg)
    
/// <summary>
/// 自定义断言方法， func() == true 抛出异常    
/// </summary>
/// <param name="func"></param>
/// <param name="errMsg"></param>
/// <remarks>系统断言不能在 Release 版保留，用这个方法替代</remarks>
public static void Assert(Func<bool> func, string errMsg)
    
/// <summary>
/// 直接报错误提示
/// </summary>
/// <param name="errMsg"></param>
/// <exception cref="Exception"></exception
public static void Assert( string errMsg)
```



## 文件操作

MFileUtil 文件操作类

- 命名空间: MechTE_480.FileCategory
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



### 文件检查(判断)

#### 判断文件或目录是否为空

目录：里面没有文件时为空 
文件：文件大小为0时为空

**参数**

`path`：文件或目录的路径

**返回值**

 bool

```csharp
public static bool IsEmpty(string path)
```

#### 检测指定目录是否存在

**参数**

`directoryPath`：目录的绝对路径

**返回值**

 bool

```csharp
public static bool IsExistDirectory(string directoryPath)
```

#### 检测指定文件是否存在

**参数**

`filePath`：文件的绝对路径

**返回值**

 bool

```csharp
public static bool IsExistFile(string filePath)
```

#### 检测指定目录是否为空

**参数**

`directoryPath`：指定目录的绝对路径

**返回值**

 bool

```csharp
public static bool IsEmptyDirectory(string directoryPath)
```



#### 检测指定目录中是否存在指定的文件

**参数**

`directoryPath`：指定目录的绝对路径
`searchPattern`：模式字符串，"*"代表0或N个字符，"?"代表1个字符 , 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。
`isSearchChild`：是否搜索子目录

**返回值**

 bool

```csharp
public static bool Contains(string directoryPath, string searchPattern)
// 检测指定目录中是否存在指定的文件
public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
```

```csharp
Contains(@"D:\\sw\\","*");
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





## MFormUtil

常用窗体

- 命名空间: MechTE_480.FormCategory
- 类名:MFormUtil

### MoveForm

```csharp
 /// <summary>
 /// 鼠标按住窗体移动,先调用 Capture = false; MForm.MoveForm(Handle);
 /// </summary>
 /// <param name="handle"></param>
 public static void MoveForm(IntPtr handle)
```

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
 /// 在窗体重载中使用 >>  MForm.X = this.Width; MForm.Y = this.Height;  MForm.SetTag(this);
 /// </summary>
 /// <param name="cons"></param>
public static void SetTag(Control cons)
    

/// <summary>
/// 设置缩放,在Resize事件中使用 >>
///  float newX = this.Width / MForm.X;
///  float newY = this.Height / MForm.Y;
///  MForm.SetControls(newX,newY,this);
/// </summary>
/// <param name="newX">X轴</param>
/// <param name="newY">Y轴</param>
/// <param name="cons"></param>
public static void SetControls(float newX,float newY,Control cons)
```



### 弹框选择文件夹

```csharp
/// <summary>
/// 弹框选择文件夹
/// </summary>
/// <param name="description">描述</param>
/// <returns></returns>
public static string ShowDialog(string description)
```



### 按键测试

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

- 命名空间: MechTE_480.Files
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
/// 读取ini string[],key对应的value值以,分割
/// </summary>
/// <param name="section">ini文件[xxxx]头部标识</param>
/// <param name="key">key</param>
/// <param name="path">文件路径</param>
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

### ClearAllSection

```csharp
/// <summary>
/// 删除ini文件下所有段落
/// </summary>
public static void ClearAllSection(string filePath)
```

### ClearSection

```csharp
/// <summary>
/// 删除ini文件下personal段落下的所有键
/// </summary>
/// <param name="Section"></param>
/// <param name="filePath"></param>
public static void ClearSection(string Section,string filePath)
```



## MStringUtil

字符串操作类

- 命名空间: MechTE_480.util
- 类名:MStringUtil

### 启动应用网站

```csharp
// 扩展方法
public static void StartAppEx(this string value)
```

**参数**

`value`：要转换的字符串

### 字符反序

```csharp
public static string Reverse(string value)
// 扩展方法
public static string ReverseEx(this string value)
```

**参数**

`value`：将字符按2个长度为一组进行反序

**返回值**

转换成功则返回转换后44332211->11223344

### 清除字符串中的空格

清除字符串中的空格(00 00 00 00 > 00000000)

**参数**

`value`：将字符按2个长度为一组进行反序

**返回值**

转换成功则返回转换后44332211->11223344

```csharp
public static string ClearSpaces(string value)
// 扩展方式
public static string ClearSpacesEx(this string value)
```

### 字符串按照分隔符转List

把字符串按照分隔符转换成List

**参数**

`str`：源字符串
`speater`：分隔符
`toLower`：是否转换为小写

**返回值** 

`List<string>`

```csharp
public static List<string> StringToListStr(string str,char speater,bool toLower)
```



### StringToArray

```csharp
 /// <summary>
 /// 把字符串转 按照, 分割 换为数组
 /// </summary>
 /// <param name="str"></param>
 /// <returns></returns>
 public static string[] StringToArray(string str)
```



### StrLength

```csharp
 #region 得到字符串长度，一个汉字长度为2
 /// <summary>
 /// 得到字符串长度，一个汉字长度为2
 /// </summary>
 /// <param name="inputString">参数字符串</param>
 /// <returns></returns>
 public static int StrLength(string inputString)
```

### IsNullOrEmpty

```csharp
 /// <summary>
 /// 检测一个字符串是否为空
 /// </summary>
 /// <param name="str"></param>
 /// <returns></returns>
 public static bool IsNullOrEmpty(string str)
```

## MLogUtil

LOG数据操作

- 命名空间: MechTE_480.LogCategory
- 类名:MLogUtil

### LogWrite

```csharp
public static void LogWrite(string paths, string name, string str)
```

**参数**

`paths`：写入log路径
`name`：log文件名称
`str`：写入内容



## MConvertUtil

处理数据类型转换，数制转换、编码转换相关的类

- 命名空间: MechTE_480.ConvertCategory
- 类名:MConvertUtil

### 将字符转换为整型

```csharp
public static int ToInt32(string value)
//扩展方法
public static int ToInt32Ex(this string value)
```

**参数**

`value`：要转换的字符串

**返回值**

转换成功则返回转换后的整型值，否则返回0



### 将字符转换为长整型

```csharp
public static long ToInt64(string value)
```

**参数**

`value`：要转换的字符串。

**返回值**

转换成功则返回转换后的长整型值，否则返回0



### 将字符转换为布尔型

```csharp
public static bool ToBoolean(string value)
```

**参数**

`value`：要转换的字符串。

**返回值**

转换成功则返回转换后的布尔型值，否则返回false

### 将字符转换为双精度浮点型

**参数**

`value`：要转换的字符串。

**返回值**

将字符串转换为双精度浮点型，转换失败返回0

```csharp
public static double ToDouble(string value)
```

### 进制数间的转换

实现2,8,10,16进制数间的转换

```csharp
public static string ConvertBase(string value, int from, int to)
```

**参数**

`value`: 原值
`from` : 原值的进制,只能是2,8,10,16四个值
`to` : 要转换到的目标进制，只能是2,8,10,16四个值

**返回值**

string

### 转换成byte[]

使用指定字符集将string转换成byte[]

```csharp
public static byte[] ToBytes(string value, Encoding encoding)
```

**参数**

`value`: 原要转换的字符串
`encoding` : 字符编码

**返回值**

byte[]

### 将byte[]转换成string

```csharp
public static string BytesToString(byte[] bytes, Encoding encoding)
```

**参数**

`bytes`: 要转换的字节数组
`encoding` : 字符编码

**返回值**

string

### 16进制字符转ASCII字符

**参数**

`hex`: 16个数字（0-9和A-F）来表示

**返回值**

ASCII字符

```csharp
public static string HexToAscii(string hex)
//扩展方法
public static string HexToAsciiEx(this string hex)   
```



### ASCII字符转16进制字符

ASCII字符转为16进制字符

**参数**

`value`：要转换的字符串

**返回值**

16进制字符

```csharp
public static string AsciiStrToHexStr(string value)
//扩展方法
public static string AsciiStrToHexStrEx(this string value)    
```

### 将字符转换为十进制数

将字符串转换为十进制数，转换失败返回0

**参数**

`value`：要转换的值

**返回值**

decimal

```csharp
public static decimal ToDecimal(string value)
```

### 将字符转换为十进制数

将字符串转换为日期时间，转换失败返回DateTime.MinValue

`value`：要转换的值

**返回值**

DateTime

```csharp
public static DateTime ToDateTime(string value)
```

### 将字符串转换为枚举类型

将字符串转换为枚举类型，转换失败返回默认值

`value`：要转换的值
`defaultValue`：

**返回值**

DateTime

```csharp
public static T ToEnum<T>(string value, T defaultValue = default) where T : struct
```

单元测试

```csharp
 public enum MyEnum
 {
     Value1,
     Value2,
     Value3
 }
 [Fact]
 public void ToEnum()
 {
     // 使用方法
     string input = "Value2";
     var convertedEnum = MConvertUtil.ToEnum<MyEnum>(input); // 如果 "Value2" 存在于 MyEnum 中，则转换成功并返回相应枚举成员
     var defaultValueCase = MConvertUtil.ToEnum<MyEnum>("NonExistentValue"); // 如果 "NonExistentValue" 不存在于 MyEnum 中，则返回默认值 MyEnum.Value1
     _msg.WriteLine(convertedEnum.ToString());
     _msg.WriteLine(defaultValueCase.ToString());
     Assert.Equal(defaultValueCase, defaultValueCase);
 }
```



### 16进制字符转为ASCII16进制字符

ASCII字符转为16进制字符

**参数**

`value`：要转换的字符串

**返回值**

76312E342E30 > 373633313245333432453330

```cs
public static string HexToAsciiHex(string value)
```

### 将字符转换HID指令格式

**参数**

`value`：要转换的字符串如:0021032334

**返回值**

转换成功则返回00 21 03 23 3

```csharp
public static string ToHidFormat(string value)   
public static string ToHidFormatEx(this string value)
```

### 将字符串转换为单精度浮点型

**参数**

`value`：将字符串转换为单精度浮点型

**返回值**

转换失败返回0

```csharp
public static float ToSingle(string value)
```



### StringToByteArray

```csharp
 /// <summary>
 /// 将字符串转换为字节数组
 /// 示例："ABCDEF" -> [ 0xAB, 0xCD, 0xEF ]
 /// </summary>
 /// <param name="str"></param>
 /// <returns></returns>
 public static List<byte> StringToByteArray(string str)
```

### NumberStrToIntArray

```csharp
/// <summary>
/// 将数字类型字符串转int数组
/// </summary>
/// <param name="ar">数字字符用空格分割</param>
/// <returns></returns>
public static int[] NumberStrToIntArray(string ar)
```

### ByteToHex

```csharp
/// <summary>
/// Byte数组转16进制字符串
/// </summary>
/// <param name="bytes">Byte数组</param>
/// <returns>16进制字符串</returns>
public static string ByteToHex(byte[] bytes)
```

### ByteToHex

```csharp
/// <summary>
/// 通过给定的索引，从字节数组中提取特定位置的字节，并将其转换为十六进制字符串
/// </summary>
/// <param name="bytes">Byte数组</param>
/// <param name="index">Byte数组索引数组</param>
/// <returns>16进制字符串</returns>
public static string ByteToHex(byte[] bytes, string index)
```



## 时间操作

MDateTimeUtil 时间类操作 

- 命名空间: MechTE_480.DateTimeCategory
- 类名:MDateTimeUtil

### 获取当前日期

**返回值**

yyyy-MM-dd

```csharp
public static string GetTime()
```

### 获取前一天时间

**返回值**

yyyy-MM-dd

```csharp
public static string GetYesterdayTime()
```

### 获取当前日期的星期几

**返回值**

DayOfWeek

```csharp
public static DayOfWeek GetDayOfWeek()
```



## 数据加密





## MMeasure

测量代码执行时间

- 命名空间: MechTE_480.Util
- 类名:MMeasure

```csharp
/// <summary>
/// 测量代码的执行时间
/// </summary>
/// <param name="callback"></param>
public MMeasure(Action<TimeSpan> callback)
```

**单元测试**

```csharp
[Fact]
public void MMeasure()
{
    using (new MMeasure(duration => _msg.WriteLine($"执行时间：{duration}")))
    {
        // 在这里编写需要测量执行时间的代码
        for (int i = 0; i < 5; i++)
        {
            // 一些耗时的操作
            Thread.Sleep(1000);
        }
    }
}      
```

## MProcessUtil

使用进程调用cmd命令或程序

- 命名空间: MechTE_480.ProcessCategory	
- 类名:MProcessUtil

### 根据名称获取wifi密码

```csharp
/// <summary>
/// 根据名称获取wifi密码
/// </summary>
/// <param name="value"></param>
/// <returns></returns>
public static string GetWiFiPassword(string value)
```

### 执行cmd

```csharp
 /// <summary>
 /// 单个线程执行多个cmd指令
 /// </summary>
 /// <param name="commands"></param>
 public static void ExCmdWrite(string[] commands)
     
 /// <summary>
/// 执行多个cmd,每次都会创建一次进程
/// </summary>
/// <param name="commands"></param>
/// <returns></returns>
public static string ExCmd(string[] commands)
     
 /// <summary>
 /// 执行单个cmd命令获取返回值
 /// </summary>
 /// <param name="cmd"></param>
 /// <returns></returns>
 public static string ExCmd(string cmd)
```

调用示例

```csharp
MProcessUtil.ExCmd("mstsc");
 MProcessUtil.ExCmdWrite(new[] { "d:", @"cd D:\sw\model\MSP168\MSP168\MerryDll\bin\Debug\fw", "FWupdate.exe /VID_03F0 /PID_0D84 -USB -Ver" });
MProcessUtil.ExCmd(new[] { "ipconfig", "mstsc", "notepad" });
```

### Bat

```csharp
/// <summary>
/// 执行bat文件
/// </summary>
/// <param name="cmd"></param>
public static void Bat(string cmd)
```

### StartApp

```csharp
/// <summary>
/// 启动应用网站
/// </summary>
/// <param name="appName">/程序路径</param>
/// <returns>bool</returns>
public static bool StartApp(string appName)
```

### StartApps

```csharp
/// <summary>
/// 启动应用(管理员运行)
/// </summary>
/// <param name="appName"></param>
public static void StartApps(string appName)
```

### RestartAsAdministrator

```csharp
 /// <summary>
 /// 重新启动应用程序并请求管理员权限
 /// </summary>
 public static void RestartAsAdministrator()
```

### 执行启动外部程序/文件夹

使用Process类的Start方法启动外部程序/文件夹

```csharp
/// <summary>
/// 使用Process类的Start方法启动外部程序/文件夹
/// </summary>
/// <param name="path">路径</param>
public static void OpenProgram(string path)
```

**Fact**

```csharp
MFile.OpenProcessFile(@"D:\sw\winfrom\Merry-exeStartTool\bin\exeStartTool\dw");
```



## MRegexUtil

操作正则表达式的常用类

- 命名空间: MechTE_480.RegexsCategory
- 类名:MRegexUtil

### 邮箱验证

```csharp
 /// <summary>
 /// 验证EMail是否合法
 /// </summary>
 /// <param name="email">要验证的Email</param>
 public static bool IsEmail(string email)
```





## MSerialPort

串口工具类操作

- 命名空间: MechTE_480.port
- 类名:MSerialPort

创建串口

```csharp
var mSerialPort = new MSerialPort(portName, 9600, Parity.None, 8, StopBits.One);
```

### GetPortName

```csharp
/// <summary>
/// 获取可用串口设备的名称数组
/// </summary>
public static string[] GetPortName()
```

### SendData

```csharp
 /// <summary>
 /// 写字符串指令
 /// </summary>
 /// <param name="data"></param>
 /// <exception cref="ApplicationException"></exception>
 public void SendData(string data)
     

/// <summary>
/// 写二进制指令
/// </summary>
/// <param name="data">如:byte[] s1 = { 0x55, 0x01, 0x32, 0x00, 0x00, 0x00, 0x01, 0x89 }</param>
/// <param name="f">默认 0</param>
/// <param name="l">字节数</param>
/// <exception cref="ApplicationException"></exception>
public void SendData(byte[] data, int f, int l)
```

### SendHexString

```csharp
/// <summary>
/// 使用16进制字符串发送数据
/// </summary>
/// <param name="hexString"></param>
public void SendHexString(string hexString)
```

## MLogUtil

写本地log文本数据

网络工具类

- 命名空间: MechTE_480.LogCategory
- 类名:MLogUtil

### LogWrite

```csharp
/// <summary>
/// 写入本地log,自动生成当前时间
/// </summary>
/// <param name="paths">写入log路径</param>
/// <param name="name">log文件名称</param>
/// <param name="value">写入内容</param>
public static void LogWrite(string paths, string name, string value)
```

###  LogWriteYesterdayTime

```csharp
/// <summary>
/// 写入本地log,自动生成前一天时间
/// </summary>
/// <param name="paths"></param>
/// <param name="name"></param>
/// <param name="value"></param>
public static void LogWriteYesterdayTime(string paths, string name, string value)
```

## MNetHelper

网络工具类

- 命名空间: MechTE_480.network
- 类名:MNetHelper

### GetAddressIp

```csharp
/// <summary>
/// 获取本地IP
/// </summary>
/// <returns></returns>
public static string GetAddressIp()
```

### GetValidPort

```csharp
/// <summary>
/// 检查设置的端口号是否正确，并返回正确的端口号,无效端口号返回-1。
/// </summary>
/// <param name="port">设置的端口号</param>        
public static int GetValidPort(string port)
```

### StringToIpAddress

```csharp
 /// <summary>
 /// 将字符串形式的IP地址转换成IPAddress对象
 /// </summary>
 /// <param name="ip">字符串形式的IP地址</param>        
 public static IPAddress StringToIpAddress(string ip)
```

### GetHostName

```csharp
/// <summary>
/// 获取本机的计算机名
/// </summary>
public static string GetHostName()
```

### WANIP

```csharp
 /// <summary>
 /// 获取本机在Internet网络的广域网IP
 /// </summary>        
 public static string WANIP()

```

### LANIP

```csharp
 /// <summary>
 /// 获取本机的局域网IP
 /// </summary>        
 public static string LANIP()

```

## MHidUtil

HID指令帮助类

- 命名空间: MechTE_480.PortCategory.hid
- 类名:MHidUtil

执行流程

1. 先获取装置句柄 GetHandle
2. 执行下指令函数: 如WriteReturn
3. 关闭句柄CloseHandle

### 使用实例

```csharp
private static readonly MHidUtil _command = new MHidUtil();
private const string MCU_VID = "03F0";
private const string MCU_PID = "02B5";

 /// <summary>
 /// 执行打开和关闭,
 /// 而不必每次都重复打开和关闭串口。
 /// </summary>
 /// <param name="action"></param>
 private void ExMCU_Cmd(Action action)
 {
     _command.GetHandle(MCU_VID, MCU_PID);
     Thread.Sleep(100);
     action();
     _command.CloseHandle();
 }

/// <summary>
/// MUC指令
/// </summary>
/// <param name="cmd">指令</param>
private bool Write_MUCSend(string cmd)
{
    bool ret = false;
    ExMCU_Cmd(() => { ret = _command.WriteSend(cmd, 64, _command.SetHandle1[1]); });
    if (ret) return true;
    return false;
}
```



### WriteSend

```csharp
/// <summary>
/// 使用write下下指令
/// </summary>
/// <param name="command">指令如: 06 00 05...</param>
/// <param name="length">指令长度</param>
/// <param name="intPtr">装置句柄(如果没有调用GetHandle获取)</param>
/// <returns>bool</returns>
public bool WriteSend(string command, int length, IntPtr intPtr)

 /// <summary>
 /// 使用write下下指令(64长度)
 /// </summary>
 /// <param name="command">指令如: 06 00 05...</param>
 /// <param name="intPtr">装置句柄(如果没有调用GetHandle获取)</param>
 /// <returns>bool</returns>
 public bool WriteSend(string command, IntPtr intPtr)
```





## MUsbUtil

USB端口类

- 命名空间: MechTE_480.PortCategory.usb
- 类名:MUsbUtil

### GetDeviceName

```csharp
/// <summary>
/// 装置USB装置名称
/// </summary>
/// <param name="vendorId">供应商标识VID</param>
/// <param name="productId">产品编号PID</param>
/// <param name="names">匹配装置名称</param>
/// <returns></returns>
public static string GetDeviceName(ushort vendorId, ushort productId, string names)
```

### IsDevice

```csharp
/// <summary>
/// 检测USB播放装置是否存在
/// </summary>
/// <param name="deviceName">装置名称(DeviceID:PID_A527)</param>
/// <returns></returns>
public static bool IsDevice(string deviceName)
```



## MUtil

通用工具类

- 命名空间: MechTE_480.util
- 类名:MUtil

### Execute

```csharp
/// <summary>
/// 检测传入的方法是否超时
/// </summary>
/// <param name="timeoutMethod">方法</param>
/// <param name="param">方法的参数</param>
/// <param name="result">执行结果</param>
/// <param name="timeout">超时时间</param>
/// <typeparam name="T">方法的参数类型</typeparam>
/// <typeparam name="TR">执行结果的类型</typeparam>
/// <returns>是否超时</returns>
public static bool Execute<T, TR>(
    TimeOutDelegate<T, TR> timeoutMethod, T param, out TR result, TimeSpan timeout)
```

单元测试

```csharp
 // 示例方法，接受一个字符串参数，并返回一个字典
 public static Dictionary<Guid, string> Test(string sourceString)
 {
     // 将字符串转换为字典，每个字符作为键，使用Guid作为值
     var result = sourceString.ToDictionary(
         character => Guid.NewGuid(),
         character => character.ToString(CultureInfo.InvariantCulture));
     // 模拟耗时操作，暂停4秒
     Thread.Sleep(4000);
     
     return result;
 }
 [Fact]
 public void Execute()
 {
     Dictionary<Guid, string> result;
     
     // 调用TimeoutFunction类的Execute方法执行带有超时检查的方法
     // Test方法是一个示例方法，它接受一个字符串参数，并返回一个字典
     // "Hello, World!"是传递给Test方法的参数
     // result是用于接收Test方法的返回值的字典
     // TimeSpan.FromSeconds(3)表示超时时间为3秒
     // Execute方法返回一个布尔值，表示是否超时
    var ret=   MUtil.Execute(Test, "Hello, World!", out result, TimeSpan.FromSeconds(3));
     _msg.WriteLine(ret.ToString());
 }      
```



### WaitSomething

```csharp
 /// <summary>
 /// 在指定的时间内等待某个函数的执行结果,调用 bool result = WaitSomething(5000, 1000, () =>{})
 /// </summary>
 /// <param name="timeout">表示等待的最大时间，以毫秒为单位</param>
 /// <param name="freq">表示等待的频率，即每隔多少毫秒检查一次函数的执行结果</param>
 /// <param name="func">表示要等待的函数，它是一个返回布尔值的委托</param>
 /// <returns></returns>
 public static bool WaitSomething(int timeout, int freq, Func<bool> func)
```

### GetCurrentProgramDirectory

```csharp
/// <summary>
/// 获取当前程序根目录地址(D:\File\bin\Debug)
/// </summary>
/// <returns></returns>
public static string GetCurrentProgramDirectory()
```

### GenerateNumberSequence

```csharp
/// <summary>
/// 生成数字字符串序列(传0,6生成 0 1 2 3 4 5)
/// </summary>
/// <param name="startNumber">序列中第一个整数的值</param>
/// <param name="sequenceLength">生成的顺序总条数</param>
/// <returns>string</returns>  
public static string GenerateNumberSequence(int startNumber, int sequenceLength)
```

### IsInt

```csharp
  /// <summary>
  /// 验证是否为整数 如果为空，认为验证不合格 返回false
  /// </summary>
  /// <param name="number">要验证的整数</param>      
  public static bool IsInt(string number)
```

### IsNumber

```
 /// <summary>
 /// 验证是否为数字
 /// </summary>
 /// <param name="number">要验证的数字</param>        
 public static bool IsNumber(string number)

```



## MWin

系统相关API

- 命名空间: MechTE_480.Windows
- 类名:MWin

### IsConnectInternet

```csharp
/// <summary>
/// 用于检查网络是否可以连接互联网
/// </summary>
/// <returns></returns>
public static bool IsConnectInternet()
```

### PingIpOrDomainName

```csharp
/// <summary>
/// 用于检查IP地址或域名(www.cnblogs.com)是否可以使用TCP/IP协议访问(使用Ping命令),true表示Ping成功,false表示Ping失败
/// </summary>
/// <param name="strIpOrDName">输入参数,表示IP地址或域名</param>
/// <returns></returns>
public static bool PingIpOrDomainName(string strIpOrDName)
```

单元测试

```csharp
[Fact]
public void IsConnectInternet()
{
    var ret = MWin.PingIpOrDomainName("www.cnblogs.com");
    Assert.True(ret);
}
```

### SetMasterVolume

```csharp
 /// <summary>
 /// 设置系统音量
 /// </summary>
 public static void SetMasterVolume(float newLevel)
```

单元测试

```csharp
 [Fact]
 public void SetMasterVolume()
 {
     MWin.SetMasterVolume(20);
     var data = MWin.GetMasterVolume();
     _msg.WriteLine(data.ToString());
     Assert.Equal(data, data);
 }
```

### GetMasterVolume

```csharp
 /// <summary>
 /// 返回系统音量(1~100)
 /// </summary>
 public static float GetMasterVolume()
```

### SetMasterVolumeMute

```csharp
/// <summary>
/// 设置系统静音
/// </summary>
/// <param name="isMuted"></param>
public static void SetMasterVolumeMute(bool isMuted)
```

### MesBoxs

```csharp
 /// <summary>
 /// 弹出提示
 /// </summary>
 /// <param name="text">内容描述</param>
 /// <param name="caption">标题</param>
 public static void MesBoxs(string text, string caption)
     
 /// <summary>
 /// 弹出提示,传参
 /// </summary>
 /// <param name="text">内容描述</param>
 /// <param name="caption">标题</param>
 /// <param name="options">1:确认/取消,2:终止/重试/忽略,3:是/否/取消,4:是/否,5:重试/取消,6:取消/重试/继续</param
 /// <returns></returns>
 public static int MesBoxs(string text, string caption, int options)
```

### OpenA2Dp

```csharp
 /// <summary>
 /// 启动播放装置
 /// </summary>
 /// <returns></returns>
 public static void OpenA2Dp()
```



### EnterHfp



```csharp
 /// <summary>
 /// 开启音频设置显示到桌面
 /// </summary>
 /// <returns></returns>
 public static bool EnterHfp()
```

### OpenDevice

```csharp
/// <summary>
/// 开启系统设备管理器
/// </summary>
public static void OpenDevice()
```

### CloseRunDll

```csharp
/// <summary>
/// 清除rundll32进程
/// </summary>
/// <param name="processName">rundll32</param>
/// <returns>bool</returns>
public static bool CloseRunDll(string processName = "rundll32")
```

### IsUserAdministrator

```csharp
/// <summary>
/// 判断当前程序是否是管理员
/// </summary>
/// <returns></returns>
public static bool IsUserAdministrator()
```



## MXml

定制类



# MechTE_ContextMenu

自定义桌面菜单库



# MechTE_Speech

微软语音识别封装

