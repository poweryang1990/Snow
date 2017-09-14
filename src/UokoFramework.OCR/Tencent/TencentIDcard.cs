using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UokoFramework.OCR.Common;
using UokoFramework.OCR.Tencent.Utils;
using UokoFramework.Web.Utils;

namespace UokoFramework.OCR.Tencent
{
    /// <summary>
    /// 腾讯身份OCR识别
    /// </summary>
    public static class TencentIDcard
    {
        private static string url = "http://service.image.myqcloud.com/ocr/idcard";
        private static int appId = 1252754859;
        private static string secretId = "AKIDEQRHD2SGWaSYWJbCyb2GUZS8dOyMHzKU";
        private static string secretKey = "CGDzYv6QdiibI7pMhreXIsGTUIyTlrmV";
        private static string bucketName = "idcard";

        private static WebApiProvider webApiProvider = new WebApiProvider();

        /// <summary>
        /// 腾讯身份OCR识别
        /// </summary>
        /// <param name="imgUrls"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public static TencentResult Detect(string[] imgUrls, IDcardType cardType)
        {
            var expired = DateTime.Now.ToUnixTime() / 1000 + 60;
            var sign = TencentSign.DetectionSignature(appId, secretId, secretKey, expired, bucketName);

            var data = new Dictionary<string, object>();
            data.Add("appid", appId);
            data.Add("bucket", bucketName);
            data.Add("card_type", cardType.GetHashCode());
            data.Add("url_list", imgUrls);

            var reulst = webApiProvider.Post<object, TencentResult>(url, data, sign);
            return reulst;
        }
    }
}
