using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MechTE.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //2.Task线程也是来自于线程池

            List<int> countList = new List<int>();
            List<Task> taskList = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                int k = i;
                taskList.Add(Task.Run(() =>
                {
                    countList.Add(Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine($@"k={k}");
                    Console.WriteLine($@"线程ID为:{Thread.CurrentThread.ManagedThreadId}");
                }));
            }

            Task.WaitAll(taskList.ToArray()); //等待所有的线程执行完毕
            Console.WriteLine($@"最大线程数目为：{countList.Distinct().Count()}");
            //输出结果里面只有4,5,6,7,8这几个ID的线程，最大线程数目为5
            Console.ReadKey();
        }
    }
}