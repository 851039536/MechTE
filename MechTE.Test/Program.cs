using System;
using MechTE_480.Files;
using MechTE_480.port.hid;
using MechTE_480.port.usb;

namespace MechTE.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MHid hid = new MHid();
          var ret=  hid.GetHandle("a520", "413c");
          Console.WriteLine(ret);
            Console.ReadKey();
        }
    }
}