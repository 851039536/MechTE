using System;
using System.Reflection;
using System.Threading;
using System.Timers;

namespace MechTE.Test
{
    internal class Program
    {
     

        static void Main(string[] args)
        {
            System.Media.SystemSounds.Asterisk.Play(); 
            Thread.Sleep(1000);
            System.Media.SystemSounds.Beep.Play(); 
            Thread.Sleep(1000);
            System.Media.SystemSounds.Exclamation.Play(); 
            Thread.Sleep(1000);
            System.Media.SystemSounds.Hand.Play(); 
            Thread.Sleep(1000);
            System.Media.SystemSounds.Question.Play();
            Console.ReadKey();
        }
    }
}