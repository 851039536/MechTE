using System.Drawing;
using System.Reflection;
using MechTE_480.windows;
using Xunit;

namespace xUnit_Test
{
    public class IniTest
    {
        //read ini
        [Fact]
        public void ReadIni()
        {
            
        } 
        [Fact]
        public void MouseMove()
        {
             Point p =  new Point(800, 800);
             MMouseControl.MouseMove(p, 20, 20);
        }
    }
}