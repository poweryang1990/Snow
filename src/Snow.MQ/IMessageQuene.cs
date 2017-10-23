using RabbitMQ.Client;

namespace Snow.MQ
{
    /// <summary>
    /// 消息队列接口
    /// </summary>
    public interface IMessageQuene
    {
        /// <summary>
        /// 创建通道
        /// </summary>
        /// <returns>RabbitMQ</returns>
        IModel CreateChannel();
    }
}
