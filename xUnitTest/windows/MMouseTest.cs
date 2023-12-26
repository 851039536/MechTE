using System.Drawing;
using System.Threading;
using MechTE_480.windows;
using Xunit;

namespace xUnit_Test.windows
{
    public class MMouseTest
    {
    
        [Fact]
        public void MouseMove()
        {
             Point p =  new Point(250, 140);
             MMouse.MouseMove(p, 20, 20);
             MMouse.MouseLeftClick();
             Thread.Sleep(2000);
             Point b =  new Point(310, 150);
             MMouse.MouseMove( b, 20, 20);
             MMouse.MouseLeftClick();
        }
    }
}