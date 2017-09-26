using UokoFramework.OCR.Alibaba;
using UokoFramework.OCR.Tencent;
using UOKOFramework;
using Xunit;
// ReSharper disable InconsistentNaming

namespace UokoFramework.OCR.Test
{
    /// <summary>
    /// todo:待完善。
    /// </summary>
    public class TencentIDCardTest
    {

        private readonly IDCardRequest request = new IDCardRequest()
        {
            Type = IDCardType.Face,
            ImgUrl = "http://7xodcr.com1.z0.glb.clouddn.com/%E6%AD%A3%E9%9D%A2.png",
        };

        [Fact]
        public void DetectTencent()
        {
            TencentOCROptions options = new TencentOCROptions
            {
                AppId = 1252754859,
                SecretId = "AKIDEQRHD2SGWaSYWJbCyb2GUZS8dOyMHzKU",
                SecretKey = "CGDzYv6QdiibI7pMhreXIsGTUIyTlrmV",
                Bucket = "idcard"
            };

            IIDCardClient tencentIDcard = new TencentIDCardClient(options, new Clock());
            var result = tencentIDcard.DetectAsync(request).Result;
        }

        [Fact]
        public void DetectAlibaba()
        {
            AlibabaOCROptions options = new AlibabaOCROptions
            {
                Appcode = "",
                Url = "",
            };

            IIDCardClient alibabaIDcard = new AlibabaIDCardClient(options);
            var result = alibabaIDcard.DetectAsync(request).Result;
        }
    }
}
