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

        /// <summary>
        /// 获取Scope
        /// </summary>
        internal string Scope => this._scope;

        /// <summary>
        /// 获取名字
        /// </summary>
        internal string Name { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scope">key的scope</param>
        protected CacheKey(string scope)
        {
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new ArgumentNullException(nameof(scope));
            }
            _scope = scope.ToLower();
        }

        /// <summary>
        /// 克隆key
        /// </summary>
        /// <param name="name">key的固定部分的名字</param>
        /// <returns>克隆后的新CacheKey</returns>
        protected CacheKey Clone(string name)
        {
            var clonedCacheKey = (CacheKey)MemberwiseClone();
            clonedCacheKey.Name = name.Trim();
            if (_params != null)
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
        /// <typeparam name="TCacheKey">具体的key的类型</typeparam>
        /// <param name="key">动态参数的key</param>
        /// <param name="value">动态参数的value</param>
        /// <returns>当前CacheKey自身</returns>
        protected TCacheKey Build<TCacheKey>(string key, string value)
            where TCacheKey : CacheKey
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _params = new Dictionary<string, string>()
            {
                [key] = value
            };

            return (TCacheKey)this;
        }

        /// <summary>
        /// 构造key
        /// </summary>
        /// <typeparam name="TCacheKey">具体的key的类型</typeparam>
        /// <param name="keyValues">动态参数集合</param>
        /// <returns></returns>
        protected TCacheKey Build<TCacheKey>(IDictionary<string, string> keyValues)
            where TCacheKey : CacheKey
        {
            if (keyValues == null)
            {
                throw new ArgumentNullException(nameof(keyValues));
            }

            _params = keyValues;

            return (TCacheKey)this;
        }

        /// <summary>
        /// 转换成字符串形式的key，结构如下：
        /// <para>scope:&amp;param1=value1&amp;param2=value2#name</para>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Name == null)
            {
                throw new ArgumentNullException(nameof(Name));
            }

            var keyBuilder = new StringBuilder(_scope);
            keyBuilder.Append(':');

            if (_params != null)
            {
                foreach (var param in _params)
                {
                    keyBuilder.Append('&');
                    keyBuilder.Append(param.Key);
                    keyBuilder.Append('=');
                    keyBuilder.Append(param.Value);
                }
            }

            keyBuilder.Append('#');
            keyBuilder.Append(Name);

            return keyBuilder.ToString();
        }
    }
}