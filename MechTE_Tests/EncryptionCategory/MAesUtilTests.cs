using MechTE_480.EncryptionCategory;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.EncryptionCategory
{
    public class MAesUtilTests
    {
        private readonly ITestOutputHelper _msg;

        public MAesUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        
        [Fact]
        public void EncryptSecret16Test()
        {
            var input = "abbfly";
            var sk = "1234567890123456";
            var en = MAesUtil.Encrypt(input, sk);
            var de = MAesUtil.Decrypt(en, sk);
            _msg.WriteLine(en);
            Assert.True(de == input);
        }

        [Fact]
        public void EncryptSecret24Test()
        {
            var input = "abbfly";
            var sk = "123456789012345678901234";
            var en = MAesUtil.Encrypt(input, sk);
            var de = MAesUtil.Decrypt(en, sk);
            _msg.WriteLine(en);
            Assert.True(de == input);
        }

        [Fact]
        public void EncryptSecret32Test()
        {
            var input = "abbfly";
            var sk = "12345678901234567890123456789012";
            var en = MAesUtil.Encrypt(input, sk);
            var de = MAesUtil.Decrypt(en, sk);
            _msg.WriteLine(en);
            Assert.True(de == input);
        }
    }
}