using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hprose.Client;
using Snow.RPC.Client.Discovery;
namespace Snow.RPC.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class RpcHttpClient
    {
        private readonly string _serviceName;
        private readonly ServiceRegistryAddress _serviceRegistryAddress;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceRegistryAddress"></param>
        public RpcHttpClient(string serviceName, ServiceRegistryAddress serviceRegistryAddress)
        {
            _serviceName = serviceName;
            _serviceRegistryAddress = serviceRegistryAddress;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T UseService<T>()
        {
            var consulDiscoveryService = new ConsulDiscoveryService(_serviceRegistryAddress);
            var service = consulDiscoveryService.GetRpcService(_serviceName);
            if (service==null)
            {
                throw  new ServiceDiscoveryException($"未发现可用的【{_serviceName}】 服务");
            }
            var client = new HproseHttpClient($"http://{service.Host}:{service.Port}/");
            return client.UseService<T>();
        }
    }
}
