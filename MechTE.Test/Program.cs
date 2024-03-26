using System;
using MechTE_OpenCv;

namespace MechTE.Test
{
    class Program
    {
        private static void Main(string[] args)
        {
           var ret=  MOpenCv.ReadRGB(@"C:\Users\ch190006\Desktop\test\1.png");
           Console.WriteLine(ret);
        }
    }

}