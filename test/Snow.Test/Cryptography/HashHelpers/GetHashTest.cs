using System;
using System.Security.Cryptography;
using Xunit;

namespace Snow.Test.Cryptography.HashHelpers
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ExpressionIsAlwaysNull
    public class GetHashTest : BaseTest
    {
        [Fact]
        public void when_HashAlgorithm_is_null_should_throw_ArgumentNullException()
        {
            var hashHelper = BuildHashHelper("优客");
            HashAlgorithm hashAlgorithm = null;

            Assert.Throws<ArgumentNullException>(() => hashHelper.GetHash(hashAlgorithm));
        }

        [Fact]
        public void when_HashAlgorithm_is_not_null()
        {
            var hashHelper = BuildHashHelper("优客");
            HashAlgorithm hashAlgorithm = RIPEMD160.Create();

            var hashBytes = hashHelper.GetHash(hashAlgorithm);

            var hashHex = GetHex(hashBytes);
            Assert.Equal("333AC709F22467C201C069EA258627760705C06A", hashHex);
        }
    }
}