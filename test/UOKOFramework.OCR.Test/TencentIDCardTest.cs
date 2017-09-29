using UOKOFramework.OCR.Tencent;
using UOKOFramework;
using Xunit;
using System.Net.Http;
using System;
using UOKOFramework.OCR.Aliyun;

// ReSharper disable InconsistentNaming

namespace UOKOFramework.OCR.Test
{
    public class TencentIDCardTest
    {

        private readonly IDCardRequest request = new IDCardRequest()
        {
            Type = IDCardType.Face,
            ImgUrl = "http://7xodcr.com1.z0.glb.clouddn.com/%E6%AD%A3%E9%9D%A2.png",
        };

        #region Tencent
        private TencentOCROptions _tencentOptions = new TencentOCROptions
        {
            Apiurl = "http://service.image.myqcloud.com/ocr/idcard",
            AppId = "1252754859",
            SecretId = "AKIDEQRHD2SGWaSYWJbCyb2GUZS8dOyMHzKU",
            SecretKey = "CGDzYv6QdiibI7pMhreXIsGTUIyTlrmV",
            Bucket = "idcard",
            HttpClient = new HttpClient(),
        };

        [Fact]
        public async void Normal_DetectTencent()
        {
            IIDCardClient tencentIDcard = new TencentIDCardClient(_tencentOptions, new Clock());
            var result = await tencentIDcard.DetectAsync(request);
            Assert.Equal("刘源", result.Result.Name);
        }

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
        #endregion

        #region Aliyun


        AliyunOCROptions _aliyunOptions = new AliyunOCROptions
        {
            Appcode = "11",
            Url = "www.biying.com",
            HttpClient = new HttpClient(),
        };
        [Fact]
        public void Normal_DetectAliyun()
        {
            IIDCardClient aliyunIDcard = new AliyunIDCardClient(_aliyunOptions);
            var result = aliyunIDcard.DetectAsync(request).Result;
        }

        [Fact]
        public void when_AliyunOCROptions_isNull_DetectAliyun()
        {
            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(null));
        }

        [Fact]
        public void when_AliyunOCROptions_Appcode_isNull_DetectAliyun()
        {
            _aliyunOptions.Appcode = null;

            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(_aliyunOptions));
        }

        [Fact]
        public void when_AliyunOCROptions_Url_isNull_DetectAliyun()
        {
            _aliyunOptions.Url = null;

            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(_aliyunOptions));
        }

        [Fact]
        public void when_AliyunOCROptions_HttpClient_isNull_DetectAliyun()
        {
            _aliyunOptions.HttpClient = null;

            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(_aliyunOptions));
        }

        [Fact]
        public async void when_IDCardRequest_isNull_DetectAliyun()
        {
            IIDCardClient aliyunIDcard = new AliyunIDCardClient(_aliyunOptions);
            await Assert.ThrowsAsync<ArgumentNullException>(() => aliyunIDcard.DetectAsync(null));
        }

        [Fact]
        public async void when_IDCardRequest_ImgUrl_isNull_DetectAliyun()
        {
            IIDCardClient aliyunIDcard = new AliyunIDCardClient(_aliyunOptions);
            await Assert.ThrowsAsync<ArgumentNullException>(() => aliyunIDcard.DetectAsync(new IDCardRequest()
            {
                Type = IDCardType.Face,
                ImgUrl = null,
            }));

        }
        #endregion
    }
}
