using MechTE_452.MECH;
using Xunit;

namespace xUnit_Test
{
    public class ShellTest
    {



        [Fact]
        public void StartShell()
        {
            MechCmd.StartShell("notepad");
            Assert.True(true);
        }

        [Fact]
        public void StartApp()
        {
           var data = MechCmd.StartApp(@"D:\software\Notepad++\notepad++.exe");
            Assert.Equal(true, data);
        }
        

    }
}