using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;

namespace MechTE_480.MECH
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class MechUtils 
    {
   
        
        /// <summary>
        ///  定义一个泛型委托，用于定义带有超时检查的方法的签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR"></typeparam>
        public delegate TR TimeOutDelegate<in T, out TR>(T param);
        /// <summary>
        /// 执行带有超时检查的方法
        /// </summary>
        /// <param name="timeoutMethod">目标方法</param>
        /// <param name="param">目标方法的参数</param>
        /// <param name="result">执行结果</param>
        /// <param name="timeout">超时时间</param>
        /// <typeparam name="T">目标方法的参数类型</typeparam>
        /// <typeparam name="TR">执行结果的类型</typeparam>
        /// <returns>是否超时</returns>
        public static bool Execute<T, TR>(
            TimeOutDelegate<T, TR> timeoutMethod, T param, out TR result, TimeSpan timeout)
        {
            // 使用异步方式执行目标方法
            var asyncResult = timeoutMethod.BeginInvoke(param, null, null);
            
            // 等待指定的超时时间
            if (!asyncResult.AsyncWaitHandle.WaitOne(timeout, false))
            {
                // 如果超时，则将结果设置为默认值，并返回true
                result = default(TR);
                return true;
            }
            // 如果未超时，则获取执行结果，并返回false
            result = timeoutMethod.EndInvoke(asyncResult);
            return false;
        }
        
        
        /// <summary>
        /// 在指定的时间内等待某个函数的执行结果，并返回一个布尔值表示是否等待成功,
        /// 调用 bool result = WaitSomething(5000, 1000, () =>{})
        /// </summary>
        /// <param name="timeout">表示等待的最大时间，以毫秒为单位</param>
        /// <param name="freq">表示等待的频率，即每隔多少毫秒检查一次函数的执行结果</param>
        /// <param name="func">表示要等待的函数，它是一个返回布尔值的委托</param>
        /// <returns></returns>
        public static bool WaitSomething(int timeout, int freq, Func<bool> func)
        {
            for (int index = 0; index < timeout; index += freq)
            {
                if (func())
                    return true;
                Thread.Sleep(freq);
            }
            return false;
        }
        /// <summary>
        /// 判断当前程序是否是管理员
        /// </summary>
        /// <returns></returns>
        public static bool IsUserAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// 重新启动应用程序并请求管理员权限
        /// </summary>
        public static void RestartAsAdministrator()
        {
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory
            };
            var processModule = Process.GetCurrentProcess().MainModule;
            if (processModule != null) startInfo.FileName = processModule.FileName;
            startInfo.Verb = "runas"; // 请求管理员权限
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"无法以管理员权限重新启动应用程序：" + ex.Message);
            }
        }
        /// <summary>
        /// 获取当前程序根目录,
        ///如: D:\sw\Console\FileDownLoad\DownLoad\bin\Debug
        /// </summary>
        /// <returns></returns>
        public static string GetTheCurrentProgramAndDirectory()
        {
            // 获取当前程序集的执行路径(根目录)D:\sw\Console\FileDownLoad\DownLoad\bin\Debug
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// 生成数字字符串序列
        /// </summary>
        /// <param name="startNumber">序列中第一个整数的值</param>
        /// <param name="sequenceLength">生成的顺序总条数</param>
        /// <returns>传入0,6 生成 0 1 2 3 4 5</returns>  
        public static string GenerateNumberStringSequence(int startNumber, int sequenceLength)
        {
            // 生成一个包含数字字符串的序列                                                                                                                                 
            var strLen = Enumerable.Range(startNumber, sequenceLength).Select(i => i.ToString())
                .Aggregate((a, b) => a + " " + b);
            return strLen;
        }

        /// <summary>
        /// 将字符串转换为字节数组
        /// 示例："ABCDEF" -> [ 0xAB, 0xCD, 0xEF ]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<byte> StringToByteArray(string str)
        {
            var bytes = new List<byte>();
            for (int i = 0; i < str.Length - 1; i += 2)
            {
                // 将字符串中的每两个字符转换为一个字节
                var byteStr = $"0x{str[i]}{str[i + 1]}";
                bytes.Add(Convert.ToByte(byteStr, 16));
            }

            return bytes;
        }

        /// <summary>
        /// 将字节数组转换为十六进制字符串
        /// 示例：[ "AB", "CD", "EF" ] -> "AB{separator}CD{separator}EF"
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string ByteArrayToHexStrings(List<byte> bytes, string separator = "")
        {
            var result = "";
            foreach (var item in bytes)
            {
                // 将字节转换为十六进制字符串，并添加分隔符
                result += $"{item:X2}{separator}";
            }

            // 去除最后一个分隔符
            result = result.Substring(0, result.Length - separator.Length);
            // 返回十六进制字符串
            return result;
        }

        /// <summary>
        /// 将字符转换HID指令格式 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns>"keyValue=0021032334 > 00 21 03 23 34</returns>
        public static string CharacterConversionHidFormat(string keyValue)
        {
            string[] splitStrings = new string[keyValue.Length / 2];
            for (int i = 0; i < splitStrings.Length; i++)
            {
                splitStrings[i] = keyValue.Substring(i * 2, 2);
            }

            string formattedStringKey = string.Join(" ", splitStrings);
            return formattedStringKey;
        }

        /// <summary>
        ///  判断字符串是否为空,空等于true，抛出异常
        /// </summary>
        /// <param name="str"></param>
        /// <param name="errMsg"></param>
        public static void IsEmptyAssert(string str, string errMsg)
        {
            //如果为空，则等于true，抛出异常
            Assert(string.IsNullOrEmpty(str), errMsg);
        }

        /// <summary>
        /// 自定义断言方法， result == true 抛出异常
        /// </summary>
        /// <param name="result">bool</param>
        /// <param name="errMsg">错误信息</param>
        /// <remarks>系统断言不能在 Release 版保留，用这个方法替代</remarks>
        // ReSharper disable once MemberCanBePrivate.Global
        public static void Assert(bool result, string errMsg)
        {
            if (result) throw new Exception(errMsg);
        }

        /// <summary>
        /// 自定义断言方法， func() == true 抛出异常    
        /// </summary>
        /// <param name="func"></param>
        /// <param name="errMsg"></param>
        /// <remarks>系统断言不能在 Release 版保留，用这个方法替代</remarks>
        public static void Assert(Func<bool> func, string errMsg)
        {
            Assert(func(), errMsg);
        }
    }
}