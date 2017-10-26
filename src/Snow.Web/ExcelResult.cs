namespace Snow.Web
{
    /// <summary>
    /// ExcelResult
    /// </summary>
    public class ExcelResult : DownloadResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="excelFileName">Excel文件名(包含扩展名)</param>
        /// <param name="excelFileBytes">文件的原始字节</param>
        public ExcelResult(string excelFileName, byte[] excelFileBytes)
            : base("application/vnd.ms-excel", excelFileName, excelFileBytes) { }
    }
}