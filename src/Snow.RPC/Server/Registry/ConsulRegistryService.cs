using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC.Server.Registry
{
    /// <summary>
    /// Consul服务注册
    /// </summary>
    public class ConsulRegistryService : BaseConsulService,IRegistryService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryAddress"></param>
        public ConsulRegistryService(ServiceRegistryAddress registryAddress) : base(registryAddress)
        {
        }
        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="rpcService"></param>
        public void Deregister(RpcService rpcService)
        {
            using (var consul = BuildConsul())
            {
                consul.Agent.ServiceDeregister(GetServiceId(rpcService)).ConfigureAwait(false).GetAwaiter().GetResult();
            };
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="rpcService"></param>
        public void Register(RpcService rpcService)
        {
            using (var consul = BuildConsul())
            {
                var serviceRegistration = new AgentServiceRegistration()
                {
                    ID = GetServiceId(rpcService),
                    Name = rpcService.Name,
                    Address = rpcService.Host,
                    Port = rpcService.Port,
                    Tags = new []{ rpcService.Protocol.ToString()},//服务协议添加为标签
                    Check= new AgentServiceCheck
                    {
                         TCP=$"{rpcService.Host}:{rpcService.Port}",
                         Interval=TimeSpan.FromSeconds(5),
                         Timeout= TimeSpan.FromSeconds(2)
                    }
                };
                var result = consul.Agent.ServiceRegister(serviceRegistration).ConfigureAwait(false).GetAwaiter().GetResult();
            };
  
        }
        private string GetServiceId(RpcService rpcService)
        {
            return $"{rpcService.Name}_{rpcService.Host}_{rpcService.Port}";
        }
    }
}
