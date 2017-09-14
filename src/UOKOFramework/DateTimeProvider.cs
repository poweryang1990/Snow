using System;

namespace UOKOFramework
{
    /// <summary>
    /// DateTime提供者
    /// </summary>
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        public DateTime Now { get; private set; } = DateTime.Now;

        /// <summary>
        /// 获取当前的UTC时间
        /// </summary>
        public DateTime UtcNow { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// 设置当前时间
        /// </summary>
        /// <param name="datetime">指定的时间</param>
        public DateTimeProvider SetNow(DateTime datetime)
        {
            this.Now = datetime;
            return this;
        }

        /// <summary>
        /// 设置当前的UTC时间
        /// </summary>
        /// <param name="datetime">指定的时间</param>
        public DateTimeProvider SetUtcNow(DateTime datetime)
        {
            this.UtcNow = datetime;
            return this;
        }
    }
}