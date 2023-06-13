using Xunit;

namespace xUnit_Test
{
    public class IniTest
    {
        //read ini
        [Fact]
        public void ReadIni()
        {
            var data = MechTE_452.Ini.MechIni.ReadIni("test", "test", "test.ini");
            Assert.Equal("test", data);
        }
    }
}