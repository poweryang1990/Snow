using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hprose.Client;
using Snow.RPC.Client.Discovery;
using Snow.RPC.Client.LoadBalancer;

namespace Snow.RPC.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class RpcHttpClient
    {
        private readonly string _serviceName;
        private readonly ServiceRegistryAddress _serviceRegistryAddress;
        private readonly ILoadBalancer _loadBalancer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceRegistryAddress"></param>
        /// <param name="loadBalancer"></param>
        public RpcHttpClient(string serviceName, ServiceRegistryAddress serviceRegistryAddress, ILoadBalancer loadBalancer)
        {
            _serviceName = serviceName;
            _serviceRegistryAddress = serviceRegistryAddress;
            _loadBalancer=loadBalancer;

    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T UseService<T>()
        {
            var consulDiscoveryService = new ConsulDiscoveryService(_serviceRegistryAddress, _loadBalancer);
            var service = consulDiscoveryService.GetRpcService(_serviceName);
            if (service==null)
            {
                throw  new ServiceDiscoveryException($"未发现可用的【{_serviceName}】 服务");
            }
            var client = new HproseHttpClient(service.ToString());
            return client.UseService<T>();
        }
    }
}
