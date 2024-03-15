using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.Util
{
    public class MStringUtilTest
    {
        private readonly ITestOutputHelper _msg;
        public MStringUtilTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        public void StringToListStr()
        {
            var data = MStringUtil.StringToListStr("DFSD,w123T",',',true);

            foreach (var item in data)
            {
                _msg.WriteLine(item);
            }
            Assert.Equal(data,data);
        }

        [Fact]
        public void StringToArray()
        {
            var data = MStringUtil.StringToArray("DFSD,w123T");

            foreach (var item in data)
            {
                _msg.WriteLine(item);
            }
            Assert.Equal(data,data);
        }

       

        [Fact]
        public void StrLength()
        {
            var data = MStringUtil.StrLength("托尔斯泰");
            _msg.WriteLine(data.ToString());
            Assert.Equal(data,data);
        }




    }
}