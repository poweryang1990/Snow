using Newtonsoft.Json.Linq;
using Snow.Extensions;
using Xunit;

namespace Snow.Serialization.Test.Extensions.JObjectExtension
{
    // ReSharper disable once InconsistentNaming
    public class GetPropertyTest
    {
        [Fact]
        public void when_jobject_is_null_should_return_null()
        {
            JObject jObject = null;

            Assert.Null(jObject.GetProperty("a"));
        }

        [Fact]
        public void when_property_name_is_valid_should_return_null()
        {
            var jObject = JObject.Parse("{}");

            Assert.Null(jObject.GetProperty(null));
            Assert.Null(jObject.GetProperty(""));
            Assert.Null(jObject.GetProperty(" "));
            Assert.Null(jObject.GetProperty("\r\t"));
        }

        [Fact]
        public void when_property_name_is_not_exist_should_return_null()
        {
            var jObject = JObject.Parse("{}");

            Assert.Null(jObject.GetProperty("a"));
        }

        [Fact]
        public void when_jobject_and_property_name_is_valid()
        {
            var jObject = JObject.Parse("{'a':123}");
            var propertyA = JToken.Parse("123");

            Assert.Equal(propertyA, jObject.GetProperty("a"));
            Assert.Equal(propertyA, jObject.GetProperty("A"));
        }
    }
}
