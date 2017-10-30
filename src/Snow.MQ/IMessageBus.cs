using System;

namespace Snow.MQ
{
    /// <summary>
    /// 消息总线接口
    /// </summary>
    public interface IMessageBus
    {
        /// <summary>
        /// 发送一个消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="queue">队列的名称</param>
        /// <param name="message">消息</param>
        void Send<TMessage>(string queue, TMessage message);

        /// <summary>
        /// 接收一个消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="queue">队列的名称</param>
        /// <param name="callback">回调函数</param>
        void Receive<TMessage>(string queue, Func<TMessage, bool> callback);

        /// <summary>
        /// 发布一个消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="exchange">交换机的名称</param>
        /// <param name="message">消息</param>
        void Pubilsh<TMessage>(string exchange, TMessage message);

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="exchange">交换机的名称</param>
        /// <param name="callback">回调函数</param>
        void Subscribe<TMessage>(string exchange, Func<TMessage, bool> callback);
    }
}
