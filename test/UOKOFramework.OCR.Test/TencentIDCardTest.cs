using UOKOFramework.OCR.Alibaba;
using UOKOFramework.OCR.Tencent;
using UOKOFramework;
using Xunit;
using System.Net.Http;
using System;
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

        #region Alibaba


        AlibabaOCROptions _alibabaOptions = new AlibabaOCROptions
        {
            Appcode = "11",
            Url = "www.biying.com",
            HttpClient = new HttpClient(),
        };
        [Fact]
        public void Normal_DetectAlibaba()
        {
            IIDCardClient alibabaIDcard = new AlibabaIDCardClient(_alibabaOptions);
            var result = alibabaIDcard.DetectAsync(request).Result;
        }

        [Fact]
        public void when_AlibabaOCROptions_isNull_DetectAlibaba()
        {
            Assert.Throws<ArgumentNullException>(() => new AlibabaIDCardClient(null));
        }

        [Fact]
        public void when_AlibabaOCROptions_Appcode_isNull_DetectAlibaba()
        {
            _alibabaOptions.Appcode = null;

            Assert.Throws<ArgumentNullException>(() => new AlibabaIDCardClient(_alibabaOptions));
        }

        [Fact]
        public void when_AlibabaOCROptions_Url_isNull_DetectAlibaba()
        {
            _alibabaOptions.Url = null;

            Assert.Throws<ArgumentNullException>(() => new AlibabaIDCardClient(_alibabaOptions));
        }

        [Fact]
        public void when_AlibabaOCROptions_HttpClient_isNull_DetectAlibaba()
        {
            _alibabaOptions.HttpClient = null;

            Assert.Throws<ArgumentNullException>(() => new AlibabaIDCardClient(_alibabaOptions));
        }

        [Fact]
        public async void when_IDCardRequest_isNull_DetectAlibaba()
        {
            IIDCardClient alibabaIDcard = new AlibabaIDCardClient(_alibabaOptions);
            await Assert.ThrowsAsync<ArgumentNullException>(() => alibabaIDcard.DetectAsync(null));
        }

        [Fact]
        public async void when_IDCardRequest_ImgUrl_isNull_DetectAlibaba()
        {
            IIDCardClient alibabaIDcard = new AlibabaIDCardClient(_alibabaOptions);
            await Assert.ThrowsAsync<ArgumentNullException>(() => alibabaIDcard.DetectAsync(new IDCardRequest()
            {
                Type = IDCardType.Face,
                ImgUrl = null,
            }));

        }
        #endregion
    }
}
