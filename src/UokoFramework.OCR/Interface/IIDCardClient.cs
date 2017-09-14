using System;
using System.Collections.Generic;
using System.Text;
using UokoFramework.OCR.Common;

namespace UokoFramework.OCR.Interface
{
    /// <summary>
    /// 统一暴露对外的身份证 OCR识别接口
    /// </summary>
    public interface IIDCardClient
    {
        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IDCardResponse Detect(IDCardRequest info);
    }
}
