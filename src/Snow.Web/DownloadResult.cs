using System.Web.Mvc;

namespace Snow.Web
{
    /// <summary>
    /// 下载Result
    /// </summary>
    public class DownloadResult : ActionResult
    {
        private readonly string _contentType;
        private readonly string _fileName;
        private readonly byte[] _fileBytes;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contentType">Content-Type</param>
        /// <param name="fileName">文件名(包含扩展名)</param>
        /// <param name="fileBytes">文件的原始字节</param>
        public DownloadResult(string contentType, string fileName, byte[] fileBytes)
        {
            Throws.ArgumentNullException(contentType, nameof(contentType));
            Throws.ArgumentNullException(fileName, nameof(fileName));
            Throws.ArgumentNullException(fileBytes, nameof(fileBytes));
            this._contentType = contentType;
            this._fileName = fileName;
            this._fileBytes = fileBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = this._contentType;
            response.AddHeader("content-disposition", $"attachment;filename={this._fileName}");
            response.Buffer = true;
            response.BinaryWrite(this._fileBytes);
            response.Flush();
            response.End();
        }
    }
}