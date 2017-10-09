using System.Net.Http;
using Snow.OCR.Aliyun;

namespace Snow.OCR.Test.Aliyun.AliyunIDCardClients
{
    public class BaseTest
    {
        protected IDCardRequest BuildDefaultIDCardRequest(string img = null)
        {
            return new IDCardRequest
            {
                Type = IDCardType.Face,
                ImgUrl = img ?? "http://7xodcr.com1.z0.glb.clouddn.com/%E6%AD%A3%E9%9D%A2.png",
            };
        }

        protected AliyunOCROptions BuildDefaultAliyunOCROptions(string apiUrl = null)
        {
            return new AliyunOCROptions
            {
                Appcode = "11",
                Url = apiUrl ?? "htt//aliyun.com",
                HttpClient = new HttpClient(),
            };
        }
    }
}