using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.util
{
    public class MConvertTest
    {
        private readonly ITestOutputHelper _msg;
        public MConvertTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        public void ConvertBase()
        {
            var data = MConvert.ConvertBase("18", 10, 16);
            _msg.WriteLine(data);
            Assert.Equal(data, data);
        }      
       
        [Fact]
        public void HexToAscii()
        {
            var data= MConvert.HexToAscii("0676312E342E30");
            Assert.Equal("v1.4.0", data);
        }    
        [Fact]
        public void AsciiToHex()
        {
            var data2= MConvert.HexToAscii("0676312E342E30");
            var data= MConvert.AsciiToHex(data2);
            // data = data.Substring(1, 6);
            Assert.Equal("v1.4.0", data);
        }
    }
}