using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UOKOFramework.Cache.Memory
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCache : IDisposable, IMemoryCache
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        private Dictionary<string, CacheItem> _memoryCache = new Dictionary<string, CacheItem>();

        private readonly ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dateTimeProvider"></param>
        public MemoryCache(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(CacheKey key, object value)
        {
            return Set(key, value, DateTime.MaxValue);
        }

        /// <summary>
        /// 增加或者更新缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="duration">相对的缓存时间</param>
        public bool Set(CacheKey key, object value, TimeSpan duration)
        {
            return Set(key, value, this._dateTimeProvider.Now.Add(duration));
        }

        /// <summary>
        /// 增加或者更新缓存项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireAt">绝对的过期时间</param>
        public bool Set(CacheKey key, object value, DateTime expireAt)
        {
            var keyString = key.ToString();

            _readWriteLock.EnterUpgradeableReadLock();

            try
            {
                if (_memoryCache.TryGetValue(keyString, out var result))
                {
                    _readWriteLock.EnterWriteLock();
                    try
                    {
                        result.Value = value;
                        result.ExpireTime = expireAt;
                    }
                    finally
                    {
                        _readWriteLock.ExitWriteLock();
                    }
                }
                else
                {
                    _readWriteLock.EnterWriteLock();
                    try
                    {
                        _memoryCache.Add(keyString, new CacheItem(value, expireAt));
                    }
                    finally
                    {
                        _readWriteLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _readWriteLock.ExitUpgradeableReadLock();
            }
            return true;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(CacheKey key)
        {
            var keyString = key.ToString();

            _readWriteLock.EnterReadLock();

            try
            {
                if (_memoryCache.TryGetValue(keyString, out var cacheItem))
                {
                    if (cacheItem.ExpireTime > this._dateTimeProvider.Now)
                    {
                        return cacheItem.Value;
                    }
                }
                return default(object);
            }
            finally
            {
                _readWriteLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TCacheValue"></typeparam>
        /// <param name="cahceKey"></param>
        /// <returns></returns>
        public TCacheValue Get<TCacheValue>(CacheKey cahceKey)
        {
            var result = Get(cahceKey);
            if (result != null)
            {
                return (TCacheValue)result;
            }
            return default(TCacheValue);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IDictionary<CacheKey, object> Get(IEnumerable<CacheKey> keys)
        {
            var values = new Dictionary<CacheKey, object>();

            _readWriteLock.EnterReadLock();
            try
            {
                foreach (var key in keys)
                {
                    if (_memoryCache.TryGetValue(key.ToString(), out var cacheItem))
                    {

                        if (cacheItem.ExpireTime > this._dateTimeProvider.Now)
                        {
                            values.Add(key, cacheItem.Value);
                        }
                    }
                }
            }
            finally
            {
                _readWriteLock.ExitReadLock();
            }

            return values;
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(CacheKey key)
        {
            var keyString = key.ToString();

            _readWriteLock.EnterWriteLock();

            try
            {
                return _memoryCache.Remove(keyString);
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        public bool RemoveByPrefix(CacheKey keyPrefix)
        {
            var keyPrefixString = keyPrefix.ToString();
            _readWriteLock.EnterWriteLock();

            try
            {
                var keys = _memoryCache.Keys
                    .Where(t => t.StartsWith(keyPrefixString))
                    .ToList();

                var result = true;

                foreach (var key in keys)
                {
                    result &= _memoryCache.Remove(key);
                }

                return result;
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }

        #region IDisposable Members

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _memoryCache = null;
            }
            _readWriteLock.Dispose();
        }

        /// <summary>
        /// InMemoryCacheProvider
        /// </summary>
        ~MemoryCache()
        {
            Dispose(false);
        }

        #endregion

        private class CacheItem
        {
            public CacheItem(object value, DateTime expiredTime)
            {
                Value = value;
                ExpireTime = expiredTime;
            }

            /// <summary>
            /// 值
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// 过期时间
            /// </summary>
            public DateTime ExpireTime { get; set; }
        }
    }
}
