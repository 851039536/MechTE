using MechTE_480.MECH;
using Xunit;

namespace xUnit_Test
{
    public class MechUtilsTest
    {
        
        [Fact]
        public void EnterHfp()
        {
            var data = MechWin.EnterHfp();
            Assert.True(data);
        }      
    }
}