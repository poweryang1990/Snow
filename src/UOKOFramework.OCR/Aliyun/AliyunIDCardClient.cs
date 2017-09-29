using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UOKOFramework.Http.Extensions;
using UOKOFramework;
using UOKOFramework.Extensions;
using UOKOFramework.Serialization.Extensions;

// ReSharper disable InconsistentNaming
// https://help.aliyun.com/document_detail/30407.html?spm=5176.doc30403.2.20.atWaQP
namespace UOKOFramework.OCR.Aliyun
{

    /// <summary>
    /// 阿里云OCR身份证识别
    /// </summary>
    public class AliyunIDCardClient : IIDCardClient
    {
        private readonly AliyunOCROptions _options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name='options'></param>
        public AliyunIDCardClient(AliyunOCROptions options)
        {
            Throws.ArgumentNullException(options, nameof(options));
            Throws.ArgumentNullException(options.Url, nameof(options.Url));
            Throws.ArgumentNullException(options.Appcode, nameof(options.Appcode));
            Throws.ArgumentNullException(options.HttpClient, nameof(options.HttpClient));

            this._options = options;
        }

        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name='request'></param>
        /// <returns></returns>
        public async Task<IDCardResponse> DetectAsync(IDCardRequest request)
        {
            Throws.ArgumentNullException(request, nameof(request));
            Throws.ArgumentNullException(request.ImgUrl, nameof(request.ImgUrl));

            try
            {
                var httpClient = this._options.HttpClient;

                var httpRequest = await BuildHttpRequestMessage(request);

                var response = await httpClient.RequestJsonAsync<AlibabaOCRResponse>(httpRequest);

                var result = response
                    .Outputs
                    .FirstOrDefault()
                    .OutputValue
                    .DataValue;

                return new IDCardResponse
                {
                    Success = result.Success,
                    Result = new IDCard
                    {
                        Address = result.Address,
                        Authority = result.Issue,
                        Birth = result.Birth,
                        Name = result.Name,
                        Nation = result.Nationality,
                        Sex = result.Sex,
                        ValidDate = result.Start_date + "-" + result.End_date,
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<HttpRequestMessage> BuildHttpRequestMessage(IDCardRequest request)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, this._options.Url);
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("APPCODE", this._options.Appcode);

            var httpClient = this._options.HttpClient;
            var imageBytes = await httpClient.GetByteArrayAsync(request.ImgUrl);
            var imageBase64 = imageBytes.GetBase64();

            var requestJson = new
            {
                inputs = new List<object>
                {
                    new
                    {
                        image = new
                        {
                            dataType = 50,
                            dataValue = imageBase64
                        },
                        configure = new
                        {
                            dataType = 50,
                            dataValue = new
                            {
                                side = request.Type.ToString()
                            }
                        }
                    }
                }
            }.ToJson();

            httpRequest.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            return httpRequest;
        }
    }
}