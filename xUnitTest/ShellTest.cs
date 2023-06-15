using MechTE_452.Cmd;

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
        

    }
}