using MechTE.Cmd;
using MechTE.ConvertHelper;
using Xunit;

namespace xUnit_Test
{
    public class XUnitTest
    {
        [Fact]
        public void StartShell()
        {
            Cmd.StartShell("notepad");
            Assert.True(true);
        }

        [Fact]
        public void StartApp()
        {
           var data = Cmd.StartApp(@"D:\software\Notepad++\notepad++.exe");
            Assert.Equal(true, data);
        }
        
        [Fact]
        public void RepairZero()
        {
            var data = ConvertHelpers.RepairZero("1234", 5);
            Assert.Equal("01234", data);
        }
    }
}