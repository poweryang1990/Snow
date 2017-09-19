using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UokoFramework.OCR.Alibaba.Utils;
using UokoFramework.OCR.Common;
using UokoFramework.OCR.Interface;
using UokoFramework.Web.Utils;
using System.Linq;

namespace UokoFramework.OCR.Alibaba
{
    /// <summary>
    /// 阿里OCR身份证识别
    /// </summary>
    public class AlibabaIDCardClient : IIDCardClient
    {
        private readonly AlibabaOCROptions _options;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name='options'></param>
        public AlibabaIDCardClient(AlibabaOCROptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name='info'></param>
        /// <returns></returns>
        public IDCardResponse Detect(IDCardRequest info)
        {
            if (info == null)
            {
                throw new ArgumentException("Request请求信息为空");
            }
            if (string.IsNullOrEmpty(info.ImgUrl))
            {
                throw new ArgumentException("ImgUrl信息为空");
            }
            IDCardResponse response = new IDCardResponse();
            try
            {
                var bodyInfo = AlibabaOCRParam.EncapsulationParameters(info.ImgUrl, info.IDcardType);
                var responseInfo = AlibabaHttpRequest.GetResponse(_options, bodyInfo);
                var data = JsonConvert.DeserializeObject<AlibabaResponse>(responseInfo);

                if (data != null && data.Outputs.Count() > 0)
                {
                    var dataValue = data.Outputs.FirstOrDefault().OutputValue.DataValue;

                    response.Status = dataValue.Success;
                    response.IDcardInfo = new IDcardInfo()
                    {
                        Address = dataValue.Address,
                        Authority = dataValue.Issue,
                        Birth = dataValue.Birth,
                        Name = dataValue.Name,
                        Nation = dataValue.Nationality,
                        Sex = dataValue.Sex,
                        Valid_date = dataValue.Start_date + "-" + dataValue.End_date,
                    };
                }
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
    