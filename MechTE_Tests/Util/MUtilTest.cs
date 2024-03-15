using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.Util
{
    public class MUtilTest
    {
        private readonly ITestOutputHelper _msg;
        public MUtilTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        [Fact]
        public void IsInt()
        {
            var data = MUtil.IsInt("DFSD,w123T");
            _msg.WriteLine(data.ToString()); 
            var data2 = MUtil.IsInt("123214");
            _msg.WriteLine(data2.ToString());
            Assert.Equal(data,data);
        } 
        
        [Fact]
        public void IsNumber()
        {
            var data = MUtil.IsNumber("312123T");
            _msg.WriteLine(data.ToString()); 
            var data2 = MUtil.IsNumber("123214");
            _msg.WriteLine(data2.ToString());
            Assert.Equal(data,data);
        }
          
        
       

    }
}