using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using UOKOFramework.Cache.Keys;
using UOKOFramework.Cache.Extensions;

namespace UOKOFramework.Cache.Redis
{
    /// <summary>
    /// Redis实现的分布式缓存
    /// </summary>
    public class RedisDistributedCache : IDistributedCache
    {
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
        private volatile IConnectionMultiplexer _redis;
        private readonly RedisOptions _options;
        private IDatabase _db;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisOptions"></param>
        public RedisDistributedCache(RedisOptions redisOptions)
        {
            Throws.ArgumentNullException(redisOptions, nameof(redisOptions));
            this._options = redisOptions;
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
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.Connect();

            return this._db.StringSet(key.ToRedisKey(), value.ToRedisValue(), expiry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<TCache>(CacheKey key, TCache value, TimeSpan? expiry = null)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            await this.ConnectAsync();

            return await this._db.StringSetAsync(key.ToRedisKey(), value.ToRedisValue(), expiry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TCache Get<TCache>(CacheKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            this.Connect();

            return this._db
                 .StringGet(key.ToRedisKey())
                 .ToObject<TCache>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCache"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<TCache> GetAsync<TCache>(CacheKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await this.ConnectAsync();

            var redisValue = await this._db
                .StringGetAsync(key.ToRedisKey());

            return redisValue.ToObject<TCache>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(CacheKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();

            return this._db.KeyDelete(key.ToRedisKey());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(CacheKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await ConnectAsync();

            return await this._db.KeyDeleteAsync(key.ToRedisKey());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        public bool RemoveByPrefix(CacheKey keyPrefix)
        {
            if (keyPrefix == null)
            {
                throw new ArgumentNullException(nameof(keyPrefix));
            }

            Connect();

            foreach (var endPoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endPoint);
                foreach (var key in server.Keys(pattern: $"{keyPrefix}*"))
                {
                    _db.KeyDelete(key);
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        public async Task<bool> RemoveByPrefixAsync(CacheKey keyPrefix)
        {
            if (keyPrefix == null)
            {
                throw new ArgumentNullException(nameof(keyPrefix));
            }

            await ConnectAsync();

            foreach (var endPoint in _redis.GetEndPoints())
            {
                var server = _redis.GetServer(endPoint);
                foreach (var key in server.Keys(pattern: $"{keyPrefix}*"))
                {
                    await _db.KeyDeleteAsync(key);
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockObject"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Lock(LockObject lockObject, TimeSpan expiry)
        {
            if (lockObject == null)
            {
                throw new ArgumentNullException(nameof(lockObject));
            }

            Connect();

            return this._db.LockTake(lockObject.ToRedisKey(), lockObject.Value, expiry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockObject"></param>
        /// <returns></returns>
        public bool UnLock(LockObject lockObject)
        {
            if (lockObject == null)
            {
                throw new ArgumentNullException(nameof(lockObject));
            }

            Connect();

            return this._db.LockRelease(lockObject.ToRedisKey(), lockObject.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Increment(CacheKey key, long value = 1)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();

            return this._db.StringIncrement(key.ToRedisKey(), value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public long Decrement(CacheKey key, long value = 1)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();

            return this._db.StringDecrement(key.ToRedisKey(), value);
        }

        #region Connect

        private void Connect()
        {
            if (this._redis != null)
            {
                return;
            }

            this._connectionLock.Wait();
            try
            {
                if (this._redis == null)
                {
                    this._redis = ConnectionMultiplexer.Connect(this._options.Configuration);
                    this._db = this._redis.GetDatabase();
                }
            }
            finally
            {
                this._connectionLock.Release();
            }
        }

        private async Task ConnectAsync()
        {
            if (this._redis != null)
            {
                return;
            }

            await this._connectionLock.WaitAsync();
            try
            {
                if (this._redis == null)
                {
                    this._redis = await ConnectionMultiplexer.ConnectAsync(this._options.Configuration);
                    this._db = this._redis.GetDatabase();
                }
            }
            finally
            {
                this._connectionLock.Release();
            }
        }

        #endregion
    }
}