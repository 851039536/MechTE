using MechTE_480.AssertCategory;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.AssertCategory
{
    public class MAssertUtilTests
    {
        private readonly ITestOutputHelper _msg;
        public MAssertUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        
        [Fact]
        public void IsEmpty()
        {
             MAssertUtil.IsEmpty("","字符为空则空抛出异常");
        } 
       
    }
}