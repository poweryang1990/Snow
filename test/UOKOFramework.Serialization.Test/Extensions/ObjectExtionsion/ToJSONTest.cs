﻿using System;
using UOKOFramework.Serialization.Extensions;
using Xunit;

namespace UOKOFramework.Serialization.Test.Extensions.ObjectExtionsion
{
    // ReSharper disable once InconsistentNaming
    public class ToJSONTest
    {
        [Fact]
        public void when_object_is_null_should_return_null()
        {
            object value = null;

            Assert.Equal(null, value.ToJSON());
        }

        [Fact]
        public void when_object_is_valid()
        {
            object value = new { number = 123, string_property = "456" };
            var json = "{\"number\":123,\"string_property\":\"456\"}";

            Assert.Equal(json, value.ToJSON());
        }

        [Fact]
        public void when_object_is_valid_and_set_dateTimeFormat()
        {
            object value = new {
                number = 123,
                created_at = new DateTime(2017,9,20,11,12,13) 
            };

            var json = "{\"number\":123,\"created_at\":\"2017-09/20 11=12-13\"}";

            Assert.Equal(json, value.ToJSON("yyyy-MM/dd HH=mm-ss"));
        }
    }
}
