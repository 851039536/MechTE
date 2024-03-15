using System.Globalization;
using MechTE_480.Windows;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test
{
    public class WinTest
    {
        private readonly ITestOutputHelper _msg;

        public WinTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        public void MesBoxs()
        {
            var data = MWin.MesBoxs("12", "123", 1);
            Assert.Equal(1, data);
        }


        [Fact]
        public void GetMasterVolume()
        {
            var data = MWin.GetMasterVolume();
            _msg.WriteLine(data.ToString(CultureInfo.CurrentCulture));
            Assert.Equal(data, data);
        }

        [Fact]
        public void SetMasterVolume()
        {
            MWin.SetMasterVolume(20);
            var data = MWin.GetMasterVolume();
            _msg.WriteLine(data.ToString(CultureInfo.CurrentCulture));
            Assert.Equal(data, data);
        }

        [Fact]
        public void SetMasterVolumeMute()
        {
            MWin.SetMasterVolumeMute(true);
            var data = MWin.GetMasterVolume();
            _msg.WriteLine(data.ToString(CultureInfo.CurrentCulture));
            Assert.Equal(data, data);
        }

        [Fact]
        public void OpenA2Dp()
        {
            MWin.OpenA2Dp();
        }

        [Fact]
        public void EnterHfp()
        {
            var data = MWin.EnterHfp();
            Assert.True(data);
        }

        [Fact]
        public void CloseRunDll()
        {
            MWin.CloseRunDll();
        }

        [Fact]
        public void IsConnectInternet()
        {
            var ret = MWin.PingIpOrDomainName("www.cnblogs.com");
            Assert.True(ret);
        }
        
    }
}