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
        /// <param name="quene">队列的名称</param>
        /// <param name="message">消息</param>
        void Send<TMessage>(string quene, TMessage message);

        /// <summary>
        /// 广播一个消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="exchange">交换机的名称</param>
        /// <param name="message">消息</param>
        void Pubilsh<TMessage>(string exchange, TMessage message);
    }
}
