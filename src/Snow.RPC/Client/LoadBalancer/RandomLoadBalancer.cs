using System;
using System.Collections.Generic;
using System.Linq;

namespace Snow.RPC.Client.LoadBalancer
{
    /// <summary>
    /// 负载均衡随机算法
    /// </summary>
    public class RandomLoadBalancer : ILoadBalancer
    {
        private readonly Random _random;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="seed">种子</param>
        public RandomLoadBalancer( int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public RpcService GetService(IList<RpcService> list)
        {
            if (list == null || !list.Any())
            {
                return null;
            }
            return list.ElementAt(_random.Next(list.Count));
        }
    }
}