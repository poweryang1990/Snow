using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;
using UOKOFramework.Http.Extensions;

namespace UOKOFramework.Http.Test.Extensions.HttpClientExtension
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
            //mock
            var httpResponseMessage = BuildJsonHttpResponseMessage("{\"name\":\"chunqiu\"}");
            var mockHttpMessageHandler = MockHttpMessageHandler(httpResponseMessage);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://a.b.c");

            var response = await httpClient.RequestJsonAsync<MockResponse>(httpRequestMessage);

            Assert.Equal("chunqiu", response.Name);
        }

        private HttpResponseMessage BuildJsonHttpResponseMessage(string json)
        {
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
        }

        private Mock<HttpMessageHandler> MockHttpMessageHandler(HttpResponseMessage httpResponseMessage)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => httpResponseMessage));

            return mockHttpMessageHandler;
        }
    }
}
