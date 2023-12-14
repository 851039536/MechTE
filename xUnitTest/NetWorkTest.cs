using System.Net;
using MechTE_480.network;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test
{
    public class NetWorkTest
    {
        private readonly NetHelper _netHelper = new NetHelper();
        
        private readonly ITestOutputHelper _msg;

        public NetWorkTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        [Fact]
        public void GetAddressIp()
        {
           var ret=  NetHelper.GetAddressIp();
           _msg.WriteLine(ret);
            Assert.Equal(ret, ret);
        } 
        
        [Fact]
        public void GetValidPort()
        {
           var ret=  NetHelper.GetValidPort("65333");
           _msg.WriteLine(ret.ToString());
            Assert.Equal(ret, ret);
        }  
      
        
        [Fact]
        public void StringToIpAddress()
        {
           var ret=  NetHelper.StringToIpAddress("172.16.202.14");
           _msg.WriteLine(ret.ToString());
            Assert.Equal(ret, ret);
        }  
        
        [Fact]
        public void GetHostName()
        {
            var ret= NetHelper.LocalHostName(); 
           _msg.WriteLine(ret.ToString());
            Assert.Equal(ret, ret);
        }
    }
}