﻿using Xunit;

namespace Snow.Test.Cryptography.HashProviders
{
    // ReSharper disable once InconsistentNaming
    public class GetSHA256Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var hashHelper = BuildHashProvider("优客");

            var sha256Bytes = hashHelper.GetSHA256();

            var sha256Hex = GetHex(sha256Bytes);
            Assert.Equal("44E77E370BD3FAFA99DD21E86BD7D7E9407D146F12EE3DD36AFF248B9E012482", sha256Hex);
        }
    }
}