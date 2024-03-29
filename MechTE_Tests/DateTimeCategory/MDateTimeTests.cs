﻿using MechTE_480.DateTimeCategory;
using Xunit;
using Xunit.Abstractions;

namespace MechTE_Tests.DateTimeCategory
{
    public class MDateTimeTests
    {
        private readonly ITestOutputHelper _msg;

        public MDateTimeTests(ITestOutputHelper msg)
        {
            _msg = msg;
        }

        [Fact]
        public void GetTime()
        {
            var data = MDateTimeUtil.GetTime();
            _msg.WriteLine(data);
            Assert.Equal(data, data);
        }
        
        [Fact]
        public void GetYesterdayTime()
        {
            var data = MDateTimeUtil.GetYesterdayTime();
            _msg.WriteLine(data);
            Assert.Equal(data, data);
        }
        
        [Fact]
        public void GetDayOfWeek()
        {
            var data = MDateTimeUtil.GetDayOfWeek();
            _msg.WriteLine(data.ToString());
        }
    }
}