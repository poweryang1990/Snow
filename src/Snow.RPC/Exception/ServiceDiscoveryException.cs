using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.RPC
{
    /// <summary>
    /// 服务发现异常
    /// </summary>
    public class ServiceDiscoveryException: Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceDiscoveryException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ServiceDiscoveryException(string message)
            : base(message)
        { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ServiceDiscoveryException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
