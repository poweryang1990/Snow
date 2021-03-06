﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snow.Cache.Memory
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCache : IDisposable, IMemoryCache
    {
        private readonly IClock _clock;

        private Dictionary<string, MemoryObject> _memoryCache = new Dictionary<string, MemoryObject>();

        private readonly ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clock"></param>
        public MemoryCache(IClock clock)
        {
            _clock = clock;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(CacheKey key, object value, TimeSpan? expiry)
        {
            DateTimeOffset? expireAt = null;
            if (expiry.HasValue)
            {
                expireAt = this._clock.Now.Add(expiry.Value);
            }
            var keyString = key.ToString();

            _readWriteLock.EnterUpgradeableReadLock();

            try
            {
                if (_memoryCache.TryGetValue(keyString, out var result))
                {
                    _readWriteLock.EnterWriteLock();
                    try
                    {
                        result.Object = value;
                        result.ExpireAt = expireAt;
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
                        _memoryCache.Add(keyString, new MemoryObject(value, expireAt));
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
                    if (cacheItem.IsExpired(this._clock.Now) == false)
                    {
                        return cacheItem.Object;
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

                        if (cacheItem.IsExpired(this._clock.Now) == false)
                        {
                            values.Add(key, cacheItem.Object);
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
        /// <param name="keyAsPrefix"></param>
        /// <returns></returns>
        public bool Delete(CacheKey key, bool keyAsPrefix)
        {
            var keyString = key.ToString();

            _readWriteLock.EnterWriteLock();

            try
            {
                if (keyAsPrefix)
                {
                    return DeleteByPrefix(key);
                }
                else
                {
                    return _memoryCache.Remove(keyString);
                }
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
        private bool DeleteByPrefix(CacheKey keyPrefix)
        {
            var keyPrefixString = keyPrefix.ToString();

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
    }
}
