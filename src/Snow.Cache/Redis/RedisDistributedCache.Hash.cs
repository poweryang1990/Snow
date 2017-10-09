using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Snow.Extensions;

namespace Snow.Cache.Redis
{
    /// <summary>
    /// Redis实现的分布式缓存
    /// </summary>
    public partial class RedisDistributedCache
    {
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
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashField, nameof(hashField));
            Throws.ArgumentNullException(hashValue, nameof(hashValue));

            this.Connect();

            return this._db.HashSet(key.ToRedisKey(), hashField, hashValue.ToRedisValue());
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
            foreach (var hashItem in hashItems)
            {
                if (hashItem.Value == null)
                {
                    throw new ArgumentNullException($"key={hashItem.Key}的值为null。");
                }
            }

            this.Connect();

            this._db.HashSet(key.ToRedisKey(), hashItems.ToRedisHashEntrys());

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
        public async Task<bool> HashSetAsync<TCache>(CacheKey key, string hashField, TCache hashValue)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashField, nameof(hashField));
            Throws.ArgumentNullException(hashValue, nameof(hashValue));

            await this.ConnectAsync();

            return await this._db.HashSetAsync(key.ToRedisKey(), hashField, hashValue.ToRedisValue());
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashItems"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<TCache>(CacheKey key, IReadOnlyDictionary<string, TCache> hashItems)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashItems, nameof(hashItems));
            foreach (var hashItem in hashItems)
            {
                if (hashItem.Value == null)
                {
                    throw new ArgumentNullException($"key={hashItem.Key}的值为null。");
                }
            }

            await this.ConnectAsync();

            await this._db.HashSetAsync(key.ToRedisKey(), hashItems.ToRedisHashEntrys());

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<string> HashGetKeys(CacheKey key)
        {
            Throws.ArgumentNullException(key, nameof(key));

            this.Connect();

            var redisKeys = this._db.HashKeys(key.ToRedisKey());

            return redisKeys
                .Select(value => (string)value)
                .ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<string>> HashGetKeysAsync(CacheKey key)
        {
            Throws.ArgumentNullException(key, nameof(key));

            await this.ConnectAsync();

            var redisKeys = await this._db.HashKeysAsync(key.ToRedisKey());

            return redisKeys
                .Select(value => (string)value)
                .ToList();
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

            this.Connect();

            var redisValue = this._db.HashGet(key.ToRedisKey(), hashField);

            return redisValue.JsonToObject<TCache>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<TCache> HashGetValueAsync<TCache>(CacheKey key, string hashField)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashField, nameof(hashField));

            await this.ConnectAsync();

            var redisValue = await this._db.HashGetAsync(key.ToRedisKey(), hashField);

            return redisValue.JsonToObject<TCache>();
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

            this.Connect();

            var redisValues = this._db.HashValues(key.ToRedisKey());

            return redisValues
                .Select(value => value.JsonToObject<TCache>())
                .ToList();
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<IList<TCache>> HashGetValuesAsync<TCache>(CacheKey key)
        {
            Throws.ArgumentNullException(key, nameof(key));

            await this.ConnectAsync();

            var redisValues = await this._db.HashValuesAsync(key.ToRedisKey());

            return redisValues
                .Select(value => value.JsonToObject<TCache>())
                .ToList();
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

            this.Connect();

            var redisHashFields = hashFields
                .Select(item => (RedisValue)item)
                .ToArray();

            this._db.HashDelete(key.ToRedisKey(), redisHashFields);

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(CacheKey key, params string[] hashFields)
        {
            Throws.ArgumentNullException(key, nameof(key));
            Throws.ArgumentNullException(hashFields, nameof(hashFields));

            await this.ConnectAsync();

            var redisHashFields = hashFields
                .Select(item => (RedisValue)item)
                .ToArray();

            await this._db.HashDeleteAsync(key.ToRedisKey(), redisHashFields);

            return true;
        }
    }
}