using MechTE_452.Data;
using Xunit;

namespace xUnit_Test
{
    public class UtilsTest
    {
        //GenerateNumberStringSequence
        [Fact]
        public void GenerateNumberStringSequence()
        {
            var data = MechUtils.GenerateNumberStringSequence(1, 10);
            Assert.Equal("1 2 3 4 5 6 7 8 9 10", data.ToString());
        }
    }
}