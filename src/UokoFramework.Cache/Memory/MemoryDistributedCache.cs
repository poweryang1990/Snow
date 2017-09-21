using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UOKOFramework.Cache.Lock;

namespace UOKOFramework.Cache.Memory
{
    /// <summary>
    /// 
    /// </summary>
    public class MemoryDistributedCache : IDistributedCache
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="memoryCache"></param>
        public MemoryDistributedCache(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set<TCache>(CacheKey key, TCache value, TimeSpan? expiry = null)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(value, nameof(value));
            return this._memoryCache.Set(key, value, expiry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public Task<bool> SetAsync<TCache>(CacheKey key, TCache value, TimeSpan? expiry = null)
        {
            return Task.FromResult(this.Set(key, value, expiry));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TCache Get<TCache>(CacheKey key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            return this._memoryCache.Get<TCache>(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<TCache> GetAsync<TCache>(CacheKey key)
        {
            return Task.FromResult(this.Get<TCache>(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyAsPrefix"></param>
        /// <returns></returns>
        public bool Delete(CacheKey key, bool keyAsPrefix = false)
        {
            Throws.ArgumentNullException(key, nameof(key));
            return this._memoryCache.Delete(key, keyAsPrefix);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyAsPrefix"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(CacheKey key, bool keyAsPrefix = false)
        {
            return Task.FromResult(this.Delete(key, keyAsPrefix));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockObject"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Lock(LockObject lockObject, TimeSpan expiry)
        {
            Throws.ArgumentNullException(lockObject, nameof(lockObject));
            if (this._memoryCache.Get(lockObject) != null)
            {
                return false;
            }
            return this._memoryCache.Set(lockObject, lockObject.Value, expiry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockObject"></param>
        /// <returns></returns>
        public bool UnLock(LockObject lockObject)
        {
            Throws.ArgumentNullException(lockObject, nameof(lockObject));
            return this._memoryCache.Delete(lockObject);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Increment(CacheKey key, long value = 1)
        {
            Throws.ArgumentNullException(key, nameof(key));

            var cacheValue = this._memoryCache.Get<long>(key);

            cacheValue = cacheValue + value;

            this._memoryCache.Set(key, cacheValue);

            return cacheValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Decrement(CacheKey key, long value = 1)
        {
            Throws.ArgumentNullException(key, nameof(key));

            var cacheValue = this._memoryCache.Get<long>(key);

            cacheValue = cacheValue - value;

            this._memoryCache.Set(key, cacheValue);

            return cacheValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="hashValue"></param>
        /// <returns></returns>
        public bool HashSet<TCache>(CacheKey key, string hashField, TCache hashValue)
        {
            Throws.ArgumentNullException(hashField, nameof(hashField));
            Throws.ArgumentNullException(hashValue, nameof(hashValue));
            var hashTable = GetHashTable(key);
            hashTable.Add(hashField, hashValue);
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashItems"></param>
        /// <returns></returns>
        public bool HashSet<TCache>(CacheKey key, IReadOnlyDictionary<string, TCache> hashItems)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashItems, nameof(hashItems));
            var hashTable = GetHashTable(key);
            foreach (var hashItem in hashItems)
            {
                hashTable.Add(hashItem.Key, hashItem.Value);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <param name="hashValue"></param>
        /// <returns></returns>
        public Task<bool> HashSetAsync<TCache>(CacheKey key, string hashField, TCache hashValue)
        {
            return Task.FromResult(this.HashSet(key, hashField, hashValue));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashItems"></param>
        /// <returns></returns>
        public Task<bool> HashSetAsync<TCache>(CacheKey key, IReadOnlyDictionary<string, TCache> hashItems)
        {
            return Task.FromResult(this.HashSet(key, hashItems));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<string> HashGetKeys(CacheKey key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            var hashTable = GetHashTable(key);
            return hashTable.Keys.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<IList<string>> HashGetKeysAsync(CacheKey key)
        {
            return Task.FromResult(this.HashGetKeys(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public TCache HashGetValue<TCache>(CacheKey key, string hashField)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashField, nameof(hashField));
            var hashTable = GetHashTable(key);
            if (hashTable.ContainsKey(hashField))
            {
                return (TCache)hashTable[hashField];
            }
            return default(TCache);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public Task<TCache> HashGetValueAsync<TCache>(CacheKey key, string hashField)
        {
            return Task.FromResult(this.HashGetValue<TCache>(key, hashField));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<TCache> HashGetValues<TCache>(CacheKey key)
        {
            Throws.ArgumentNullException(key, nameof(key));
            var hashTable = GetHashTable(key);
            return hashTable
                .Values
                .Select(value => (TCache)value)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<IList<TCache>> HashGetValuesAsync<TCache>(CacheKey key)
        {
            return Task.FromResult(this.HashGetValues<TCache>(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public bool HashDelete(CacheKey key, params string[] hashFields)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashFields, nameof(hashFields));
            var hashTable = GetHashTable(key);
            foreach (var hashField in hashFields)
            {
                hashTable.Remove(hashField);
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public Task<bool> HashDeleteAsync(CacheKey key, params string[] hashFields)
        {
            return Task.FromResult(this.HashDelete(key, hashFields));
        }

        private Dictionary<string, object> GetHashTable(CacheKey key)
        {
            var hashTable = this._memoryCache.Get<Dictionary<string, object>>(key);
            if (hashTable == null)
            {
                hashTable = new Dictionary<string, object>();
                this._memoryCache.Set(key, hashTable);
            }
            return hashTable;
        }
    }
}
