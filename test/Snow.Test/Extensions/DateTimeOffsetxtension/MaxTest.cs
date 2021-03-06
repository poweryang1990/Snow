﻿using System;
using Snow.Extensions;
using Xunit;

namespace Snow.Test.Extensions.DateTimeExtension
{
    public class MaxTest
    {
        [Fact]
        public void Max()
        {
            var dateTimeMin = DateTimeOffset.Parse("2017-1-1");
            var dateTimeMax = DateTimeOffset.Parse("2017-1-2");

            Assert.Equal(dateTimeMin, dateTimeMin.Max(dateTimeMin));
            Assert.Equal(dateTimeMax, dateTimeMin.Max(dateTimeMax));
            Assert.Equal(dateTimeMax, dateTimeMax.Max(dateTimeMin));
        }
    }
}
