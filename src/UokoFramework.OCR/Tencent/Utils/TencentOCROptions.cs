using System;
using System.Collections.Generic;
using System.Text;

namespace UokoFramework.OCR.Tencent.Utils
{
    /// <summary>
    /// 腾讯OCR Options
    /// </summary>
    public class TencentOCROptions
    {
        /// <summary>
        /// app id
        /// </summary>
        public int AppId { get; set; }

        /// <summary>
        /// 秘钥id
        /// </summary>
        public string SecretId { get; set; }

        /// <summary>
        /// 秘钥可以
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 空间名
        /// </summary>
        public string BucketName { get; set; }
    }
}
