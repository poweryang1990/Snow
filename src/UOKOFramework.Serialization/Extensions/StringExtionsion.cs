using Newtonsoft.Json;

namespace UOKOFramework.Serialization.Extensions
{
    /// <summary>
    /// String的扩展方法
    /// </summary>
    public static class StringExtionsion
    {
        /// <summary>
        /// JSON转化为对象
        /// </summary>
        /// <param name="json">this</param>
        /// <returns>对象</returns>
        public static TObject JsonToObject<TObject>(this string json)
        {
            if (json == null)
            {
                return default(TObject);
            }
            return JsonConvert.DeserializeObject<TObject>(json);
        }
    }
}
