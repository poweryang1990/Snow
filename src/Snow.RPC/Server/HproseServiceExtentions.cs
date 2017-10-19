using System.Linq;
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
        /// <typeparam name="T"></typeparam>
        /// <param name="server"></param>
        /// <param name="implement"></param>
        public static void RegisterService<T>(this HproseService server, object implement) where T : class
        {
            var methods = typeof(T).GetMethods().Where(t => t.IsPublic).Select(t => t.Name).ToArray();
            server.Add(methods, implement);
        }
    }
}
