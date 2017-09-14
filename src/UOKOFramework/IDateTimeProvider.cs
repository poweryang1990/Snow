using System;

namespace UOKOFramework
{
    /// <summary>
    /// DateTime提供者接口
    /// <para>为了方便测试，静态的属性在代码中不方便进行Mock，不利于测试。</para>
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// 获取当前的UTC时间
        /// </summary>
        DateTime UtcNow { get; }
    }
}
