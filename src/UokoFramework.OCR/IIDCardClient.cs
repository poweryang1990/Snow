// ReSharper disable InconsistentNaming

using System.Threading.Tasks;

namespace UOKOFramework.OCR
{
    /// <summary>
    /// 身份证OCR识别接口
    /// </summary>
    public interface IIDCardClient
    {
        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IDCardResponse> DetectAsync(IDCardRequest request);
    }
}