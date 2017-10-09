using Snow.OCR.Tencent;
using Snow;
using Xunit;
using System.Net.Http;
using System;
using Snow.OCR.Aliyun;
using Snow.TestHelper;
using Snow.TestHelper.Builders;
using Snow.Serialization.Extensions;

// ReSharper disable InconsistentNaming

namespace Snow.OCR.Test.Tencent.TencentIDCardClients
{
    public class TencentIDCardTest
    {

        private readonly IDCardRequest idCardRequest = new IDCardRequest()
        {
            Type = IDCardType.Face,
            ImgUrl = "http://7xodcr.com1.z0.glb.clouddn.com/%E6%AD%A3%E9%9D%A2.png",
        };

        private TencentOCROptions _tencentOptions = new TencentOCROptions
        {
            Apiurl = "http://service.image.myqcloud.com/ocr/idcard",
            AppId = "1111",
            SecretId = "dsdasdsadsa",
            SecretKey = "weqwewqe",
            Bucket = "idcard",
            HttpClient = new HttpClient(),
        };

        [Fact]
        public void when_TencentOCROptions_isNull_DetectTencent()
        {
            Assert.Throws<ArgumentNullException>(() => new TencentIDCardClient(null, new Clock()));
        }

        [Fact]
        public void when_TencentOCROptions_Apiurl_isNull_DetectTencent()
        {
            _tencentOptions.Apiurl = null;

            Assert.Throws<ArgumentNullException>(() => new TencentIDCardClient(_tencentOptions, new Clock()));
        }

        [Fact]
        public void when_TencentOCROptions_AppId_isNull_DetectTencent()
        {
            _tencentOptions.AppId = null;

            Assert.Throws<ArgumentNullException>(() => new TencentIDCardClient(_tencentOptions, new Clock()));
        }

        [Fact]
        public void when_TencentOCROptions_SecretId_isNull_DetectTencent()
        {
            _tencentOptions.SecretId = null;

            Assert.Throws<ArgumentNullException>(() => new TencentIDCardClient(_tencentOptions, new Clock()));
        }

        [Fact]
        public void when_TencentOCROptions_SecretKey_isNull_DetectTencent()
        {
            _tencentOptions.SecretKey = null;

            Assert.Throws<ArgumentNullException>(() => new TencentIDCardClient(_tencentOptions, new Clock()));
        }

        [Fact]
        public void when_TencentOCROptions_Bucket_isNull_DetectTencent()
        {
            _tencentOptions.Bucket = null;

            Assert.Throws<ArgumentNullException>(() => new TencentIDCardClient(_tencentOptions, new Clock()));
        }

        [Fact]
        public void when_TencentOCROptions_HttpClient_isNull_DetectTencent()
        {
            _tencentOptions.HttpClient = null;

            Assert.Throws<ArgumentNullException>(() => new TencentIDCardClient(_tencentOptions, new Clock()));
        }

        [Fact]
        public async void when_IDCardRequest_isNull_DetectTencent()
        {
            IIDCardClient tencentIDcard = new TencentIDCardClient(_tencentOptions, new Clock());

            await Assert.ThrowsAsync<ArgumentNullException>(() => tencentIDcard.DetectAsync(null));
        }

        [Fact]
        public async void when_IDCardRequest_ImgUrl_isNull_DetectTencent()
        {
            IIDCardClient tencentIDcard = new TencentIDCardClient(_tencentOptions, new Clock());

            await Assert.ThrowsAsync<ArgumentNullException>(() => tencentIDcard.DetectAsync(new IDCardRequest()
            {
                ImgUrl = null,
                Type = IDCardType.Face,
            }));
        }


        [Fact]
        public async void when_HttpResponse_is_OK_Face()
        {
            var json = this.GetResourceText("Tencent_ocr_face_response.json");
            
            var httpClient = MockHttpClientBuilder.New
                .AddJsonResponse(request => VerifyHttpRequestMessage(request), json)
                .Build();

            _tencentOptions.HttpClient = httpClient;

            IIDCardClient tencentIDcard = new TencentIDCardClient(_tencentOptions, new Clock());

            var response = await tencentIDcard.DetectAsync(idCardRequest);

            Assert.True(response.Success, response.Message);
            var idCard = response.Result;
            Assert.Equal("李四", idCard.Name);
            Assert.Equal("汉", idCard.Nation);
            Assert.Equal("2000/1/1", idCard.Birth);
            Assert.Equal("成都", idCard.Address);
            Assert.Equal("123456200001011234", idCard.Id);
            Assert.Equal(Gender.Male, idCard.Gender);
            Assert.Equal(null, idCard.Authority);
            Assert.Equal(null, idCard.StartDate);
            Assert.Equal(null, idCard.EndDate);
        }

        [Fact]
        public async void when_HttpResponse_is_OK_Back()
        {
            var json = this.GetResourceText("Tencent_ocr_back_response.json");

            var httpClient = MockHttpClientBuilder.New
                .AddJsonResponse(request => VerifyHttpRequestMessage(request), json)
                .Build();

            _tencentOptions.HttpClient = httpClient;

            IIDCardClient tencentIDcard = new TencentIDCardClient(_tencentOptions, new Clock());

            var response = await tencentIDcard.DetectAsync(idCardRequest);

            Assert.True(response.Success, response.Message);
            var idCard = response.Result;
            Assert.Equal(null, idCard.Name);
            Assert.Equal(null, idCard.Nation);
            Assert.Equal(null, idCard.Birth);
            Assert.Equal(null, idCard.Address);
            Assert.Equal(null, idCard.Id);
            Assert.Equal(null, idCard.Gender);

            Assert.Equal("某市派出所", idCard.Authority);
            Assert.Equal("2012.01.01", idCard.StartDate);
            Assert.Equal("2022.01.01", idCard.EndDate);
        }

        private bool VerifyHttpRequestMessage(HttpRequestMessage request)
        {
            if (request.RequestUri.OriginalString != "http://service.image.myqcloud.com/ocr/idcard")
            {
                return false;
            }

            var requestBody = request.Content.ReadAsStringAsync().Result;
            var json = requestBody.JsonToObject<dynamic>();

            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("1111", json.appid.Value);
            Assert.Equal("idcard", json.bucket.Value);
            return true;
        }
    }
}
