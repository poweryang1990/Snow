using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UokoFramework.OCR.Tencent.Common;
using UokoFramework.Web.Utils;

namespace UokoFramework.OCR.Tencent
{
    /// <summary>
    /// 腾讯身份OCR识别
    /// </summary>
    public class TencentIDcard
    {
        const string url = "http://service.image.myqcloud.com/ocr/idcard";
        private int appId= 1252754859;
        private string secretId= "AKIDEQRHD2SGWaSYWJbCyb2GUZS8dOyMHzKU";
        private string secretKey= "CGDzYv6QdiibI7pMhreXIsGTUIyTlrmV";
        private string bucketName= "idcard";

        private static WebApiProvider webApiProvider = new WebApiProvider();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgUrls"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        public RequestResult Detect(string[] imgUrls, IDcardType cardType)
        {

            var expired = DateTime.Now.ToUnixTime() / 1000 + 60;
            var sign = Sign.DetectionSignature(appId, secretId, secretKey, expired, bucketName);

            var header = new Dictionary<string, string>();
            header.Add("Authorization", sign);
            header.Add("Content-Type", "application/json");

            var data = new Dictionary<string, object>();
            data.Add("appid", appId);
            data.Add("bucket", bucketName);
            data.Add("card_type", cardType.GetHashCode());
            data.Add("url_list", imgUrls);

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", sign);
            var memStream = new MemoryStream();

            var json = JsonConvert.SerializeObject(data);
            var jsonByte = Encoding.GetEncoding("utf-8").GetBytes(json.ToString());
            memStream.Write(jsonByte, 0, jsonByte.Length);
            request.ContentLength = memStream.Length;

            var reulst = webApiProvider.Post<object, RequestResult>(request.RequestUri, data);
            return reulst;
        }
    }
}
