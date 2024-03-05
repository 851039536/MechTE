using MechTE_480.PortCategory.usb;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.port
{
    public class MUsbTest
    {
        private readonly MUsb _usb = new MUsb();
        
        private readonly ITestOutputHelper _msg;

        public MUsbTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        [Fact]
        public void GetDeviceName()
        {
            var ret = MUsb.GetDeviceName(0x413c, 0xa527,"Dell");
           _msg.WriteLine(ret);
            Assert.Equal(ret, ret);
        } 
        
       
    }
}