using System;
using Snow.OCR.Aliyun;
using Xunit;

namespace Snow.OCR.Test.Aliyun.AliyunIDCardClients
{
    public class ConstructorTest : BaseTest
    {
        [Fact]
        public void when_AliyunOCROptions_isNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(null));
        }

        [Fact]
        public void when_AliyunOCROptions_Appcode_isNull()
        {
            var aliyunOptions = BuildDefaultAliyunOCROptions();
            aliyunOptions.Appcode = null;

            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(aliyunOptions));
        }

        [Fact]
        public void when_AliyunOCROptions_Url_isNull()
        {
            var aliyunOptions = BuildDefaultAliyunOCROptions();
            aliyunOptions.Url = null;

            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(aliyunOptions));
        }

        [Fact]
        public void when_AliyunOCROptions_HttpClient_isNull()
        {
            var aliyunOptions = BuildDefaultAliyunOCROptions();
            aliyunOptions.HttpClient = null;

            Assert.Throws<ArgumentNullException>(() => new AliyunIDCardClient(aliyunOptions));
        }
    }
}