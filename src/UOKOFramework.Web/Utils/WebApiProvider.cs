using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace UokoFramework.Web.Utils
{
    /// <summary>
    /// WebAPI访问支持
    /// </summary>
    public class WebApiProvider
    {
        private HttpClient _client;

        /// <summary>
        /// 
        /// </summary>
        public WebApiProvider()
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };
            _client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseAddress"></param>
        public WebApiProvider(string baseAddress)
        {
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            _client = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync<T>(string requestUrl, T dto)
        {
            return await _client.PostAsJsonAsync(requestUrl,dto).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync<T>(Uri requestUrl, T dto)
        {
            return await _client.PostAsJsonAsync(requestUrl, dto).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(string requestUrl)
        {
            return await _client.GetAsync(requestUrl).ConfigureAwait(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TResponse Post<TRequest, TResponse>(string requestUrl, TRequest dto)
        {
            var result = PostAsync(requestUrl, dto).Result;
            result.EnsureSuccessStatusCode();
            return result.Content.ReadAsAsync<TResponse>().Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="requestUrl"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public TResponse Post<TRequest, TResponse>(Uri requestUrl, TRequest dto)
        {
            var result = PostAsync(requestUrl, dto).Result;
            result.EnsureSuccessStatusCode();
            return result.Content.ReadAsAsync<TResponse>().Result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        public  TResponse Get<TResponse>(string requestUrl)
        {
            var result = GetAsync(requestUrl).Result;
            result.EnsureSuccessStatusCode();
            return result.Content.ReadAsAsync<TResponse>().Result;
        }

    }
}