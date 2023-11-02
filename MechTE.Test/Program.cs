using MechTE_480.Form;
using System;
using System.IO;

namespace MechTE.Test
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)

        {
            ConsoleKeyInfo c;

            // 防止按下Ctrl+C时程序终止
            Console.TreatControlCAsInput = true;

            Console.WriteLine("按下任意键与CTL、ALT和SHIFT的组合键，或按下Esc键退出：\n");

            do {
                c = Console.ReadKey();
                Console.Write(" - 按下的键是 ");

                // 只输出按下的键
                Console.WriteLine(c.Key.ToString());

                // 退出条件
            } while (c.Key != ConsoleKey.Escape);
            //3.模拟测试项
            //while (true)
            //{
            //    var cmd = Console.ReadLine();
            //    // Console.WriteLine(merryDll.Run(cmd));
            //}
        }
    }
}