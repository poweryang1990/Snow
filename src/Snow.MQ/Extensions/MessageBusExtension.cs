using System;
using Snow.MQ;

// ReSharper disable CheckNamespace
namespace Snow.Extensions
{
    /// <summary>
    /// IMessageBus的扩展方法
    /// </summary>
    public static class MessageBusExtension
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="messageBus">消息总线</param>
        /// <param name="message">消息</param>
        public static void Send<TMessage>(this IMessageBus messageBus, TMessage message)
        {
            Throws.ArgumentNullException(messageBus, nameof(messageBus));
            var queue = typeof(TMessage).Name;
            messageBus.Send(queue, message);
        }

        /// <summary>
        /// 接收一个消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="messageBus">消息总线</param>
        /// <param name="callback">回调函数</param>
        public static void Receive<TMessage>(this IMessageBus messageBus, Func<TMessage, bool> callback)
        {
            Throws.ArgumentNullException(messageBus, nameof(messageBus));
            var queue = typeof(TMessage).Name;
            messageBus.Receive(queue, callback);
        }
    }
}
