using System;
using System.Threading;
using System.Threading.Tasks;
using MechTE_Speech;

namespace MechTE.Test
{
    
    class Program
    {
        // 获取 Singleton 的唯一实例  
        private static readonly SpeechSingleton SSingletonInstance = SpeechSingleton.Instance;
        static void Main(string[] args)
        {
            Task.Run(() => MSpeech.OutSpeech(new[] { "开始测试", "123", "456" }));
            Thread.Sleep(1000);
            Console.WriteLine("回车退出!");
            Console.ReadKey();
            SSingletonInstance.SpeechState = false;
            Console.ReadKey();
        }
    }
}