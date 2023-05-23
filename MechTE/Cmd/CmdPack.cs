using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MechTE.Cmd
{
    /// <summary>
    /// cmd包装类
    /// </summary>
    public static class CmdPack
    {
        /// <summary>
        /// 执行多条cmd.exe命令
        /// </summary>
        /// <param name="commandTexts"></param>
        public static void ExeCommand(IEnumerable<string> commandTexts)
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
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        /// <summary>
        /// 使用cmd执行Shell命名
        /// </summary>
        /// <param name="commandTexts"></param>
        /// <returns></returns>
        public static async Task<bool> ExeCommandAsync(IEnumerable<string> commandTexts)
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
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}