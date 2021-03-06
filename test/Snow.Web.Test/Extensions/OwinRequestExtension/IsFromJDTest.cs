﻿using Microsoft.Owin;
using Snow.Extensions;
using Xunit;

// ReSharper disable InconsistentNaming

namespace Snow.Web.Test.Extensions.OwinRequestExtension
{
    public class IsFromJDTest : BaseTest
    {
        [Fact]
        public void when_this_is_null_shoule_return_false()
        {
            IOwinRequest owinRequest = null;

            Assert.False(owinRequest.IsFromJD());
        }

        [Theory]
        [InlineData("JD", true)]
        [InlineData("jd", true)]
        [InlineData("abc", false)]
        [InlineData(null, false)]
        public void when_this_is_valid(string userAgent, bool result)
        {
            var owinRequest = BuildIOwinRequest("User-Agent", userAgent);
            Assert.Equal(result, owinRequest.IsFromJD());
        }
    }
}
