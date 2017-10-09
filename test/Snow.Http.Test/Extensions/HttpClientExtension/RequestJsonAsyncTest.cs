using System;
using System.Net.Http;
using Snow.Extensions;
using Xunit;
using Snow.TestHelper.Builders;

namespace Snow.Http.Test.Extensions.HttpClientExtension
{
    // ReSharper disable ExpressionIsAlwaysNull
    public class RequestJsonAsyncTest
    {
        public class MockResponse
        {
            public string Name { get; set; }
        }
        [Fact]
        public async void when_HttpClient_is_null_should_throw_ArgumentNullException()
        {
            HttpClient httpClient = null;
            var httpRequestMessage = new HttpRequestMessage();

            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClient.RequestJsonAsync<dynamic>(httpRequestMessage));
        }

        [Fact]
        public async void when_HttpRequestMessage_is_null_should_throw_ArgumentNullException()
        {
            var httpClient = new HttpClient();

            await Assert.ThrowsAsync<ArgumentNullException>(() => httpClient.RequestJsonAsync<dynamic>(null));
        }

        [Fact]
        public async void when_HttpClient_and_HttpRequestMessage_is_valid()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://a.b.c");

            var httpClient = MockHttpClientBuilder.New
                .AddJsonResponse("a.b.c", "{\"name\":\"chunqiu\"}")
                .Build();

            var response = await httpClient.RequestJsonAsync<MockResponse>(httpRequestMessage);

            Assert.Equal("chunqiu", response.Name);
        }
    }
}
