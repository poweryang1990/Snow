// ReSharper disable InconsistentNaming

using System.Net.Http;

namespace UOKOFramework.OCR.Alibaba
{
    /// <summary>
    /// 阿里OCR配置
    /// </summary>
    public class AlibabaOCROptions
    {
        /// <summary>
        /// api地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// app code
        /// </summary>
        public string Appcode { get; set; }

        /// <summary>
        /// HttpClient
        /// </summary>
        public HttpClient HttpClient { get; set; }
    }
}