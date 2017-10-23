using System.Collections.Generic;

namespace Snow.RPC.Client.LoadBalancer
{
    /// <summary>
    /// 服务负载均衡--均使用单例模式
    /// </summary>
    public interface ILoadBalancer
    {
        /// <summary>
        /// 负载获取服务
        /// </summary>
        /// <param name="list">可用的服务列表</param>
        /// <returns></returns>
        RpcService GetService(IList<RpcService> list);
    }
}