using System.Collections.Generic;
using Xunit;
using Snow.Extensions;

namespace Snow.Test.Extensions.DictionaryExtension
{
    public class GetValueOrDefaultTest
    {
        [Fact]
        public void when_dictionary_is_null_should_return_default_value()
        {
            var dictionary = (IDictionary<string, string>)null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var value = dictionary.GetValueOrDefault("key");

            Assert.Null(value);
        }

        [Fact]
        public void when_key_not_exist_should_return_default_value()
        {
            var dictionary = new Dictionary<string, string>();

            var value = dictionary.GetValueOrDefault("key");

            Assert.Null(value);
        }

        [Fact]
        public void when_key_not_exist_and_specify_default_value_should_return_specify_default_value()
        {
            var dictionary = new Dictionary<string, string>();

            var value = dictionary.GetValueOrDefault("key","UOKO");

            Assert.Equal("UOKO", value);
        }

        [Fact]
        public void when_key_exist_should_return_value()
        {
            var dictionary = new Dictionary<string, string>
            {
                ["name"] = "UOKO"
            };

            var value = dictionary.GetValueOrDefault("name");

            Assert.Equal("UOKO", value);
        }
    }
}
