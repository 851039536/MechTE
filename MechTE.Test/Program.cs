using MechTE_480.Form;
using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using MechTE_480.port;

namespace MechTE.Test
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var name = MSerialPort.GetPortName();
            MPCBA_32.command(name[0], "1.2.3.4.5.6");
            Thread.Sleep(1000);
            MPCBA_32.command(name[0], "6.5.8.1.9.3");

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