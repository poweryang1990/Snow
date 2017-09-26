// ReSharper disable InconsistentNaming

using System.Net.Http;

namespace UOKOFramework.OCR.Tencent
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