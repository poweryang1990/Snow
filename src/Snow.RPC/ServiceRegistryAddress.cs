using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC
{
    /// <summary>
    /// 服务注册地址
    /// </summary>
    public class ServiceRegistryAddress
    {
        /// <summary>
        /// 服务注册地址
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 服务注册端口
        /// </summary>
        public int Port { get; set; }
    }
}
