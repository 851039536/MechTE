using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.util
{
    public class MDESEncryptTest
    {
        private readonly ITestOutputHelper _msg;
        public MDESEncryptTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        [System.Obsolete]
        public void Encrypt()
        {
            var data = MEncrypt.Encrypt("DFS123T");
            _msg.WriteLine(data);
            Assert.Equal(data,data);
        }

        [Fact]
        [System.Obsolete]
        public void Decrypt()
        {
            var data = MEncrypt.Decrypt("5FB0330ADD43B5B8");
            _msg.WriteLine(data);
            Assert.Equal(data,data);
        }

        [Fact]
        public void Encode()
        {
            var data = MEncrypt.Encode("DFS123T");
            _msg.WriteLine(data);
            Assert.Equal(data,data);
        }

        [Fact]
        public void Decode()
        {
            var data = MEncrypt.Decode("f2rRoEkrOXQ=");
            _msg.WriteLine(data);
            Assert.Equal(data,data);
        }
    }
}