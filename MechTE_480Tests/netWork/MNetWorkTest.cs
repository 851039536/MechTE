using MechTE_480.network;
using Xunit;
using Xunit.Abstractions;

namespace VSxUnitTest.netWork
{
    public class MNetWorkTest
    {
        private readonly MNetHelper _mNetHelper = new MNetHelper();

        private readonly ITestOutputHelper _msg;

        public MNetWorkTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        [Fact]
        public void GetAddressIp()
        {
            var ret = MNetHelper.GetAddressIp();
            _msg.WriteLine(ret);
            Assert.Equal(ret,ret);
        }

        [Fact]
        public void GetValidPort()
        {
            var ret = MNetHelper.GetValidPort("65333");
            _msg.WriteLine(ret.ToString());
            Assert.Equal(ret,ret);
        }


        [Fact]
        public void StringToIpAddress()
        {
            var ret = MNetHelper.StringToIpAddress("172.16.202.14");
            _msg.WriteLine(ret.ToString());
            Assert.Equal(ret,ret);
        }

        [Fact]
        public void GetHostName()
        {
            var ret = MNetHelper.GetHostName();
            _msg.WriteLine(ret);
            Assert.Equal(ret,ret);
        } 
        
        [Fact]
        public void LANIP()
        {
            var ret = MNetHelper.LANIP();
            _msg.WriteLine(ret);
            Assert.Equal(ret,ret);
        }   
        
        [Fact]
        public void WANIP()
        {
            var ret = MNetHelper.WANIP();
            _msg.WriteLine(ret);
            Assert.Equal(ret,ret);
        }
    }
}