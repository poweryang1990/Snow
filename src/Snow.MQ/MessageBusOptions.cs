using System;

namespace Snow.MQ
{
    /// <summary>
    /// Message Bus配置信息
    /// </summary>
    public class MessageBusOptions
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 自动恢复连接。默认为true。
        /// </summary>
        public bool AutomaticRecoveryEnabled { get; set; } = true;

        /// <summary>
        /// 恢复连接重试事件间隔，单位秒。默认3秒。
        /// </summary>
        public TimeSpan NetworkRecoveryInterval { get; set; } = TimeSpan.FromSeconds(3);

    }
}
