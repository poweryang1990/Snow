using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC.Client.Discovery
{
    /// <summary>
    /// Consul服务发现
    /// </summary>
    public class ConsulDiscoveryService : BaseConsulService, IDiscoveryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryAddress"></param>
        public ConsulDiscoveryService(ServiceRegistryAddress registryAddress) : base(registryAddress)
        {
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
                var discoveredService = GetRandomService(discoveredServices);
                if (discoveredService!=null)
                {
                    var  service=new RpcService()
                    {
                      Name = discoveredService.Service,
                      Host = discoveredService.Address,
                      Port = discoveredService.Port,
                      Protocol = ServiceProtocol.Http
                    };
                    if (discoveredService.Tags!=null&&discoveredService.Tags.Any(t=>t.Equals(ServiceProtocol.Tcp.ToString())))
                    {
                        service.Protocol = ServiceProtocol.Tcp;
                    }

                    return service;
                }
                return null;
            };
        }


        /// <summary>
        /// 获取随机的服务
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private AgentService GetRandomService(IList<AgentService> list)
        {
            if (list == null || !list.Any())
            {
                return null;
            }
            Random rnd = new Random();
            return list.ElementAt(rnd.Next(list.Count));
        }
    }
}
