using System;
using UokoFramework.OCR.Interface;
using UokoFramework.OCR.Tencent;
using UokoFramework.OCR.Tencent.Utils;
using Xunit;

namespace UokoFramework.OCR.Test
{
    public class TencentIDCardTest
    {
        [Fact]
        public void Detect()
        {
            TencentORCOptions options = new TencentORCOptions()
            {
                AppId = 1252754859,
                SecretId = "AKIDEQRHD2SGWaSYWJbCyb2GUZS8dOyMHzKU",
                SecretKey = "CGDzYv6QdiibI7pMhreXIsGTUIyTlrmV",
                BucketName = "idcard",
            };

            var request = new Common.IDCardRequest()
            {
                IDcardType = Common.IDCardType.Face,
                ImgUrl = "http://7xodcr.com1.z0.glb.clouddn.com/%E6%AD%A3%E9%9D%A2.png",
            };

            IIDCardClient tencentIDcard = new TencentIDCardClient(options);
            var result = tencentIDcard.Detect(request);
        }
    }
}
