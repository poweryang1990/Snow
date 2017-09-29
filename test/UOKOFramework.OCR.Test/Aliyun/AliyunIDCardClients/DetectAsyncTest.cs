using System;
using System.Net;
using System.Net.Http;
using UOKOFramework.Extensions;
using UOKOFramework.OCR.Aliyun;
using UOKOFramework.Serialization.Extensions;
using UOKOFramework.TestHelper;
using UOKOFramework.TestHelper.Builders;
using Xunit;

namespace UOKOFramework.OCR.Test.Aliyun.AliyunIDCardClients
{
    public class DetectAsyncTest : BaseTest
    {
        [Fact]
        public async void when_IDCardRequest_isNull()
        {
            var aliyunOptions = BuildDefaultAliyunOCROptions();
            IIDCardClient aliyunIDcard = new AliyunIDCardClient(aliyunOptions);

            await Assert.ThrowsAsync<ArgumentNullException>(() => aliyunIDcard.DetectAsync(null));
        }

        [Fact]
        public async void when_IDCardRequest_ImgUrl_isNull()
        {
            var aliyunOptions = BuildDefaultAliyunOCROptions();
            var idCardRequest = BuildDefaultIDCardRequest();
            idCardRequest.ImgUrl = null;
            IIDCardClient aliyunIDcard = new AliyunIDCardClient(aliyunOptions);

            await Assert.ThrowsAsync<ArgumentNullException>(() => aliyunIDcard.DetectAsync(idCardRequest));
        }

        [Fact]
        public async void when_HttpResponse_is_InternalServerError()
        {
            var idCardRequest = BuildDefaultIDCardRequest("http://xxx.com/idcard.jpg");
            var aliyunOptions = BuildDefaultAliyunOCROptions("http://aliyun.com/ocrapi");

            var imageResponse = HttpResponseMessageBuilder.New
                .WithBytes(new byte[] { 1 })
                .Build();

            var errorResponse = HttpResponseMessageBuilder.New
                .WithHttpStatusCode(HttpStatusCode.InternalServerError)
                .Build();

            var httpClient = MockHttpMessageHandlerBuilder.New
                .AddHttpResponseMessage("idcard.jpg", imageResponse)
                .AddHttpResponseMessage("ocrapi", errorResponse)
                .BuildHttpClient();

            aliyunOptions.HttpClient = httpClient;

            IIDCardClient aliyunIDcard = new AliyunIDCardClient(aliyunOptions);

            var response = await aliyunIDcard.DetectAsync(idCardRequest);

            Assert.False(response.Success);
        }

        [Fact]
        public async void when_HttpResponse_is_OK()
        {
            var idCardRequest = BuildDefaultIDCardRequest("http://xxx.com/idcard.jpg");
            idCardRequest.Type = IDCardType.Face;
            var aliyunOptions = BuildDefaultAliyunOCROptions("http://aliyun.com/ocrapi");
            aliyunOptions.Appcode = "testcode";

            var imageResponse = HttpResponseMessageBuilder.New
                .WithBytes(new byte[] { 1 })
                .Build();

            var json = this.GetResourceText("aliyun_orc_response.json");
            var jsonResponse = HttpResponseMessageBuilder.New
                .WithJsonContent(json)
                .Build();

            var httpClient = MockHttpMessageHandlerBuilder.New
                .AddHttpResponseMessage("idcard.jpg", imageResponse)
                .AddHttpMessage(request => VerifyHttpRequestMessage(request), jsonResponse)
                .BuildHttpClient();

            aliyunOptions.HttpClient = httpClient;

            IIDCardClient aliyunIDcard = new AliyunIDCardClient(aliyunOptions);

            var response = await aliyunIDcard.DetectAsync(idCardRequest);

            Assert.True(response.Success, response.Message);
            Assert.Equal("张三", response.Result.Name);
            Assert.Equal("汉", response.Result.Nation);
            Assert.Equal("20000101", response.Result.Birth);
        }

        private bool VerifyHttpRequestMessage(HttpRequestMessage request)
        {
            if (request.RequestUri.OriginalString != "http://aliyun.com/ocrapi")
            {
                return false;
            }

            var requestBody = request.Content.ReadAsStringAsync().Result;
            var json = requestBody.JsonToObject<dynamic>();

            Assert.Equal(HttpMethod.Post, request.Method);

            Assert.Equal("APPCODE testcode", request.Headers.Authorization.ToString());

            string imageDataType = json.inputs[0].image.dataType.ToString();
            Assert.Equal("50", imageDataType);

            var imageDataValue = json.inputs[0].image.dataValue.ToString();

            Assert.Equal(new byte[] { 1 }.GetBase64(), imageDataValue);

            string iconfigureDataType = json.inputs[0].image.dataType.ToString();
            Assert.Equal("50", iconfigureDataType);

            var iconfigureDataValue = json.inputs[0].configure.dataValue.side.ToString();

            Assert.Equal("face", iconfigureDataValue);
            return true;
        }
    }
}