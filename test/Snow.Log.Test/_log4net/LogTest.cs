using System;
using log4net.Core;
using Moq;
using Xunit;

namespace Snow.Log.Test._log4net
{
    public class LogTest
    {
        private static Mock<ILogger> MockedLog4ILogger()
        {
            return new Mock<ILogger>(MockBehavior.Loose);
        }

        private static Mock<log4net.ILog> MockLog4ILog(ILogger logger = null)
        {
            var mockedLog4ILog = new Mock<log4net.ILog>(MockBehavior.Loose);

            if (logger != null)
            {
                mockedLog4ILog.SetupGet(_ => _.Logger).Returns(logger);
            }

            return mockedLog4ILog;
        }

        private static ILog BuildILog(log4net.ILog log)
        {
            return new Snow.Log._log4net.Log4(log);
        }

        [Fact]
        public void Trace_Test()
        {
            var mockedLog4ILogger = MockedLog4ILogger();
            var mockedLog4ILog = MockLog4ILog(mockedLog4ILogger.Object);
            ILog log = BuildILog(mockedLog4ILog.Object);
            var logType = log.GetType();
            var traceLevel = Level.Trace;
            var argumentNullException = new ArgumentNullException();

            log.Trace("abc");
            log.Trace("abc", argumentNullException);
            log.Trace("{0}:abc", 123);

            mockedLog4ILogger.Verify(_ => _.Log(logType, traceLevel, "abc", null), Times.Once());
            mockedLog4ILogger.Verify(_ => _.Log(logType, traceLevel, "abc", argumentNullException), Times.Once());
            mockedLog4ILogger.Verify(_ => _.Log(logType, traceLevel, "123:abc", null), Times.Once());
        }

        [Fact]
        public void Debug_Test()
        {
            var mockedLog4ILog = MockLog4ILog();
            ILog log = BuildILog(mockedLog4ILog.Object);
            var argumentNullException = new ArgumentNullException();

            log.Debug("abc");
            log.Debug("abc", argumentNullException);
            log.Debug("{0}:abc", 123);

            mockedLog4ILog.Verify(_ => _.Debug("abc"), Times.Once());
            mockedLog4ILog.Verify(_ => _.Debug("abc", argumentNullException), Times.Once());
            mockedLog4ILog.Verify(_ => _.DebugFormat("{0}:abc", new object[] { 123 }), Times.Once());
        }

        [Fact]
        public void Info_Test()
        {
            var mockedLog4ILog = MockLog4ILog();
            ILog log = BuildILog(mockedLog4ILog.Object);
            var argumentNullException = new ArgumentNullException();

            log.Info("abc");
            log.Info("abc", argumentNullException);
            log.Info("{0}:abc", 123);

            mockedLog4ILog.Verify(_ => _.Info("abc"), Times.Once());
            mockedLog4ILog.Verify(_ => _.Info("abc", argumentNullException), Times.Once());
            mockedLog4ILog.Verify(_ => _.InfoFormat("{0}:abc", new object[] { 123 }), Times.Once());
        }

        [Fact]
        public void Warn_Test()
        {
            var mockedLog4ILog = MockLog4ILog();
            ILog log = BuildILog(mockedLog4ILog.Object);
            var argumentNullException = new ArgumentNullException();

            log.Warn("abc");
            log.Warn("abc", argumentNullException);
            log.Warn("{0}:abc", 123);

            mockedLog4ILog.Verify(_ => _.Warn("abc"), Times.Once());
            mockedLog4ILog.Verify(_ => _.Warn("abc", argumentNullException), Times.Once());
            mockedLog4ILog.Verify(_ => _.WarnFormat("{0}:abc", new object[] { 123 }), Times.Once());
        }

        [Fact]
        public void Error_Test()
        {
            var mockedLog4ILog = MockLog4ILog();
            ILog log = BuildILog(mockedLog4ILog.Object);
            var argumentNullException = new ArgumentNullException();

            log.Error("abc");
            log.Error("abc", argumentNullException);
            log.Error("{0}:abc", 123);

            mockedLog4ILog.Verify(_ => _.Error("abc"), Times.Once());
            mockedLog4ILog.Verify(_ => _.Error("abc", argumentNullException), Times.Once());
            mockedLog4ILog.Verify(_ => _.ErrorFormat("{0}:abc", new object[] { 123 }), Times.Once());
        }

        [Fact]
        public void Fatal_Test()
        {
            var mockedLog4ILog = MockLog4ILog();
            ILog log = BuildILog(mockedLog4ILog.Object);
            var argumentNullException = new ArgumentNullException();

            log.Fatal("abc");
            log.Fatal("abc", argumentNullException);
            log.Fatal("{0}:abc", 123);

            mockedLog4ILog.Verify(_ => _.Fatal("abc"), Times.Once());
            mockedLog4ILog.Verify(_ => _.Fatal("abc", argumentNullException), Times.Once());
            mockedLog4ILog.Verify(_ => _.FatalFormat("{0}:abc", new object[] { 123 }), Times.Once());
        }
    }
}
