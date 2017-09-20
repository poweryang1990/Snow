using System;
using Newtonsoft.Json.Linq;

namespace UOKOFramework.Serialization.Extensions
{
    /// <summary>
    /// JObject的扩展方法
    /// </summary>
    public static class JObjectExtension
    {
        /// <summary>
        /// 获取指定的属性
        /// </summary>
        /// <param name="jsonObject">this</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="stringComparison">name是否忽略大小写</param>
        /// <returns>指定的属性</returns>
        public static JToken GetProperty(
            this JObject jsonObject,
            string propertyName,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            if (jsonObject == null || string.IsNullOrWhiteSpace(propertyName))
            {
                return null;
            }

            if (jsonObject.TryGetValue(propertyName, stringComparison, out var jtoken))
            {
                return jtoken;
            }

            return null;
        }

        /// <summary>
        /// 获取指定的属性，转为sting类型。
        /// </summary>
        /// <param name="jsonObject">this</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="stringComparison">name是否忽略大小写</param>
        /// <returns>指定的属性的字符串值</returns>
        public static string GetStringProperty(
            this JObject jsonObject,
            string propertyName,
            StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
        {
            var property = jsonObject.GetProperty(propertyName, stringComparison);
            if (property == null)
            {
                return string.Empty;
            }
            return property.ToString();
        }
    }
}