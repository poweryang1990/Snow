﻿using System;
using Snow.Text;
using Xunit;

namespace Snow.Test.Text.ByteHelpers
{
    public class GetBase64Test : BaseTest
    {

        [Fact]
        public void when_bytes_is_null_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();
            byte[] bytes = null;

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBase64(bytes));
        }

        [Fact]
        public void when_bytes_is_empty_should_throw_ArgumentNullException()
        {
            var byteHelper = ByteHelper.New();
            byte[] bytes = new byte[0];

            Assert.Throws<ArgumentNullException>(() => byteHelper.GetBase64(bytes));
        }

        [Fact]
        public void when_bytes_is_not_null()
        {
            var byteHelper = ByteHelper.New();
            var bytes = byteHelper.GetBytes("优客");

            var base64 = byteHelper.GetBase64(bytes);

            Assert.Equal("5LyY5a6i", base64);
        }
    }
}