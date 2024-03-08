using MechTE_480.ConvertCategory;
using Xunit;
using Xunit.Abstractions;

namespace VSxUnitTest.util
{
    public class MConvertUtilTests
    {
        private readonly ITestOutputHelper _msg;
        public MConvertUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        
        [Fact]
        public void ToInt32()
        {
            var data = MConvertUtil.ToInt32("18");
            _msg.WriteLine(data.ToString());
            Assert.Equal(data,data);
        }
             [Fact]
        public void ToInt64()
        {
            var data = MConvertUtil.ToInt64("183213213214");
            _msg.WriteLine(data.ToString());
            Assert.Equal(data,data);
        }
        
        
        [Fact]
        public void ConvertBase()
        {
            var data = MConvertUtil.ConvertBase("18",10,16);
            _msg.WriteLine(data);
            Assert.Equal(data,data);
        }

        [Fact]
        public void HexToAscii()
        {
            var data = MConvertUtil.HexToAscii("0676312E342E30");
            Assert.Equal("v1.4.0",data);
        }
        [Fact]
        public void AsciiToHex()
        {
            var data2 = MConvertUtil.HexToAscii("0676312E342E30");
            var data = MConvertUtil.AsciiStrToHexStr(data2);
            // data = data.Substring(1, 6);
            Assert.Equal("v1.4.0",data);
        }
    }
}