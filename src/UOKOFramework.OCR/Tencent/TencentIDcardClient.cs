using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UOKOFramework.Http.Extensions;
using UOKOFramework.Extensions;
using UOKOFramework.Serialization.Extensions;

// ReSharper disable InconsistentNaming
// https://cloud.tencent.com/document/product/460/6895
namespace UOKOFramework.OCR.Tencent
{
    /// <summary>
    /// 腾讯OCR身份证 实现
    /// </summary>
    public class TencentIDCardClient : IIDCardClient
    {
        private readonly TencentOCROptions _options;
        private readonly IClock _clock;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TencentIDCardClient(TencentOCROptions options, IClock clock)
        {
            Throws.ArgumentNullException(options, nameof(options));
            Throws.ArgumentNullException(options.Apiurl, nameof(options.Apiurl));
            Throws.ArgumentNullException(options.AppId, nameof(options.AppId));
            Throws.ArgumentNullException(options.SecretId, nameof(options.SecretId));
            Throws.ArgumentNullException(options.SecretKey, nameof(options.SecretKey));
            Throws.ArgumentNullException(options.Bucket, nameof(options.Bucket));
            Throws.ArgumentNullException(options.HttpClient, nameof(options.HttpClient));

            _options = options;
            _clock = clock;
        }

        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IDCardResponse> DetectAsync(IDCardRequest request)
        {
            Throws.ArgumentNullException(request, nameof(request));
            Throws.ArgumentNullException(request.ImgUrl, nameof(request.ImgUrl));
            try
            {
                var httpClient = this._options.HttpClient;

                var httpRequest = BuildHttpRequestMessage(request);

                var response = await httpClient.RequestJsonAsync<TencentOCRResponse>(httpRequest);

                var result = response.Result_List.First();

                return new IDCardResponse
                {
                    Success = result.Code == 0,
                    Message = result.Message,
                    Result = new IDCard
                    {
                        Address = result.Data.Address,
                        Authority = result.Data.Authority,
                        Birth = result.Data.Birth,
                        Id = result.Data.Id,
                        Name = result.Data.Name,
                        Nation = result.Data.Nation,
                        Sex = result.Data.Sex,
                        ValidDate = result.Data.Valid_date
                    }
                };
            }
            catch (Exception ex)
            {
                return new IDCardResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        private HttpRequestMessage BuildHttpRequestMessage(IDCardRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _options.Apiurl);

            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", BuildSignature());

            var requestJson = new
            {
                appid = this._options.AppId,
                bucket = this._options.Bucket,
                cart_type = (int)request.Type,
                url_list = new List<string>
                {
                    request.ImgUrl
                }
            }.ToJson();

            httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            return httpRequest;
        }

        private string BuildSignature()
        {
            var now = _clock.UnixTime;
            var option = this._options;
            // ReSharper disable once UseStringInterpolation
            var plainText = string.Format("a={0}&b={1}&k={2}&t={3}&e={4}",
                option.AppId,
                option.Bucket,
                option.SecretId,
                now, 
                now + 60);

            var plainBytes = plainText.GetBytes();

            var macBytes = plainBytes.GetHMACSHA1(option.SecretKey.GetBytes());

            return macBytes.Combine(plainBytes).GetBase64();
        }
    }
}