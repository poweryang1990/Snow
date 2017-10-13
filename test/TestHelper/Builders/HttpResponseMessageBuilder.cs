using System.Net;
using System.Net.Http;
using System.Text;

namespace TestHelper.Builders
{
    public class HttpResponseMessageBuilder
    {
        private readonly HttpResponseMessage _httpResponseMessage;

        public static HttpResponseMessageBuilder New => new HttpResponseMessageBuilder()
            .WithHttpStatusCode(HttpStatusCode.OK);

        private HttpResponseMessageBuilder()
        {
            this._httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessageBuilder WithHttpStatusCode(HttpStatusCode httpStatusCode)
        {
            this._httpResponseMessage.StatusCode = httpStatusCode;
            return this;
        }

        public HttpResponseMessageBuilder WithStringContent(string content, Encoding encoding, string mediaType)
        {
            this._httpResponseMessage.Content = new StringContent(content, encoding, mediaType);
            return this;
        }

        public HttpResponseMessageBuilder WithJsonContent(string json)
        {
            return WithStringContent(json, Encoding.UTF8, "application/json");
        }

        public HttpResponseMessageBuilder WithBytesContent(byte[] bytes)
        {
            this._httpResponseMessage.Content = new ByteArrayContent(bytes);
            return this;
        }

        public HttpResponseMessage Build()
        {
            return this._httpResponseMessage;
        }
    }
}