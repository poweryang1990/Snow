using System;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using Snow.Extensions;
using Snow.OCR.Aliyun;
using TestHelper;
using TestHelper.Builders;
using Xunit;

namespace Snow.OCR.Test.Aliyun.AliyunIDCardClients
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

            var httpClient = MockHttpClientBuilder.New
                .AddBytesResponse("idcard.jpg", new byte[] { 1 })
                .AddStatusCodeResponse("ocrapi", HttpStatusCode.InternalServerError)
                .Build();

            aliyunOptions.HttpClient = httpClient;

            IIDCardClient aliyunIDcard = new AliyunIDCardClient(aliyunOptions);

            var response = await aliyunIDcard.DetectAsync(idCardRequest);

            Assert.False(response.Success);
        }

        [Fact]
        public async void when_HttpResponse_is_OK_Face()
        {
            var idCardRequest = BuildDefaultIDCardRequest("http://xxx.com/idcard.jpg");
            idCardRequest.Type = IDCardType.Face;
            var aliyunOptions = BuildDefaultAliyunOCROptions("http://aliyun.com/ocrapi");
            aliyunOptions.Appcode = "testcode";

            var json = this.GetBelongAssemblyResourceText("aliyun_ocr_face_response.json");

            var httpClient = MockHttpClientBuilder.New
                .AddBytesResponse("idcard.jpg", new byte[] { 1 })
                .AddJsonResponse(request => VerifyHttpRequestMessage(request), json)
                .Build();

            aliyunOptions.HttpClient = httpClient;

            IIDCardClient aliyunIDcard = new AliyunIDCardClient(aliyunOptions);

            var response = await aliyunIDcard.DetectAsync(idCardRequest);

            Assert.True(response.Success, response.Message);
            var idCard = response.Result;

            idCard.Name.Should().Be("张三");
            idCard.Nation.Should().Be("汉");
            idCard.Birth.Should().Be("20000101");
            idCard.Address.Should().Be("浙江省杭州市余杭区文一西路969号");
            idCard.Id.Should().Be("1234567890");
            idCard.Gender.Should().Be(Gender.Male);
            idCard.Authority.Should().BeNull();
            idCard.StartDate.Should().BeNull();
            idCard.EndDate.Should().BeNull();
        }

        [Fact]
        public async void when_HttpResponse_is_OK_Back()
        {
            var idCardRequest = BuildDefaultIDCardRequest("http://xxx.com/idcard.jpg");
            idCardRequest.Type = IDCardType.Back;
            var aliyunOptions = BuildDefaultAliyunOCROptions("http://aliyun.com/ocrapi");
            aliyunOptions.Appcode = "testcode";

            var json = this.GetBelongAssemblyResourceText("aliyun_ocr_back_response.json");

            var httpClient = MockHttpClientBuilder.New
                .AddBytesResponse("idcard.jpg", new byte[] { 1 })
                .AddJsonResponse("ocrapi", json)
                .Build();

            aliyunOptions.HttpClient = httpClient;

            IIDCardClient aliyunIDcard = new AliyunIDCardClient(aliyunOptions);

            var response = await aliyunIDcard.DetectAsync(idCardRequest);

            Assert.True(response.Success, response.Message);
            var idCard = response.Result;
            Assert.Equal("杭州市公安局", idCard.Authority);
            Assert.Equal("19700101", idCard.StartDate);
            Assert.Equal("19800101", idCard.EndDate);
            Assert.Null(idCard.Name);
            Assert.Null(idCard.Nation);
            Assert.Null(idCard.Birth);
            Assert.Null(idCard.Address);
            Assert.Null(idCard.Id);
            Assert.Null(idCard.Gender);
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