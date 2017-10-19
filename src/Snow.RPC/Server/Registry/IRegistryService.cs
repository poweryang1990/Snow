using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC.Server.Registry
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public interface IRegistryService
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="rpcService"></param>
        void Register(RpcService rpcService);
        /// <summary>
        /// 注销服务
        /// </summary>
        /// <param name="rpcService"></param>
        void Deregister(RpcService rpcService);
    }
}
