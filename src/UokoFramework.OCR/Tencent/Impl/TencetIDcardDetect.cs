using System;
using System.Collections.Generic;
using System.Text;
using UokoFramework.OCR.Common;
using UokoFramework.OCR.Tencent;
using System.Linq;

namespace UokoFramework.OCR.Interface.Impl
{
    /// <summary>
    /// 腾讯OCR身份证 实现
    /// </summary>
    public class TencetIDcardDetect : IIDcardDetectSvc
    {
        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public IDcardResultInfo Detect(IDcardRequestInfo info)
        {
            try
            {
                List<string> urls = new List<string>() { info.ImgUrl };
                var reulst = TencentIDcard.Detect(urls.ToArray(), info.IDcardType).Result_List.Select(r => new IDcardResultInfo
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
                return new IDcardResultInfo()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
    }
}
