using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UokoFramework.OCR.Common;

namespace UokoFramework.OCR.Alibaba.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class AlibabaHttpRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="bodyInfo"></param>
        /// <returns></returns>
        public static string GetResponse(AlibabaOCROptions options, string bodyInfo)
        {
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(options.Url);
            httpRequest.Method = "Post";
            httpRequest.Headers.Add("Authorization", "APPCODE " + options.Appcode);
            httpRequest.ContentType = "application/json; charset=UTF-8";

            if (bodyInfo.Length > 0)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodyInfo);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            HttpWebResponse httpResponse = null;
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var responseInfo = reader.ReadToEnd();
            return responseInfo;
        }
    }
}
