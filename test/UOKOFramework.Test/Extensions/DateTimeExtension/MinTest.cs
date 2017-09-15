﻿using System;
using UOKOFramework.Extensions;
using Xunit;

namespace UOKOFramework.Test.Extensions.DateTimeExtension
{
    public class MinTest
    {
        [Fact]
        public void Min()
        {
            var dateTimeMin = DateTime.Parse("2017-1-1");
            var dateTimeMax = DateTime.Parse("2017-1-2");

            Assert.Equal(dateTimeMin, dateTimeMin.Min(dateTimeMin));
            Assert.Equal(dateTimeMin, dateTimeMin.Min(dateTimeMax));
            Assert.Equal(dateTimeMin, dateTimeMax.Min(dateTimeMin));
        }
    }
}
