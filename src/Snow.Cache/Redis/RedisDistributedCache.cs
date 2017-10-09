using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Snow.Cache.Extensions;

namespace Snow.Cache.Redis
{
    /// <summary>
    /// Redis实现的分布式缓存
    /// </summary>
    public partial class RedisDistributedCache : IDistributedCache
    {
        private readonly SemaphoreSlim _redisLock = new SemaphoreSlim(initialCount: 1, maxCount: 1);
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
                 .JsonToObject<TCache>();
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

            return redisValue.JsonToObject<TCache>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyAsPrefix"></param>
        /// <returns></returns>
        public bool Delete(CacheKey key, bool keyAsPrefix)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Connect();
            if (keyAsPrefix == true)
            {
                foreach (var endPoint in _redis.GetEndPoints())
                {
                    var server = _redis.GetServer(endPoint);
                    var keys = server.Keys(pattern: $"{key}*").ToArray();
                    this._db.KeyDelete(keys);
                }
                return true;
            }
            else
            {
                return this._db.KeyDelete(key.ToRedisKey());
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="keyAsPrefix"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(CacheKey key, bool keyAsPrefix)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            await ConnectAsync();
            if (keyAsPrefix == true)
            {
                foreach (var endPoint in _redis.GetEndPoints())
                {
                    var server = _redis.GetServer(endPoint);
                    var keys = server.Keys(pattern: $"{key}*").ToArray();
                    await this._db.KeyDeleteAsync(keys);
                }
                return true;
            }
            {
                return await this._db.KeyDeleteAsync(key.ToRedisKey());
            }
        }

        #region Connect

        private void Connect()
        {
            if (this._redis != null)
            {
                return;
            }

            this._redisLock.Wait();
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
                this._redisLock.Release();
            }
        }

        private async Task ConnectAsync()
        {
            if (this._redis != null)
            {
                return;
            }

            await this._redisLock.WaitAsync();
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
                this._redisLock.Release();
            }
        }

        #endregion Connect
    }
}