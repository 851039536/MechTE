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
            var data = MechWin. MesBoxs("12","123",1);
            Assert.Equal(1,data);
        }        
        
        [Fact]
        public void EnterHfp()
        {
            var data = MechWin.EnterHfp();
            Assert.True(data);
        }   
        
        [Fact]
        public void GetMasterVolume()
        {
            var data = MechWin.GetMasterVolume();
            _msg.WriteLine(data.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(data,data);
        }  
        
        [Fact]
        public void SetMasterVolume()
        {
             MechWin.SetMasterVolume(20);
             var data = MechWin.GetMasterVolume();
            _msg.WriteLine(data.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(data,data);
        }     
        
        [Fact]
        public void SetMasterVolumeMute()
        {
             MechWin.SetMasterVolumeMute(true);
             var data = MechWin.GetMasterVolume();
            _msg.WriteLine(data.ToString(CultureInfo.InvariantCulture));
            Assert.Equal(data,data);
        }  
    }
}