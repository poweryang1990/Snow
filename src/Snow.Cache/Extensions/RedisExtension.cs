﻿using System.Collections.Generic;
using System.Linq;
using Snow.Cache;
using StackExchange.Redis;
// ReSharper disable CheckNamespace

namespace Snow.Extensions
{
    internal static class RedisExtension
    {
        public static RedisKey ToRedisKey(this CacheKey cacheKey)
        {
            return cacheKey.ToString();
        }

        public static RedisValue ToRedisValue(this object value)
        {
            return value.ToJson();
        }

        public static TCache JsonToObject<TCache>(this RedisValue value)
        {
            return ((string)value).JsonToObject<TCache>();
        }

        public static HashEntry[] ToRedisHashEntrys<TCache>(this IEnumerable<KeyValuePair<string, TCache>> keyValues)
        {
            return keyValues.Select(keyValue =>
                new HashEntry(keyValue.Key, ToRedisValue(keyValue)))
            .ToArray();
        }
    }
}