using MechTE_480.FileCategory;
using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.FileCategory
{
    public class MFileUtilTests
    {
        private readonly ITestOutputHelper _msg;
        private readonly string _cuPath;


        public MFileUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
            _cuPath = MUtil.GetCurrentProgramDirectory();
        }

        [Fact]
        public void OpenFile()
        {
            MFileUtil.OpenFile(@"D:\sw\winfrom\Merry-exeStartTool\bin\exeStartTool\dw");
        }

        [Fact]
        public void CopyFile()
        {
            var ret = MFileUtil.CopyFile(@"C:\Users\ch190006\Desktop\test", @"C:\Users\ch190006\Desktop\test2");
            Assert.Equal(1, ret);
        }
    }
}