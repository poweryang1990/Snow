using System;
using log4netlog = log4net.ILog;
using log4netlogLevel = log4net.Core.Level;

namespace Snow.Log._log4net
{
    /// <summary>
    /// log4net 日志
    /// </summary>
    internal class Log4 : ILog
    {
        private static readonly Type ThisType = typeof(Log4);

        private readonly log4netlog _inner;

        public Log4(log4netlog log)
        {
            _inner = log;
        }


        public bool IsTraceEnabled => _inner.Logger.IsEnabledFor(log4netlogLevel.Trace);

        public bool IsDebugEnabled => _inner.IsDebugEnabled;

        public bool IsInfoEnabled => _inner.IsInfoEnabled;

        public bool IsWarnEnabled => _inner.IsWarnEnabled;

        public bool IsErrorEnabled => _inner.IsErrorEnabled;

        public bool IsFatalEnabled => _inner.IsFatalEnabled;


        public void Trace(string message)
        {
            _inner.Logger.Log(ThisType, log4netlogLevel.Trace, message, null);
        }

        public void Trace(string message, Exception exception)
        {
            _inner.Logger.Log(ThisType, log4netlogLevel.Trace, message, exception);
        }

        public void Trace(string format, params object[] args)
        {
            _inner.Logger.Log(ThisType, log4netlogLevel.Trace, string.Format(format, args), null);
        }

        public void Debug(string message)
        {
            _inner.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            _inner.Debug(message, exception);
        }

        public void Debug(string format, params object[] args)
        {
            _inner.DebugFormat(format, args);
        }

        public void Info(string message)
        {
            _inner.Info(message);
        }


        public void Info(string message, Exception exception)
        {
            _inner.Info(message, exception);
        }


        public void Info(string format, params object[] args)
        {
            _inner.InfoFormat(format, args);
        }


        public void Warn(string message)
        {
            _inner.Warn(message);
        }


        public void Warn(string message, Exception exception)
        {
            _inner.Warn(message, exception);
        }


        public void Warn(string format, params object[] args)
        {
            _inner.WarnFormat(format, args);
        }

        public void Error(string message)
        {
            _inner.Error(message);
        }


        public void Error(string message, Exception exception)
        {
            _inner.Error(message, exception);
        }


        public void Error(string format, params object[] args)
        {
            _inner.ErrorFormat(format, args);
        }

        public void Fatal(string message)
        {
            _inner.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            _inner.Fatal(message, exception);
        }

        public void Fatal(string format, params object[] args)
        {
            _inner.FatalFormat(format, args);
        }
    }
}