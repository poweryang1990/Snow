using System.Collections.Generic;

namespace UOKOFramework.Extensions
{
    /// <summary>
    /// Dictionary的扩展方法
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 通过Key获取值，如果key不寻存在，则返回指定的默认值
        /// </summary>
        /// <typeparam name="TKey">Key的类型</typeparam>
        /// <typeparam name="TValue">Value的类型</typeparam>
        /// <param name="dictionary">this</param>
        /// <param name="key">指定的key</param>
        /// <param name="defaultValue">可选的默认值</param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
            {
                return defaultValue;
            }

            if (dictionary.TryGetValue(key, out var value))
            {
                return value;
            }
            return defaultValue;
        }
    }
}
