using System;
using System.Collections.Generic;
using System.Text;

namespace UokoFramework.OCR.Common
{
    /// <summary>
    /// IDcard RequestInfo
    /// </summary>
    public class IDcardRequestInfo
    {
        /// <summary>
        /// 身份证图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 身份证正反面
        /// </summary>
        public IDcardType IDcardType { get; set; }
    }
}
