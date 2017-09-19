using System;
using UokoFramework.OCR.Alibaba;
using UokoFramework.OCR.Alibaba.Utils;
using UokoFramework.OCR.Common;
using UokoFramework.OCR.Interface;
using UokoFramework.OCR.Tencent;
using UokoFramework.OCR.Tencent.Utils;
using Xunit;

namespace UokoFramework.OCR.Test
{
    public class TencentIDCardTest
    {

        private IDCardRequest request = new IDCardRequest()
        {
            IDcardType = Common.IDCardType.Face,
            ImgUrl = "http://7xodcr.com1.z0.glb.clouddn.com/%E6%AD%A3%E9%9D%A2.png",
        };

        [Fact]
        public void DetectTencent()
        {
            TencentOCROptions options = new TencentOCROptions()
            {
                AppId = 1252754859,
                SecretId = "AKIDEQRHD2SGWaSYWJbCyb2GUZS8dOyMHzKU",
                SecretKey = "CGDzYv6QdiibI7pMhreXIsGTUIyTlrmV",
                BucketName = "idcard",
            };

            IIDCardClient tencentIDcard = new TencentIDCardClient(options);
            var result = tencentIDcard.Detect(request);
        }

        [Fact]
        public void DetectAlibaba()
        {
            AlibabaOCROptions options = new AlibabaOCROptions()
            {
                Appcode = "",
                Url = "",
            };

            IIDCardClient alibabaIDcard = new AlibabaIDCardClient(options);
            var result = alibabaIDcard.Detect(request);
        }
    }
}
