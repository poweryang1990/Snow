using System;
using UokoFramework.OCR.Interface;
using UokoFramework.OCR.Interface.Impl;
using UokoFramework.OCR.Tencent;
using Xunit;

namespace UokoFramework.OCR.Test
{
    public class TencentIDcardTest
    {
        [Fact]
        public void Detect()
        {
            IIDcardDetectSvc tencentIDcard = new TencetIDcardDetect();
            var result = tencentIDcard.Detect(new Common.IDcardRequestInfo()
            {
                IDcardType = Common.IDcardType.Face,
                ImgUrl = "http://7xodcr.com1.z0.glb.clouddn.com/%E6%AD%A3%E9%9D%A2.png",
            });
        }
    }
}
