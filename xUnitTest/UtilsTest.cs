using MechTE_480.MECH;
using Xunit;

namespace xUnit_Test
{

    
    public class UtilsTest
    {
        MechXML _mechXml = new MechXML("command_sw.xml");
        
        //GenerateNumberStringSequence
        [Fact]
        public void GenerateNumberStringSequence()
        {
            var data = MechUtils.GenerateNumberStringSequence(1, 10);
            Assert.Equal("1 2 3 4 5 6 7 8 9 10", data.ToString());
        }      
        
        [Fact]
        public void GetSelectedPath()
        {
            var data = MechUtils.GetSelectedPath();
            Assert.Equal("1 2 3 4 5 6 7 8 9 10", data);
        }


        
        

    }
}