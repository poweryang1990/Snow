using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using UokoFramework.OCR.Common;
using UokoFramework.Web.Utils;

namespace UokoFramework.OCR.Alibaba.Utils
{
    /// <summary>
    /// 阿里OCR参数处理
    /// </summary>
    public static class AlibabaOCRParam
    {
        /// <summary>
        /// 组装阿里OCR身份证参数请求body参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="idCardType"></param>
        /// <returns></returns>
        public static string EncapsulationParameters(string url, IDCardType idCardType)
        {
            string imageBase64 = ImageHelper.ConvertImageURLToBase64(url);
            var param = new
            {
                inputs = new List<object>
                {
                     new
                     {
                         image = new
                         {
                             dataType = 50,
                             dataValue = imageBase64
                         },
                         configure = new
                         {
                             dataType = 50,
                             dataValue = new
                             {
                                 side = idCardType.ToString()
                             }
                         }
                     }
                }
            };
            var parameters = JsonConvert.SerializeObject(param);
            return parameters;
        }
    }
}
