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

        private readonly HproseHttpClient _client;

        /// <summary>
        /// 从Consul注册中心去发现服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceRegistryAddress"></param>
        /// <param name="loadBalancer"></param>
        /// <param name="onError"></param>
        public RpcHttpClient(string serviceName, ServiceRegistryAddress serviceRegistryAddress, ILoadBalancer loadBalancer=null, Action<string, Exception> onError = null)
        {
            var consulDiscoveryService = new ConsulDiscoveryService(serviceRegistryAddress, loadBalancer);
            var service = consulDiscoveryService.GetRpcService(serviceName);
            if (service == null)
            {
                throw new ServiceDiscoveryException($"未发现可用的【{serviceName}】 服务");
            }
            _client = new HproseHttpClient(service.ToString());
            if (onError != null)
            {
                _client.OnError += (name, e) =>
                {
                    onError(name, e);
                };
            }
        }
        /// <summary>
        /// 直接使用服务地址访问
        /// </summary>
        /// <param name="serviceHost"></param>
        /// <param name="servicePort"></param>
        /// <param name="onError"></param>
        public RpcHttpClient(string serviceHost,int servicePort, Action<string, Exception> onError = null)
        {
           
            _client = new HproseHttpClient($"http://{serviceHost}:{servicePort}/");
            if (onError != null)
            {
                _client.OnError += (name, e) =>
                {
                    onError(name, e);
                };
            }
        }

        /// <summary>
        /// 设置客户端超时时间
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public RpcHttpClient SetTimeout(int timeout)
        {
            _client.Timeout = timeout;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T UseService<T>()
        {      
            return _client.UseService<T>();
        }
    }
}
