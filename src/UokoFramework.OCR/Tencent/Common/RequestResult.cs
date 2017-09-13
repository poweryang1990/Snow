using System;
using System.Collections.Generic;
using System.Text;

namespace UokoFramework.OCR.Tencent.Common
{
    /// <summary>
    /// 接收腾讯OCR识别返回值
    /// </summary>
    public class RequestResult
    {
        /// <summary>
        /// 服务器错误码，0 为成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 服务器返回的信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 当前图片的 url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 身份证识别信息, 分正反面
        /// </summary>
        public object Data { get; set; }
    }
}
