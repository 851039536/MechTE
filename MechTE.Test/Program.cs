using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MechTE_480.Files;

namespace MechTE.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MFile.FileDialog();
            Console.ReadKey();
        }
    }
}