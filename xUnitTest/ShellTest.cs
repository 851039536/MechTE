using MechTE_452.Cmd;
using MechTE_452.ConvertHelper;
using MechTE_452.Data;
using Xunit;

namespace xUnit_Test
{
    public class ShellTest
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
        
        [Fact]
        public void ConvertBase()
        {
            var data = ConvertHelpers.ConvertBase("1", 10, 16);
            Assert.Equal("1", data);
        }
    }
}