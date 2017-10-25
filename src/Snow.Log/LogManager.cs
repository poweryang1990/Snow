using System;
using System.Reflection;
using Snow.Log._log4net;
using log4netLogManager = log4net.LogManager;

namespace Snow.Log
{
    /// <summary>
    /// 日志管理者
    /// </summary>
    public static class LogManager
    {
        /// <summary>
        /// 创建ILog
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILog CreateLog<T>()
        {
            return CreateLog(typeof(T));
        }

        /// <summary>
        /// 创建ILog
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILog CreateLog(Type type)
        {
            return new Log4(log4netLogManager.GetLogger(type));
        }

        /// <summary>
        /// 创建ILog
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILog CreateLog(string name)
        {
            return new Log4(log4netLogManager.GetLogger(Assembly.GetCallingAssembly(), name));
        }
    }
}
