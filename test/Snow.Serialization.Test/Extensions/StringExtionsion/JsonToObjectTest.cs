using Snow.Serialization.Extensions;
using Xunit;

namespace Snow.Serialization.Test.Extensions.StringExtionsion
{
    public class JsonToObjectTest
    {
        public class MockObject
        {
            public int number { get; set; }

            public string string_property { get; set; }
        }

        [Fact]
        public void when_json_is_null_should_return_null()
        {
            string json = null;

            Assert.Equal(null, json.JsonToObject<string>());
        }

        [Fact]
        public void when_json_is_valid()
        {
            var json = "{\"number\":123,\"string_property\":\"456\"}";
            var value = new MockObject { number = 123, string_property = "456" };

            var mockObject = json.JsonToObject<MockObject>();

            Assert.Equal(value.number, mockObject.number);
            Assert.Equal(value.string_property, mockObject.string_property);
        }
    }


}
