using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
// ReSharper disable CheckNamespace

namespace Snow.TestHelper.Builders
{
    public static class MockHttpClientBuilderExtension
    {
        public static MockHttpClientBuilder AddStatusCodeResponse(
            this MockHttpClientBuilder @this,
            string requestUrl,
            HttpStatusCode httpStatusCode)
        {
            var httpStatusCodeResponse = HttpResponseMessageBuilder.New
                .WithHttpStatusCode(httpStatusCode)
                .Build();

            return @this.AddHttpMessage(requestUrl, httpStatusCodeResponse);
        }


        public static MockHttpClientBuilder AddJsonResponse(
            this MockHttpClientBuilder @this,
            string requestUrl,
            string json)
        {
            return @this.AddHttpMessage(requestUrl, BuildJsonResponseMessage(json));
        }

        public static MockHttpClientBuilder AddBytesResponse(
            this MockHttpClientBuilder @this,
            string requestUrl,
            byte[] bytes)
        {
            return @this.AddHttpMessage(requestUrl, BuildBytesResponseMessage(bytes));
        }

        public static MockHttpClientBuilder AddJsonResponse(
            this MockHttpClientBuilder @this,
            Expression<Func<HttpRequestMessage, bool>> match,
            string json)
        {
            return @this.AddHttpMessage(match, BuildJsonResponseMessage(json));
        }

        public static MockHttpClientBuilder AddBytesResponse(
            this MockHttpClientBuilder @this,
            Expression<Func<HttpRequestMessage, bool>> match,
            byte[] bytes)
        {
            return @this.AddHttpMessage(match, BuildBytesResponseMessage(bytes));
        }

        private static HttpResponseMessage BuildJsonResponseMessage(string json)
        {
            return HttpResponseMessageBuilder.New
                .WithJsonContent(json)
                .Build();
        }

        private static HttpResponseMessage BuildBytesResponseMessage(byte[] bytes)
        {
            return HttpResponseMessageBuilder.New
                .WithBytesContent(bytes)
                .Build();
        }
    }
}
