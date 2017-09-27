using System;
using System.Collections.Generic;
using System.Text;

namespace UOKOFramework.Cache
{
    /// <summary>
    /// CacheKey由三部分组成。
    /// <para>1. scope : 表示cache的应用范围</para>
    /// <para>2. params: 表示构造key所需的动态参数</para>
    /// <para>3. name  : 表示构造key所需的固定部分的名字</para>
    /// <para>例子:user:&amp;user-id=xxx#profile</para>
    /// </summary>
    public class CacheKey
    {
        private readonly string _scope;

        private IDictionary<string, string> _params;

        private string _name;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scope">key的scope</param>
        protected CacheKey(string scope)
        {
            Throws.ArgumentNullException(scope, nameof(scope));
            this._scope = scope;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scope">key的scope</param>
        /// <param name="name">key的固定部分的名字</param>
        protected CacheKey(string scope, string name)
        {
            Throws.ArgumentNullException(scope, nameof(scope));
            Throws.ArgumentNullException(name, nameof(name));
            this._scope = scope;
            this._name = name;
        }

        /// <summary>
        /// 克隆key
        /// </summary>
        /// <param name="name">key的固定部分的名字</param>
        /// <returns>克隆后的新CacheKey</returns>
        protected CacheKey Clone(string name)
        {
            var clonedCacheKey = (CacheKey)MemberwiseClone();
            clonedCacheKey._name = name.Trim();
            if (this._params != null)
            {
                clonedCacheKey._params = new Dictionary<string, string>(_params.Count);
                foreach (var param in _params)
                {
                    clonedCacheKey._params.Add(param);
                }
            }
            return clonedCacheKey;
        }

        /// <summary>
        /// 构造key
        /// </summary>
        /// <param name="key">动态参数的key</param>
        /// <param name="value">动态参数的value</param>
        /// <returns>当前CacheKey自身</returns>
        protected void SetParams(string key, string value)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(value, nameof(value));
            this._params = new Dictionary<string, string>
            {
                [key] = value
            };
        }

        /// <summary>
        /// 构造key
        /// </summary>
        /// <param name="keyValues">动态参数集合</param>
        /// <returns></returns>
        protected void SetParams(IDictionary<string, string> keyValues)
        {
            Throws.ArgumentNullException(keyValues, nameof(keyValues));
            this._params = keyValues;
        }

        /// <summary>
        /// 转换成字符串形式的key，结构如下：
        /// <para>scope:&amp;param1=value1&amp;param2=value2#name</para>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this._name == null)
            {
                throw new ArgumentNullException(nameof(this._name));
            }

            var keyBuilder = new StringBuilder(this._scope);
            keyBuilder.Append(':');

            if (this._params != null)
            {
                foreach (var param in this._params)
                {
                    keyBuilder.Append('&');
                    keyBuilder.Append(param.Key);
                    keyBuilder.Append('=');
                    keyBuilder.Append(param.Value);
                }
            }

            keyBuilder.Append('#');
            keyBuilder.Append(this._name);

            return keyBuilder.ToString().ToLower();
        }
    }
}