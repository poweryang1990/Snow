﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

// ReSharper disable CheckNamespace
namespace Snow.Extensions
{
    /// <summary>
    /// Object的扩展方法
    /// </summary>
    public static class ObjectExtionsion
    {
        /// <summary>
        /// 转化为JSON字符串
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="settings">JSON序列化设置</param>
        /// <returns>JSON</returns>
        public static string ToJson(
            this object value,
            JsonSerializerSettings settings = null)
        {
            if (value == null)
            {
                return null;
            }

            if (settings != null)
            {
                return JsonConvert.SerializeObject(value, settings);
            }
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// 转化为JSON字符串
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="dateTimeFormat">日期格式化</param>
        /// <returns>JSON</returns>
        public static string ToJson(
            this object value,
            string dateTimeFormat)
        {
            if (value == null)
            {
                return null;
            }

            return JsonConvert.SerializeObject(value, new IsoDateTimeConverter
            {
                DateTimeFormat = dateTimeFormat
            });
        }

        /// <summary>
        /// 转化为JSON-bytes
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="settings">JSON序列化设置</param>
        /// <returns>bytes</returns>
        public static byte[] ToJsonBytes(
            this object value,
            JsonSerializerSettings settings = null)
        {
            return ToJson(value)?.GetBytes();
        }

        /// <summary>
        /// 转化为JSON-bytes
        /// </summary>
        /// <param name="value">this</param>
        /// <param name="dateTimeFormat">日期格式化</param>
        /// <returns>bytes</returns>
        public static byte[] ToJsonBytes(
            this object value,
            string dateTimeFormat)
        {
            return ToJson(value, dateTimeFormat)?.GetBytes();
        }
    }
}
