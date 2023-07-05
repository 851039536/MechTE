using System;
using System.Collections.Generic;
using System.Linq;
using MechTE_Shell;
using Microsoft.Win32;

namespace MechTE.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ArrContextMenu menu = new ArrContextMenu();
            string rootPath = menu.getRootPath();
            Console.WriteLine(rootPath+"2");
            List<string> paths = menu.SelectedItemPaths.ToList();
            paths.Add(".exe");
            foreach (var r in paths)
            {
                Console.WriteLine(r.ToString());
            }
           
            // RegistryHelper.CreateUserContextMenu("test",@"D:\sw\winfrom\Merry-DesktopTool\TestTool\bin\Debug\net6.0-windows\TestTool.exe");
            // RegistryHelper.DeleteUserContextMenu("test");
            Console.ReadKey();
        }
        /// <summary>
        /// 注册表帮助类
        /// 版本：v1
        /// 日期：2022年10月15日
        /// 作者：hxsfx
        /// </summary>
        public class RegistryHelper
        {
            /// <summary>
            /// 计算机\HKEY_CURRENT_USER\Software\Classes\*\shell
            /// </summary>
            private static RegistryKey RegistryUserSoftwareClassesAllShell
            {
                get
                {
                    return Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Classes").OpenSubKey("*").OpenSubKey("shell", true);
                }
            }
            /// <summary>
            /// 生成右键菜单按钮（当前用户）
            /// </summary>
            /// <param name="keyName">右键菜单名</param>
            /// <param name="programPath">点击按钮后打开的程序路径</param>
            public static void CreateUserContextMenu(string keyName, string programPath)
            {
                if (RegistryUserSoftwareClassesAllShell.GetSubKeyNames().Contains(keyName)) return;
                var contextMenuRegistryKey = RegistryUserSoftwareClassesAllShell.CreateSubKey(keyName);
                contextMenuRegistryKey.SetValue("", keyName);
                var command = contextMenuRegistryKey.CreateSubKey("command");
                command.SetValue("", $"\"{programPath}\" \"%1\"");
            }
            /// <summary>
            /// 删除右键菜单按钮（当前用户）
            /// </summary>
            /// <param name="keyName">右键菜单名</param>
            public static void DeleteUserContextMenu(string keyName)
            {
                if (RegistryUserSoftwareClassesAllShell.GetSubKeyNames().Contains(keyName))
                {
                    RegistryUserSoftwareClassesAllShell.DeleteSubKeyTree(keyName);
                }
            }
        }


    }
}