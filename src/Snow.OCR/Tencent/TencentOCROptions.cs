// ReSharper disable InconsistentNaming

using System.Net.Http;

namespace Snow.OCR.Tencent
{
    /// <summary>
    /// 腾讯OCR Options
    /// </summary>
    public class TencentOCROptions
    {
        /// <summary>
        /// 腾讯OCR api rul
        /// </summary>
        public string Apiurl { get; set; }

        /// <summary>
        /// app id
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 秘钥id
        /// </summary>
        public string SecretId { get; set; }

        /// <summary>
        /// 秘钥Key
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 图片空间
        /// </summary>
        public string Bucket { get; set; }

        /// <summary>
        /// HttpClient
        /// </summary>
        public HttpClient HttpClient { get; set; }
    }
}