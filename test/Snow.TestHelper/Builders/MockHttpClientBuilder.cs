using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Snow.Extensions;

namespace Snow.TestHelper.Builders
{
    public class MockHttpClientBuilder
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        public static MockHttpClientBuilder New => new MockHttpClientBuilder();

        private MockHttpClientBuilder()
        {
        }

        public MockHttpClientBuilder AddHttpMessage(
           string requestUrl,
            HttpResponseMessage httpResponseMessage)
        {
            return this.AddHttpMessage(_ => MatchRequestUrl(_, requestUrl), httpResponseMessage);
        }

        private static bool MatchRequestUrl(HttpRequestMessage httpRequestMessage, string requestUrl)
        {
            return httpRequestMessage.RequestUri.ToString().Include(requestUrl);
        }

        public MockHttpClientBuilder AddHttpMessage(
            Expression<Func<HttpRequestMessage, bool>> match,
            HttpResponseMessage httpResponseMessage)
        {
            this._mockHttpMessageHandler.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", true, ItExpr.Is(match), ItExpr.IsAny<CancellationToken>())
               .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => httpResponseMessage));

            return this;
        }

        public Mock<HttpMessageHandler> BuildMock()
        {
            return this._mockHttpMessageHandler;
        }

        public HttpClient Build()
        {
            return new HttpClient(this._mockHttpMessageHandler.Object);
        }
    }
}
