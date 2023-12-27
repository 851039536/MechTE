using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.util
{
    public class MStringTest
    {
        private readonly ITestOutputHelper _msg;
        public MStringTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        public void StringToListStr()
        {
            var data = MString.StringToListStr("DFSD,w123T",',',true);

            foreach (var item in data)
            {
                _msg.WriteLine(item);
            }
            Assert.Equal(data,data);
        }

        [Fact]
        public void StringToArray()
        {
            var data = MString.StringToArray("DFSD,w123T");

            foreach (var item in data)
            {
                _msg.WriteLine(item);
            }
            Assert.Equal(data,data);
        }

       

        [Fact]
        public void StrLength()
        {
            var data = MString.StrLength("托尔斯泰");
            _msg.WriteLine(data.ToString());
            Assert.Equal(data,data);
        }




    }
}