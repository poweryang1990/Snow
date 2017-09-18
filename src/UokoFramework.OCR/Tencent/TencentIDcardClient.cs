using System;
using System.Collections.Generic;
using System.Text;
using UokoFramework.OCR.Common;
using System.Linq;
using UokoFramework.OCR.Interface;
using UokoFramework.OCR.Tencent.Utils;
using UokoFramework.Web.Utils;

namespace UokoFramework.OCR.Tencent
{
    /// <summary>
    /// 腾讯OCR身份证 实现
    /// </summary>
    public class TencentIDCardClient : IIDCardClient
    {
        private static WebApiProvider webApiProvider = new WebApiProvider();
        private readonly TencentORCOptions _tencentORCOptions;
        private static string url = "http://service.image.myqcloud.com/ocr/idcard";

        /// <summary>
        /// 
        /// </summary>
        public TencentIDCardClient(TencentORCOptions tencentORCOptions)
        {
            _tencentORCOptions = tencentORCOptions;
        }

        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public IDCardResponse Detect(IDCardRequest info)
        {
            try
            {
                if (info==null)
                {
                    throw new Exception("Request请求信息为空");
                }
                if (string.IsNullOrEmpty(info.ImgUrl))
                {
                    throw new Exception("ImgUrl信息为空");
                }
                List<string> urls = new List<string>() { info.ImgUrl };


                var expired = DateTime.Now.ToUnixTime() / 1000 + 60;
                var sign = TencentSign.DetectionSignature(_tencentORCOptions.AppId, _tencentORCOptions.SecretId, _tencentORCOptions.SecretKey, expired, _tencentORCOptions.BucketName);

                var dic = new Dictionary<string, object>();
                dic.Add("appid", _tencentORCOptions.AppId);
                dic.Add("bucket", _tencentORCOptions.BucketName);
                dic.Add("card_type", info.IDcardType.GetHashCode());
                dic.Add("url_list", urls);

                var data = webApiProvider.Post<object, TencentResponse>(url, dic, sign);
               
                var reulst = data.Result_List.Select(r => new IDCardResponse
                {
                    Status = r.Code == 0 ? true : false,
                    Message = r.Message,
                    IDcardInfo = new IDcardInfo()
                    {
                        Address = r.Data.Address,
                        Authority = r.Data.Authority,
                        Birth = r.Data.Birth,
                        Id = r.Data.Id,
                        Name = r.Data.Name,
                        Nation = r.Data.Nation,
                        Sex = r.Data.Sex,
                        Valid_date = r.Data.Valid_date
                    }
                });
                return reulst.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new IDCardResponse()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
    }
}
