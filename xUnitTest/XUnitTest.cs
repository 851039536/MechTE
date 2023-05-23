using MechTE.Cmd;
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
    }
}