using System.Collections.Specialized;
using System.Web;
using Moq;
using Xunit;
// ReSharper disable InconsistentNaming

namespace Snow.Web.Test.IPHelpers
{
    
    public class GetClientIPTest
    {
        [Fact]
        public void when_httprequest_is_null_should_return_null()
        {
            var ipHelper = new IPHelper();

            Assert.Null(ipHelper.GetClientIP((HttpRequest)null));
        }

        [Fact]
        public void get_ip_from_HTTP_X_FORWARDED_FOR()
        {
            var httpRequest = MockHttpRequestBase("44.34.56.78");
            httpRequest.ServerVariables.Add("HTTP_X_FORWARDED_FOR", "11.34.56.78,99.56.78.90");
            httpRequest.ServerVariables.Add("HTTP_X_REAL_IP", "22.34.56.78");
            httpRequest.ServerVariables.Add("REMOTE_ADDR", "33.34.56.78");

            var ipHelper = new IPHelper();

            Assert.Equal("11.34.56.78", ipHelper.GetClientIP(httpRequest).ToString());
        }

        [Fact]
        public void get_ip_from_HTTP_X_REAL_IP()
        {
            var httpRequest = MockHttpRequestBase("44.34.56.78");
            httpRequest.ServerVariables.Add("HTTP_X_REAL_IP", "22.34.56.78");
            httpRequest.ServerVariables.Add("REMOTE_ADDR", "33.34.56.78");
            var ipHelper = new IPHelper();

            Assert.Equal("22.34.56.78", ipHelper.GetClientIP(httpRequest).ToString());
        }

        [Fact]
        public void get_ip_from_REMOTE_ADDR()
        {
            var httpRequest = MockHttpRequestBase("44.34.56.78");
            httpRequest.ServerVariables.Add("REMOTE_ADDR", "33.34.56.78");
            var ipHelper = new IPHelper();

            Assert.Equal("33.34.56.78", ipHelper.GetClientIP(httpRequest).ToString());
        }

        [Fact]
        public void get_ip_from_UserHostAddress()
        {
            var httpRequest = MockHttpRequestBase("44.34.56.78");
            var ipHelper = new IPHelper();

            Assert.Equal("44.34.56.78", ipHelper.GetClientIP(httpRequest).ToString());
        }

        private HttpRequestBase MockHttpRequestBase(string userHostAddress)
        {
            var mockHttpRequestBase = new Mock<HttpRequestBase>(MockBehavior.Loose);

            mockHttpRequestBase.Setup(_ => _.ServerVariables).Returns(new NameValueCollection());
            mockHttpRequestBase.Setup(_ => _.UserHostAddress).Returns(userHostAddress);

            return mockHttpRequestBase.Object;
        }
    }
}
