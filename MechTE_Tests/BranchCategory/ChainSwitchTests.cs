using MechTE_480.BranchCategory;
using MechTE_480.ConvertCategory;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.BranchCategory
{
    public class ChainSwitchTests
    {
        private readonly ITestOutputHelper _msg;

        public ChainSwitchTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        
        /// <summary>
        /// 将字符串转换为整型，转换失败返回0
        /// </summary>
        [Fact]
        public void ChainSwitch()
        {
            var ret = new ChainSwitch("2")
                .Case(ConvertNumberToString)
                .Case(ConvertNumberToString, "test23213")
                .Case("1",test,"test1")
                .Case("2",test,"test2");   
            
        _msg.WriteLine(ret.Values);
        }

        private string ConvertNumberToString(string number)
        {
            return number;
        }
        private string ConvertNumberToString()
        {
            return "number";
        }
        private string test(string name)
        {
            return name;
        }
       
    }
}