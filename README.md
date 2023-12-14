# MechTE_API

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



### 文件操作类

类名 **MechFile**

#### OpenFile

使用本地系统进程打开程序/文件夹

```
OpenFile("D:\\sw");
OpenFile("D:\\sw\\",1);
```

#### CopyFile

复制文件夹及文件

```csharp
        /// <summary>
        /// 复制文件夹及文件
        /// </summary>
        /// <param name="sourceFolder">原文件路径</param>
        /// <param name="destFolder">目标文件路径</param>
        /// <returns>1 || -1</returns>
        public static int CopyFile(string sourceFolder,string destFolder)
```

#### FileDialog

浏览文件对话框

#### QueryFile

获取指定目录文件夹

QueryFile("D:\\sw\\");

|                                              |                                        |                                |
| -------------------------------------------- | -------------------------------------- | ------------------------------ |
| 删除指定的文件                               | DelFile                                | DelFile("path")                |
| 检测指定目录是否存在                         | IsExistDirectory                       | IsExistDirectory(@"D:\\sw\\"); |
| 检测指定文件是否存在                         | IsExistFile                            | IsExistFile("path")            |
| 获取指定目录中的文件列表                     | GetFileNames                           | GetFileNames(@"D:\\sw\\");     |
| 获取指定目录中所有子目录列表                 | GetDirectories                         | GetDirectories(@"D:\\sw\\");   |
| 获取指定目录及子目录中所有文件列表           | GetFileNames                           |                                |
| 检测指定目录是否为空                         | IsEmptyDirectory(string directoryPath) | IsEmptyDirectory(@"D:\\sw\\"); |
| 检测指定目录中是否存在指定的文件             | Contains                               | Contains(@"D:\\sw\\","*");     |
| 复制文件夹(递归)                             | CopyFolder                             |                                |
| 检查文件,如果文件不存在则创建                | ExistsFile                             |                                |
| 删除指定文件夹对应其他文件夹里的文件         | DeleteFolderFiles                      |                                |
| 从文件的绝对路径中获取文件名( 包含扩展名 )   | GetFileName                            |                                |
| 创建一个目录                                 | CreateDirectory                        |                                |
| 创建一个文件                                 | CreateFile                             |                                |
| 创建一个文件,并将字节流写入文件              | CreateFile                             |                                |
| 获取文本文件的行数                           | GetLineCount                           |                                |
| 获取一个文件的长度                           | GetFileSize                            |                                |
| 获取指定目录及子目录中所有子目录列表         | GetDirectories                         |                                |
| 向文本文件中写入内容                         | void WriteText                         |                                |
| 向文本文件的尾部追加内容                     | void AppendText                        |                                |
| 将现有文件的内容复制到新文件中               | Copy                                   |                                |
| 从文件的绝对路径中获取文件名( 不包含扩展名 ) | GetFileNameNoExtension                 |                                |
| 从文件的绝对路径中获取扩展名                 | GetExtension                           |                                |
| 清空文件内容                                 | ClearFile                              |                                |
| 删除指定目录及其所有子目录                   | DeleteDirectory                        |                                |
|                                              |                                        |                                |
| **electron**                                 |                                        |                                |
| ShellExecute打开程序/文件夹                  | VOpenFile                              | VOpenFile("D:\\sw");           |

### MechIni

ini文件操作类

- 静态类名：MechIni

- 调用方式：MechIni.方法

- 支持版本：**MechTE_452,480**

| 描述             | 函数         | 调用示例 |
| ---------------- | ------------ | -------- |
| 写入ini          | WriteIni     |          |
| 读取ini          | ReadIni      |          |
| 读取ini string[] | ReadIniArray |          |
| 删除一个INI文件  | DeleteIni    |          |

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









