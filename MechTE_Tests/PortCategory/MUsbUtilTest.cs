using MechTE_480.PortCategory.usb;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.PortCategory
{
    public class MUsbUtilTest
    {
        private readonly ITestOutputHelper _msg;

        public MUsbUtilTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        [Fact]
        public void GetDeviceName()
        {
            var ret = MUsbUtil.GetDeviceName(0x413c, 0xa527,"Dell");
           _msg.WriteLine(ret);
            Assert.Equal(ret, ret);
        } 
        
       
    }
}