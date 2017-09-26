using System.Net.Http;
using System.Threading.Tasks;
using UOKOFramework.Serialization.Extensions;

namespace UOKOFramework.Http.Extensions
{
    /// <summary>
    /// HttpClient的扩展方法
    /// </summary>
    public static class HttpClientExtension
    {
        /// <summary>
        /// 请求JSON
        /// </summary>
        /// <typeparam name="TResponse">JSON反序列化后的类型</typeparam>
        /// <param name="httpClient">this</param>
        /// <param name="httpRequestMessage">HttpRequestMessage</param>
        /// <returns>反序列化后的对象</returns>
        public static async Task<TResponse> RequestJsonAsync<TResponse>(
            this HttpClient httpClient,
            HttpRequestMessage httpRequestMessage)
        {
            Throws.ArgumentNullException(httpClient, nameof(httpClient));
            Throws.ArgumentNullException(httpRequestMessage, nameof(httpRequestMessage));

            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var httpResponseString = await httpResponseMessage.Content.ReadAsStringAsync();

            return httpResponseString.JsonToObject<TResponse>();
        }
    }
}