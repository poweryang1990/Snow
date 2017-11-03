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
    public class RpcTcpClient
    {
        private readonly HproseTcpClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceRegistryAddress"></param>
        /// <param name="loadBalancer"></param>
        /// <param name="onError"></param>
        public RpcTcpClient(string serviceName, ServiceRegistryAddress serviceRegistryAddress, ILoadBalancer loadBalancer=null, Action<string, Exception> onError=null)
        {
            var consulDiscoveryService = new ConsulDiscoveryService(serviceRegistryAddress, loadBalancer);
            var service = consulDiscoveryService.GetRpcService(serviceName);
            if (service == null)
            {
                throw new ServiceDiscoveryException($"未发现可用的【{serviceName}】 服务");
            }
            _client = new HproseTcpClient(service.ToString());
            if (onError != null)
            {
               _client.OnError += (name,e)=>
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
        public RpcTcpClient(string serviceHost, int servicePort, Action<string, Exception> onError = null)
        {

            _client = new HproseTcpClient($"tcp://{serviceHost}:{servicePort}/");
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
        public RpcTcpClient SetTimeout(long timeout)
        {
            _client.Timeout = timeout;
            return this;
        }
        /// <summary>
        /// 设置客户端发送超时时间
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public RpcTcpClient SetSendTimeout(int timeout)
        {
            _client.SendTimeout = timeout;
            return this;
        }
        /// <summary>
        /// 设置客户端接收超时时间
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public RpcTcpClient SetReceiveTimeout(int timeout)
        {
            _client.ReceiveTimeout = timeout;
            return this;
        }
        /// <summary>
        /// 设置客户端超时时间
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public RpcTcpClient SetSendBufferSize(int size)
        {
            _client.SendBufferSize= size;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T UseService<T>()
        {    
            return _client.UseService<T>(typeof(T).FullName);
        }
        /// <summary>
        /// 方法别名前缀
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ns"></param>
        /// <returns></returns>
        public T UseService<T>(string ns)
        {
            return _client.UseService<T>(ns);
        }
    }
}
