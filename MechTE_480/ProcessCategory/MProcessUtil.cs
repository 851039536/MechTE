using System;
using System.Diagnostics;
using System.IO;
using MechTE_480.util;

namespace MechTE_480.ProcessCategory
{
    /// <summary>
    /// 使用进程调用cmd命令或程序
    /// </summary>
    public partial class MProcessUtil
    {
        #region 单个功能

        /// <summary>
        /// 根据名称获取wifi密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetWiFiPassword(string value)
        {
            var v= ExCmd($"netsh wlan show profiles {value} key=clear");
            var ret = v.Split(new[] { "\r\n" }, StringSplitOptions.None);

            foreach (var v1 in ret)
            {
                if (v1.Contains("关键内容"))
                {
                    var v2 = v1.Split(':');
                    return  v2[1];
                }
            }
            return "查询失败";
        }

        #endregion


        /// <summary>
        /// 执行单个cmd命令获取返回值
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static string ExCmd(string cmd)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe", // 设置要运行的程序为cmd.exe  
                UseShellExecute = false, // 不使用操作系统shell启动进程  
                RedirectStandardOutput = true, // 将标准输出重定向到Process.StandardOutput  
                RedirectStandardInput = true, // 将标准输入重定向到Process.StandardInput  
                CreateNoWindow = true, // 不创建新窗口  
                // 你可以在这里设置要执行的命令，例如：  
                Arguments = $"/c {cmd}" // 执行dir命令，并立即退出  
            };
            // 启动进程  
            using var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            // 读取命令行输出  
            string output = process.StandardOutput.ReadToEnd();
            // 如果需要，也可以向命令行发送输入  
            // process.StandardInput.WriteLine("some input");  
            // 等待进程退出  
            process.WaitForExit();
            // 输出结果  
            Console.WriteLine(output);
            return output;
        }

        /// <summary>
        /// 执行多个cmd,每次都会创建一次进程
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
        public static string ExCmd(string[] commands)
        {
            string output = "";
            foreach (var cmd in commands)
            {
                // 创建ProcessStartInfo对象  
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;

                // 设置要执行的命令  
                startInfo.Arguments = "/c " + cmd; // 注意命令前的空格  

                // 创建并启动Process  
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();

                    // 读取命令输出  
                    output = process.StandardOutput.ReadToEnd();

                    // 等待进程退出  
                    process.WaitForExit();

                    // 输出结果  
                    Console.WriteLine(output);
                }
            }

            return output;
        }

        /// <summary>
        /// 单个线程执行多个cmd指令
        /// </summary>
        /// <param name="commands"></param>
        public static void ExCmdWrite(string[] commands)
        {
            // 创建ProcessStartInfo对象  
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true; // 重定向标准输入  
            startInfo.RedirectStandardOutput = true; // 重定向标准输出  
            startInfo.RedirectStandardError = true; // 可选：重定向标准错误输出  
            startInfo.CreateNoWindow = true; // 不创建新窗口  

            // 创建并启动Process  
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.Start();

                // 获取StandardInput的StreamWriter用于写入命令  
                using (StreamWriter writer = process.StandardInput)
                {
                    // 向cmd发送命令  
                    foreach (var fo in commands)
                    {
                        writer.WriteLine(fo);
                    }
                }

                // 读取命令输出  
                string output = process.StandardOutput.ReadToEnd();
                // 可选：读取错误输出  
                string errorOutput = process.StandardError.ReadToEnd();
                // 等待进程退出  
                process.WaitForExit();
                // 输出结果  
                Console.WriteLine("Output:");
                Console.WriteLine(output);

                if (!string.IsNullOrEmpty(errorOutput))
                {
                    Console.WriteLine("Error Output:");
                    Console.WriteLine(errorOutput);
                }
            }
        }

        /// <summary>
        /// 执行bat文件
        /// </summary>
        /// <param name="cmd"></param>
        public static void Bat(string cmd)
        {
            ExeBat(cmd);
        }


        #region 启动应用网站

        /// <summary>
        /// 启动应用网站
        /// </summary>
        /// <param name="appName">/程序路径</param>
        /// <returns>bool</returns>
        public static bool StartApp(string appName)
        {
            return StartApp(appName, ProcessWindowStyle.Hidden);
        }

        /// <summary>
        /// 在程序目录下启动应用(管理员运行)
        /// </summary>
        /// <param name="appName"></param>
        public static void StartApps(string appName)
        {
            // 管理员启动并传值
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = MUtil.GetCurrentProgramDirectory() + appName,
                Verb = "runas" // 请求管理员权限
            };
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"无法以管理员权限重新启动应用程序：" + ex.Message);
            }
        }

        #endregion

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
    }
}