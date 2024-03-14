﻿using MechTE_480.ConvertCategory;
using MechTE_480.util;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.port
{
    public class MHidUtilTest
    {
        private readonly ITestOutputHelper _msg;
        public MHidUtilTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
        
        /// <summary>
        /// 过给定的索引，从字节数组中提取特定位置的字节，并将其转换为十六进制字符串
        /// </summary>
        [Fact]
        public void ByteToHex()
        {
            byte[] bytes = { 0x41, 0x42, 0x43, 0x44, 0x45 };
            string index = "0 2 4";

            string result = MConvertUtil.ByteToHex(bytes, index);
            _msg.WriteLine(result);
            Assert.Equal(result, result);
        }  
        
       
        
      
    }
}