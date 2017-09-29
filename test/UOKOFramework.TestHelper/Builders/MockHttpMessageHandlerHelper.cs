using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using UOKOFramework.Extensions;

namespace UOKOFramework.TestHelper.Builders
{
    public class MockHttpMessageHandlerBuilder
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

        public static MockHttpMessageHandlerBuilder New => new MockHttpMessageHandlerBuilder();

        private MockHttpMessageHandlerBuilder()
        {
        }

        public MockHttpMessageHandlerBuilder AddHttpResponseMessage(
           string requestUrl,
            HttpResponseMessage httpResponseMessage)
        {
            return this.AddHttpMessage(_ => MatchRequestUrl(_, requestUrl), httpResponseMessage);
        }

        private static bool MatchRequestUrl(HttpRequestMessage httpRequestMessage, string requestUrl)
        {
            return httpRequestMessage.RequestUri.ToString().Include(requestUrl);
        }

        public MockHttpMessageHandlerBuilder AddHttpMessage(
            Expression<Func<HttpRequestMessage, bool>> match,
            HttpResponseMessage httpResponseMessage)
        {
            this._mockHttpMessageHandler.Protected()
               .Setup<Task<HttpResponseMessage>>("SendAsync", true, ItExpr.Is(match), ItExpr.IsAny<CancellationToken>())
               .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => httpResponseMessage));

            return this;
        }

        public Mock<HttpMessageHandler> Build()
        {
            return this._mockHttpMessageHandler;
        }

        public HttpClient BuildHttpClient()
        {
            return new HttpClient(this._mockHttpMessageHandler.Object);
        }
    }
}
