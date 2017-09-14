using System;
using System.Collections.Generic;
using System.Text;

namespace UokoFramework.OCR.Common
{
    /// <summary>
    /// IDcard Result
    /// </summary>
    public class IDcardResultInfo
    {
        /// <summary>
        /// 请求状态, true为成功
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 识别信息主体
        /// </summary>
        public IDcardInfo IDcardInfo { get; set; }
    }

    /// <summary>
    /// Data信息
    /// </summary>
    public class IDcardInfo
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
