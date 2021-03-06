﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hprose.IO;
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
        public ServiceProtocol Protocol => ServiceProtocol.Http;
        /// <summary>
        /// 
        /// </summary>
        public RpcHttpServer():base("http://127.0.0.1/")
        {
            this.Mode=HproseMode.FieldMode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public RpcHttpServer(string url) : base(url)
        {
            this.Mode = HproseMode.FieldMode;
        }
    }

}
