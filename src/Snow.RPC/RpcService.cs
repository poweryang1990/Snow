using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC
{
    /// <summary>
    /// 服务对象
    /// </summary>
    public class RpcService
    {
        /// <summary>
        /// 服务协议
        /// </summary>
        public ServiceProtocol Protocol { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Url  Path
        /// </summary>
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var protocol = Protocol == ServiceProtocol.Tcp ? "tcp" : "http";
            return $"{protocol}://{Host}:{Port}/{Path}";
        }
    }
    /// <summary>
    /// 服务协议
    /// </summary>
    public enum ServiceProtocol
    {
        /// <summary>
        /// Http
        /// </summary>
        Http,
        /// <summary>
        /// Tcp
        /// </summary>
        Tcp
    }
}
