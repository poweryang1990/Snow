﻿using System.Linq;
using System.Reflection;
using Hprose.Common;
using Hprose.Server;
namespace Snow.RPC.Server
{
    /// <summary>
    /// 
    /// </summary>
    public static class HproseServiceExtentions
    {
        /// <summary>
        /// 注册所有公共方法
        /// </summary>
        /// <param name="server"></param>
        /// <param name="obj"></param>
        public static void RegisterService(this HproseService server, object obj)
        {
            if (obj == null)
                return;
            foreach (MethodInfo declaredMethod in obj.GetType().GetTypeInfo().DeclaredMethods)
            {
                if (declaredMethod.IsPublic)
                {
                    server.Add(declaredMethod, obj, declaredMethod.Name, HproseResultMode.Normal, false);
                }
                    
            }
        }
    }
}
