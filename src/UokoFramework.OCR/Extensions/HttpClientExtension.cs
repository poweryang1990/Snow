using System.Net.Http;
using System.Threading.Tasks;
using UOKOFramework.Serialization.Extensions;

namespace UokoFramework.OCR.Extensions
{
    /// <summary>
    ///
    /// </summary>
    internal static class HttpClientExtension
    {
        public static async Task<TResponse> Request<TResponse>(
            this HttpClient httpClient,
            HttpRequestMessage httpRequestMessage)
        {
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            return responseString.JsonToObject<TResponse>();
        }
    }
}