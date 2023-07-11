using MechTE_452.MECH;
using Xunit;

namespace xUnit_Test
{
    public class IniTest
    {
        //read ini
        [Fact]
        public void ReadIni()
        {
            var data = MechIni.ReadIni("test", "test", "test.ini");
            Assert.Equal("test", data);
        }
    }
}