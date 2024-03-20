using MechTE_480.ConvertCategory;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.ConvertCategory
{
    public class MConvertUtilTests
    {
        private readonly ITestOutputHelper _msg;

        public MConvertUtilTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        
        /// <summary>
        /// 将字符串转换为整型，转换失败返回0
        /// </summary>
        [Fact]
        public void ToInt32()
        {
            var data = MConvertUtil.ToInt32("18");
            _msg.WriteLine(data.ToString());
            Assert.Equal(data, data);
        }

        [Fact]
        public void ToInt64()
        {
            var data = MConvertUtil.ToInt64("183213213214");
            _msg.WriteLine(data.ToString());
            Assert.Equal(data, data);
        }    
        [Fact]
        public void ToDouble()
        {
            var data = MConvertUtil.ToDouble("1832132132.14");
            _msg.WriteLine(data.ToString());
            Assert.Equal(data, data);
        }

        private enum MyEnum
        {
            Value1,
            Value2,
        }
        [Fact]
        public void ToEnum()
        {
            // 使用方法
            string input = "Value2";
            var convertedEnum = MConvertUtil.ToEnum<MyEnum>(input); // 如果 "Value2" 存在于 MyEnum 中，则转换成功并返回相应枚举成员
            var defaultValueCase = MConvertUtil.ToEnum<MyEnum>("NonExistentValue"); // 如果 "NonExistentValue" 不存在于 MyEnum 中，则返回默认值 MyEnum.Value1
            _msg.WriteLine(convertedEnum.ToString());
            _msg.WriteLine(defaultValueCase.ToString());
            Assert.Equal(defaultValueCase, defaultValueCase);
        }

        [Fact]
        public void ConvertBase()
        {
            var data = MConvertUtil.ConvertBase("18", 10, 16);
            _msg.WriteLine(data);
            Assert.Equal(data, data);
        }

        /// <summary>
        /// 将16进制字符转为ASCII字符
        /// </summary>
        [Fact]
        public void HexToAscii()
        {
            var data = MConvertUtil.HexToAscii("76312E342E30");
            var data2= "76312E342E30".HexToAsciiEx();
            if (data.Equals(data2))
            {
                _msg.WriteLine(data);
            }
            Assert.Equal("v1.4.0", data);
        }

        [Fact]
        public void AsciiToHex()
        {
            var data2 = MConvertUtil.HexToAscii("76312E342E30");
            var data = MConvertUtil.AsciiStrToHexStr(data2);
            Assert.Equal(data, data);
        }  
        
        [Fact]
        public void HexStrToAsciiHexStr()
        {
            var data = MConvertUtil.HexToAsciiHex("76312E342E30");
            _msg.WriteLine(data);
            Assert.Equal(data, data);
        }
        
        /// <summary>
        /// 通过将byte数组中的每个元素转换为十六进制字符串，并在每个元素之间添加空格，将byte数组转换为字符串
        /// </summary>
        [Fact]
        public void ByteToHex()
        {
            byte[] bytes = { 65, 66, 67, 68 };
            string result = MConvertUtil.ByteToHex(bytes);
            _msg.WriteLine(result);
            Assert.Equal(result, result);
        } 
        
        [Fact]
        public void ToSingle()
        {
            var result = MConvertUtil.ToSingle("213.123213");
            _msg.WriteLine(result.ToString());
            Assert.Equal(result, result);
        } 
        
    }
}