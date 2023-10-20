using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MechTE_480.Order
{
    /// <summary>
    /// cmd包装类
    /// </summary>
    public partial class MechProcess
    {
        /// <summary>
        /// 执行多条cmd.exe命令
        /// </summary>
        /// <param name="commandTexts"></param>
        private static void ExeCommand(IEnumerable<string> commandTexts)
        {
            //表示在操作系统上运行的进程
            var p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            try
            {
                p.Start();
                foreach (var item in commandTexts)
                {
                    p.StandardInput.WriteLine(item);
                }

                p.StandardInput.WriteLine("exit");
                p.StandardOutput.ReadToEnd();
                //等待进程退出
                p.WaitForExit();
                p.Close();
            } catch (Exception)
            {
                // ignored
            }
        }


        /// <summary>
        /// 执行Bat
        /// </summary>
        /// <param name="name"></param>
        private static void ExeBat(string name)
        {
            // 创建一个ProcessStartInfo对象
            ProcessStartInfo processInfo = new ProcessStartInfo();
            // 设置要执行的bat文件路径
            processInfo.FileName = name;
            // 设置以管理员权限运行
            // processInfo.Verb = "runas";
            // 创建一个Process对象
            Process process = new Process();
            // 将ProcessStartInfo对象赋值给Process对象的StartInfo属性
            process.StartInfo = processInfo;
            // 启动进程
            process.Start();
        }

        /// <summary>
        /// 使用cmd执行Shell命名
        /// </summary>
        /// <param name="commandTexts"></param>
        /// <returns></returns>
        private static async Task<bool> ExeCommandAsync(IEnumerable<string> commandTexts)
        {
            using (var p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                try
                {
                    p.Start();
                    foreach (var item in commandTexts)
                    {
                        await p.StandardInput.WriteLineAsync(item);
                    }
                    await p.StandardInput.WriteLineAsync("exit");
                    await p.StandardOutput.ReadToEndAsync();
                    p.WaitForExit();
                    return true;
                } catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 进程窗口模式
        /// </summary>
        /// <param name="appName">应用程序路径名称</param>
        /// <param name="style">显示模式</param>
        /// <returns>bool</returns>
        private static bool StartApp(string appName,ProcessWindowStyle style)
        {
            return StartApp(appName,null,style);
        }
    }
}