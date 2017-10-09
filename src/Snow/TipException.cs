using System;

namespace Snow
{
    /// <summary>
    /// 提示异常
    /// </summary>
    [Serializable]
    public class TipException : Exception
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public object Tip { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="tip">提示信息</param>
        public TipException(string message, object tip = null)
            : base(message)
        {
            this.Tip = tip;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        /// <param name="tip">提示信息</param>
        public TipException(string message, Exception innerException, object tip = null)
            : base(message, innerException)
        {
            this.Tip = tip;
        }
    }
}
