using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace UokoFramework.OCR.Tencent
{
    /// <summary>
    /// 接收腾讯OCR识别返回值
    /// </summary>
    internal class TencentOCRResponse
    {
        /// <summary>
        ///
        /// </summary>
        public List<Maininfo> Result_List { get; set; }

        /// <summary>
        /// 主体信息
        /// </summary>
        public class Maininfo
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
            /// 身份证识别信息, 正反面的都在一起
            /// </summary>
            public Data Data { get; set; }
        }

        /// <summary>
        /// Data信息
        /// </summary>
        public class Data
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 性别
            /// </summary>
            public string Sex { get; set; }

            /// <summary>
            /// 民族
            /// </summary>
            public string Nation { get; set; }

            /// <summary>
            /// 出生日期
            /// </summary>
            public string Birth { get; set; }

            /// <summary>
            /// 地址
            /// </summary>
            public string Address { get; set; }

            /// <summary>
            /// 身份证号
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// 发证机关
            /// </summary>
            public string Authority { get; set; }

            /// <summary>
            /// 证件有效期
            /// </summary>
            public string Valid_date { get; set; }
        }
    }
}