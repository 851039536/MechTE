using MechTE.Cmd;
using Xunit;

namespace xUnit_Test
{
    public class MechTest
    {
        [Fact]
        public void StartExe()
        {
            TCmd.StartExe("notepad");
            Assert.True(true);
        }

        [Fact]
        public void StartApp()
        {
           var data = TCmd.StartApp(@"D:\software\Notepad++\notepad++.exe");
            Assert.Equal(true, data);
        }
    }
}