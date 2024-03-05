using System;
using System.Timers;

namespace MechTE.Test
{
    internal class Program
    {
        // Elapsed事件处理程序  
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("触发了Elapsed事件");
        }

        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
}