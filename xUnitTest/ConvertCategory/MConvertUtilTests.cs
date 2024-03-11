using MechTE_480.ConvertCategory;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.ConvertCategory
{
    public class MConvertUtilTests
    {
        private readonly ITestOutputHelper _msg;
        public MConvertUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        
        [Fact]
        public void ToInt32()
        {
            var data = "18".MToInt32();
            _msg.WriteLine(data.ToString());
            Assert.Equal(data,data);
        }
    }
}