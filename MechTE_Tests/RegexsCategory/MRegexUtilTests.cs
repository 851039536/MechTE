using MechTE_480.RegexsCategory;
using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.RegexsCategory
{
    public class MRegexUtilTests
    {
        private readonly ITestOutputHelper _msg;
        public MRegexUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        public void IsEmails()
        {
            var data = MRegexUtil.IsEmail("DFSD,w123T");
            _msg.WriteLine(data.ToString()); 
            var data2 = MRegexUtil.IsEmail("851039538@qq.com");
            _msg.WriteLine(data2.ToString());
            Assert.Equal(data,data);
        } 
        
        
       

    }
}