using System;
using System.Runtime.CompilerServices;

namespace MechTE.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //直接调用
            DirectProcessing("*******直接调用********");
            //间接调用
            InirectProcessing();
            Console.ReadKey();
        }

        private static void InirectProcessing()
        {
            DirectProcessing("********间接调用*********");
        }

        private static void DirectProcessing(string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine("信息为: " + message);
            Console.WriteLine("方法名称: " + memberName);
            Console.WriteLine("源文件地址: " + sourceFilePath);
            Console.WriteLine("方法使用所在行号: " + sourceLineNumber);
        }
    }
}