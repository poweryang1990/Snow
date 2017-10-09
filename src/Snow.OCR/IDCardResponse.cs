// ReSharper disable InconsistentNaming

namespace Snow.OCR
{
    /// <summary>
    /// ID Card Response
    /// </summary>
    public class IDCardResponse
    {
        /// <summary>
        /// 请求状态
        /// </summary>
        public bool Success { get; internal set; }

        /// <summary>
        /// Message
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// 识别信息主体
        /// </summary>
        public IDCard Result { get; internal set; }
    }
}