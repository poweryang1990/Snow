using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hprose.Server;

namespace Snow.RPC.Server
{

    /// <summary>
    /// RPC 基于Tcp的服务
    /// </summary>
    public class RPCTcpServer: HproseTcpListenerServer
    {
        /// <summary>
        /// 
        /// </summary>
        public ServiceProtocol Protocol => ServiceProtocol.Tcp;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public RPCTcpServer(string url) : base(url)
        {
           
        }
    }

}
