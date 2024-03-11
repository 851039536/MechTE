using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using MechTE_480.util;
using MechTE_480.Util;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test
{
    public class UtilsTest
    {
        private readonly ITestOutputHelper _msg;

        public UtilsTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        //GenerateNumberStringSequence
        [Fact]
        public void GenerateNumberStringSequence()
        {
            var data = MUtil.GenerateNumberSequence(1, 10);
            Assert.Equal("1 2 3 4 5 6 7 8 9 10", data.ToString());
        }      
        

        // 示例方法，接受一个字符串参数，并返回一个字典
        public static Dictionary<Guid, string> Test(string sourceString)
        {
            // 将字符串转换为字典，每个字符作为键，使用Guid作为值
            var result = sourceString.ToDictionary(
                character => Guid.NewGuid(),
                character => character.ToString(CultureInfo.InvariantCulture));
            // 模拟耗时操作，暂停4秒
            Thread.Sleep(4000);
            
            return result;
        }
        [Fact]
        public void Execute()
        {
            Dictionary<Guid, string> result;
            
            // 调用TimeoutFunction类的Execute方法执行带有超时检查的方法
            // Test方法是一个示例方法，它接受一个字符串参数，并返回一个字典
            // "Hello, World!"是传递给Test方法的参数
            // result是用于接收Test方法的返回值的字典
            // TimeSpan.FromSeconds(3)表示超时时间为3秒
            // Execute方法返回一个布尔值，表示是否超时
           var ret=   MUtil.Execute(Test, "Hello, World!", out result, TimeSpan.FromSeconds(3));
            _msg.WriteLine(ret.ToString());
        }      
        
        [Fact]
        public void MMeasure()
        {
            using (new MMeasure(duration => _msg.WriteLine($"执行时间：{duration}")))
            {
                // 在这里编写需要测量执行时间的代码
                for (int i = 0; i < 5; i++)
                {
                    // 一些耗时的操作
                    Thread.Sleep(1000);
                }
            }
        }      
    }
}