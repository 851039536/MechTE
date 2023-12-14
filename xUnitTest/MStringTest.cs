using MechTE_480.MECH;
using MechTE_480.util;
using Xunit;
namespace xUnit_Test
{
    public class MStringTest
    {
        //GenerateNumberStringSequence
        [Fact]
        public void HexadecimalToASCII()
        {
           // var data= MechString.HexadecimalToASCII("0676312E342E30");
           var data= MConvert.HexadecimalToASCII("54");
           // data = data.Substring(1, 6);
            Assert.Equal("v1.4.0", data);
        }    
        [Fact]
        public void ASCIIConvertsDecimal16()
        {
           var data2= MConvert.HexadecimalToASCII("0676312E342E30");
           var data= MConvert.ASCIIConvertsDecimal16(data2);
           // data = data.Substring(1, 6);
            Assert.Equal("v1.4.0", data);
        }
        

    }
}