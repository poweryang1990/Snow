// ReSharper disable InconsistentNaming

namespace Snow.OCR
{
    /// <summary>
    /// ID Card Request
    /// </summary>
    public class IDCardRequest
    {
        /// <summary>
        /// 身份证图片地址
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 身份证正反面
        /// </summary>
        public IDCardType Type { get; set; }
    }
}