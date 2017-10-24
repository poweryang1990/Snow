using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Snow.RPC.Client.LoadBalancer
{
    /// <summary>
    /// 负载均衡-轮询算法
    /// </summary>
    public class RoundRobinLoadBalancer : ILoadBalancer
    {
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private int _index;
        private static RoundRobinLoadBalancer uniqueInstance;
        private static readonly object locker = new object();
        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static RoundRobinLoadBalancer GetInstance()
        {
            if (uniqueInstance==null)
            {
                lock (locker)
                {
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new RoundRobinLoadBalancer();
                    }
                }
            }

            return uniqueInstance;
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
            _lock.Wait();
            try
            {
                if (_index >= list.Count)
                {
                    _index = 0;
                }
                var service = list[_index];
                _index++;

                return service;
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}