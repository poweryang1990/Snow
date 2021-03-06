﻿using Xunit;

namespace Snow.Test.Cryptography.HashProviders
{
    // ReSharper disable once InconsistentNaming
    public class GetSHA1Test : BaseTest
    {
        [Fact]
        public void when_bytes_is_not_null()
        {
            var hashHelper = BuildHashProvider("优客");

            var sha1Bytes = hashHelper.GetSHA1();

            var sha1Hex = GetHex(sha1Bytes);
            Assert.Equal("E18A69ABFAB46709CA24105B92A1E425BDD75348", sha1Hex);
        }
    }
}