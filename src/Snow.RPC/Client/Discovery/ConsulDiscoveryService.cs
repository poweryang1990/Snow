using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snow.RPC.Client.LoadBalancer;

namespace Snow.RPC.Client.Discovery
{
    /// <summary>
    /// Consul服务发现
    /// </summary>
    public class ConsulDiscoveryService : BaseConsulService, IDiscoveryService
    {
        private readonly ILoadBalancer _loadBalancer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryAddress"></param>
        /// <param name="loadBalancer"></param>
        public ConsulDiscoveryService(ServiceRegistryAddress registryAddress, ILoadBalancer loadBalancer=null) : base(registryAddress)
        {
            _loadBalancer = loadBalancer??new RandomLoadBalancer();//默认采用随机算法
        }
        /// <summary>
        /// 根据服务名获取服务地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public RpcService GetRpcService(string serviceName)
        {
            using (var consul = BuildConsul())
            {
                //如果没有就直接随机Consul中取出的健康服务
                var discoveredServices = consul.Health.Service(serviceName,"",true).ConfigureAwait(false).GetAwaiter().GetResult().Response.Select(t => t.Service).ToList();//获取健康的服务
                var services = new List<RpcService>();
                foreach (var discoveredService in discoveredServices)
                {
                    var service = new RpcService()
                    {
                        Name = discoveredService.Service,
                        Host = discoveredService.Address,
                        Port = discoveredService.Port,
                        Protocol = ServiceProtocol.Http
                    };
                    if (discoveredService.Tags != null && discoveredService.Tags.Any(t => t.Equals(ServiceProtocol.Tcp.ToString())))
                    {
                        service.Protocol = ServiceProtocol.Tcp;
                    }
                    services.Add(service);
                }
                return _loadBalancer.GetService(services);
            };
        }
    }
}
