using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hprose.Server;

namespace Snow.RPC.Server
{
    /// <summary>
    /// RPC 基于Http的服务
    /// </summary>
    public class RpcHttpServer: HproseHttpListenerServer
    {
        /// <summary>
        /// 
        /// </summary>
        public RpcHttpServer():base("http://127.0.0.1/")
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public RpcHttpServer(string url) : base(url)
        {

        }
    }

}
