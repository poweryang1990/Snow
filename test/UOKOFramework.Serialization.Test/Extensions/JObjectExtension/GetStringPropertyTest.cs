using Newtonsoft.Json.Linq;
using Xunit;
using UOKOFramework.Serialization.Extensions;

namespace UOKOFramework.Serialization.Test.Extensions.JObjectExtension
{
    // ReSharper disable once InconsistentNaming
    public class GetStringPropertyTest
    {
        [Fact]
        public void when_jobject_is_null_should_return_string_empty()
        {
            JObject jObject = null;

            Assert.Equal(string.Empty, jObject.GetStringProperty("a"));
        }

        [Fact]
        public void when_property_name_is_not_exist_should_return_string_empty()
        {
            var jObject = JObject.Parse("{}");

            Assert.Equal(string.Empty, jObject.GetStringProperty("a"));
        }

        [Fact]
        public void when_jobject_and_property_name_is_valid()
        {
            var jObject = JObject.Parse("{a:123}");

            Assert.Equal("123", jObject.GetStringProperty("a"));
            Assert.Equal("123", jObject.GetStringProperty("A"));
        }
    }
}
