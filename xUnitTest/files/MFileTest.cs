using MechTE_480.Files;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.files
{
    public class MFileTest
    {
        private readonly ITestOutputHelper _msg;

        public MFileTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        
        [Fact]
        public void OpenFile()
        {
            MFile.OpenFile(@"D:\sw\winfrom\Merry-exeStartTool\bin\exeStartTool\dw");
        }
        
        [Fact]
        public void OpenProcessFile()
        {
            MFile.OpenProcessFile(@"D:\sw\winfrom\Merry-exeStartTool\bin\exeStartTool\dw");
        }  
        
        [Fact]
        public void CopyFile()
        {
           var ret= MFile.CopyFile(@"C:\Users\ch190006\Desktop\test",@"C:\Users\ch190006\Desktop\test2");
            Assert.Equal(1,ret);
        }
     

        [Fact]
        public void DownloadZip()
        {
            const string downPath = @"D:\TE-Download";
            // EngineeringMode
            //  var data = MFileTransfer.DownloadZip("http://10.55.2.25:20005/api/PostDownloadZIP", "TestItem",downPath, downPath, "HDT657");
            var data = MFileTransfer.DownloadZip("http://10.55.2.25:20005/api/PostDownloadZIP", "EngineeringMode", downPath, "test");
            Assert.True(data);
        }

        [Fact]
        public void UploadZip()
        {
            //PostUploadloadFileEngineeringMode 工程模式
            //PostUploadloadFileTestItem 量产模式
            var data = MFileTransfer.UploadZip("http://10.55.2.25:20005/api/PostUploadloadFileEngineeringMode",
                @"C:\Users\ch190006\Desktop\服务器\test.zip");
            Assert.True(data);
        }
    }
}