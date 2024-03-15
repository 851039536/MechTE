using MechTE_480.EncryptionCategory;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.EncryptionCategory
{
    public class MDecUtilTests
    {
        private readonly ITestOutputHelper _msg;

        public MDecUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        public void Encode()
        {
            var data = MDecUtil.Encode("DFS123T");
            _msg.WriteLine(data);
            Assert.Equal(data, data);
        }

        [Fact]
        public void Decode()
        {
            var v = MDecUtil.Encode("DFS123T");
            var data = MDecUtil.Decode(v);
            _msg.WriteLine(data);
            Assert.Equal(data, data);
        }
    }
}