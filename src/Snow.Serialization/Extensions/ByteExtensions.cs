
// ReSharper disable CheckNamespace
namespace Snow.Extensions
{
    /// <summary>
    /// byte的扩展方法
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// JSON-Bytes转化为对象
        /// </summary>
        /// <param name="jsonBytes">this</param>
        /// <returns>对象</returns>
        public static TObject JsonBytesToObject<TObject>(this byte[] jsonBytes)
        {
            if (jsonBytes == null)
            {
                return default(TObject);
            }
            return jsonBytes.GetString().JsonToObject<TObject>();
        }
    }
}
