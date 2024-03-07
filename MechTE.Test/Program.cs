using System;
using MechTE_480.ProcessCategory;

namespace MechTE.Test
{
    internal class Program
    {
     

        static void Main(string[] args)
        {
            
            Console.WriteLine(MProcessUtil.GetWiFiPassword("PM_SF"));
            // MProcessUtil.ExCmd("mstsc");
            
             //MProcessUtil.ExCmdWrite(new[] { "d:", @"cd D:\sw\model\MSP168\MSP168\MerryDll\bin\Debug\fw", "FWupdate.exe /VID_03F0 /PID_0D84 -USB -Ver" });
            // MProcessUtil.ExCmd(new[] { "ipconfig", "mstsc", "notepad" });
            Console.ReadKey();
        }
        
        
    }
}