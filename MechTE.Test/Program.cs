using System;
using System.IO;

namespace MechTE.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string[] strArray= File.ReadAllLines(@"D:\sw\class_library\MechTE\MechTE_ContextMenu\bin\Debug\config\Config.txt");	
            for(int i=0;i<strArray.Length;i++)
            {
                // strArray[i]+ "\r\n";
                Console.WriteLine(strArray[i]);
            }
          
            //3.模拟测试项
            while (true)
            {
                var cmd = Console.ReadLine();
                // Console.WriteLine(merryDll.Run(cmd));
            }
        }
    }
}

