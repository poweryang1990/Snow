using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC.Client.Discovery
{
    /// <summary>
    /// 服务发现
    /// </summary>
    public interface IDiscoveryService
    {
        /// <summary>
        /// 获取服务地址
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        RpcService GetRpcService(string  serviceName);
    }
}
