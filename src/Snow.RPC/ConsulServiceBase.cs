using Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC
{
    /// <summary>
    /// Consul基础配置
    /// </summary>
    public abstract class ConsulServiceBase
    {
        private readonly ServiceRegistryAddress _registryAddress;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registryAddress"></param>
        public ConsulServiceBase(ServiceRegistryAddress registryAddress)
        {
            _registryAddress = registryAddress;
        }
        /// <summary>
        /// 构建ConsulClient
        /// </summary>
        /// <returns></returns>
        protected ConsulClient BuildConsul()
        {
            var config = new ConsulClientConfiguration();
            config.Address = new Uri($"http://{_registryAddress.Host}:{_registryAddress.Port}");
#pragma warning disable CS0618 // 类型或成员已过时
            return new ConsulClient(config);
#pragma warning restore CS0618 // 类型或成员已过时
        }
    }
}
