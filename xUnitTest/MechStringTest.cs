using MechTE_480.MECH;
using Xunit;
namespace xUnit_Test
{
    public class MechStringTest
    {
        //GenerateNumberStringSequence
        [Fact]
        public void HexadecimalToASCII()
        {
           // var data= MechString.HexadecimalToASCII("0676312E342E30");
           var data= MechConvert.HexadecimalToASCII("54");
           // data = data.Substring(1, 6);
            Assert.Equal("v1.4.0", data);
        }    
        [Fact]
        public void ASCIIConvertsDecimal16()
        {
           var data2= MechConvert.HexadecimalToASCII("0676312E342E30");
           var data= MechConvert.ASCIIConvertsDecimal16(data2);
           // data = data.Substring(1, 6);
            Assert.Equal("v1.4.0", data);
        }
        

    }
}