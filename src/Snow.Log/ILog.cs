using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snow.Log
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// 是否启用Trace
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// 是否启用Debug
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 是否启用Info
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 是否启用Warn
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 是否启用Error
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 是否启用Fatal
        /// </summary>
        bool IsFatalEnabled { get; }




        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="message">Trace消息</param>
        void Trace(string message);

        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="message">Trace消息</param>
        /// <param name="exception">异常</param>
        void Trace(string message, Exception exception);

        /// <summary>
        /// Trace
        /// </summary>
        /// <param name="format">Trace消息的格式化字符串</param>
        /// <param name="args">格式化参数</param>
        void Trace(string format, params object[] args);


        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message">Debug消息</param>
        void Debug(string message);

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message">Debug消息</param>
        /// <param name="exception">异常</param>
        void Debug(string message, Exception exception);

        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="format">Debug消息的格式化字符串</param>
        /// <param name="args">格式化参数</param>
        void Debug(string format, params object[] args);



        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message">Info消息</param>
        void Info(string message);

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message">Info消息</param>
        /// <param name="exception">异常</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Info
        /// </summary>
        /// <param name="format">Info消息的格式化字符串</param>
        /// <param name="args">格式化参数</param>
        void Info(string format, params object[] args);




        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message">Warn消息</param>
        void Warn(string message);

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="message">Warn消息</param>
        /// <param name="exception">异常</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Warn
        /// </summary>
        /// <param name="format">Warn消息的格式化字符串</param>
        /// <param name="args">格式化参数</param>
        void Warn(string format, params object[] args);





        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">Error消息</param>
        void Error(string message);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message">Error消息</param>
        /// <param name="exception">异常</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Error
        /// </summary>
        /// <param name="format">Error消息的格式化字符串</param>
        /// <param name="args">格式化参数</param>
        void Error(string format, params object[] args);





        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="message">Fatal消息</param>
        void Fatal(string message);

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="message">Fatal消息</param>
        /// <param name="exception">异常</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        /// Fatal
        /// </summary>
        /// <param name="format">Fatal消息的格式化字符串</param>
        /// <param name="args">格式化参数</param>
        void Fatal(string format, params object[] args);

    }
}
