using System;
using System.Security.Cryptography;
using Xunit;

namespace UOKOFramework.Test.Security.HashProviders
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ExpressionIsAlwaysNull
    public class GetHashTest : BaseTest
    {
        [Fact]
        public void when_HashAlgorithm_is_null_should_throw_ArgumentNullException()
        {
            var hashProvider = BuildHashProvider("优客");
            HashAlgorithm hashAlgorithm = null;

            Assert.Throws<ArgumentNullException>(() => hashProvider.GetHash(hashAlgorithm));
        }

        [Fact]
        public void when_HashAlgorithm_is_not_null()
        {
            var hashProvider = BuildHashProvider("优客");
            HashAlgorithm hashAlgorithm = RIPEMD160.Create();

            var hashBytes = hashProvider.GetHash(hashAlgorithm);

            var hashHex = GetHex(hashBytes);
            Assert.Equal("333AC709F22467C201C069EA258627760705C06A", hashHex);
        }
    }
}