﻿using MechTE_480.util;
using System.Web;
using Xunit;
using Xunit.Abstractions;

namespace xUnit_Test.util
{
    public class MUtilTest
    {
        private readonly ITestOutputHelper _msg;
        public MUtilTest(ITestOutputHelper msg)
        {
            _msg = msg;
        }
  

        [Fact]
        [System.Obsolete]
        public void SetMD5()
        {
            var data = MUtil.SetMD5("DFSD,w123T");
            _msg.WriteLine(data);
            Assert.Equal(data,data);
        }

        [Fact]
        public void IsEmail()
        {
            var data = MUtil.IsEmail("DFSD,w123T");
            _msg.WriteLine(data.ToString()); 
            var data2 = MUtil.IsEmail("851039538@qq.com");
            _msg.WriteLine(data2.ToString());
            Assert.Equal(data,data);
        } 
        
        [Fact]
        public void IsInt()
        {
            var data = MUtil.IsInt("DFSD,w123T");
            _msg.WriteLine(data.ToString()); 
            var data2 = MUtil.IsInt("123214");
            _msg.WriteLine(data2.ToString());
            Assert.Equal(data,data);
        } 
        
        [Fact]
        public void IsNumber()
        {
            var data = MUtil.IsNumber("312123T");
            _msg.WriteLine(data.ToString()); 
            var data2 = MUtil.IsNumber("123214");
            _msg.WriteLine(data2.ToString());
            Assert.Equal(data,data);
        }
          
        
       

    }
}