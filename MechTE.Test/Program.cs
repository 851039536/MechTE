using MechTE_480.Form;
using System;

namespace MechTE.Test
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)

        {

            UpFile upFile = new UpFile();
            upFile.ShowDialog();

            Console.ReadLine();

            //3.模拟测试项
            //while (true)
            //{
            //    var cmd = Console.ReadLine();
            //    // Console.WriteLine(merryDll.Run(cmd));
            //}
        }
    }
}

