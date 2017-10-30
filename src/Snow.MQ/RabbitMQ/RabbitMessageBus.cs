using RabbitMQ.Client;
using Snow.Extensions;

namespace Snow.MQ.RabbitMQ
{
    /// <summary>
    /// RabbitMQ
    /// </summary>
    public class RabbitMessageBus : IMessageBus
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public RabbitMessageBus(MessageBusOptions options)
        {
            Throws.ArgumentNullException(options, nameof(options));
            Throws.ArgumentNullException(options.HostName, nameof(options.HostName));
            Throws.ArgumentNullException(options.UserName, nameof(options.UserName));
            Throws.ArgumentNullException(options.Password, nameof(options.Password));

            this._connectionFactory = new ConnectionFactory
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Password = options.Password,
                AutomaticRecoveryEnabled = options.AutomaticRecoveryEnabled,
                NetworkRecoveryInterval = options.NetworkRecoveryInterval
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IModel CreateChannel()
        {
            if (this._connection == null)
            {
                this._connection = this._connectionFactory.CreateConnection();
            }
            var channel = this._connection.CreateModel();
            this._connection.AutoClose = true;
            return channel;
        }


        /// <summary>
        /// 发送一个消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="quene">队列的名称</param>
        /// <param name="message">消息</param>
        public void Send<TMessage>(string quene, TMessage message)
        {
            Throws.ArgumentNullException(quene, nameof(quene));
            Throws.ArgumentNullException(message, nameof(message));

            var channel = this.CreateChannel();

            //声明队列
            channel.QueueDeclare(quene, true, false, false, null);

            //设置属性
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            //获取消息的bytes
            var body = GetBytes(message);

            //发布消息
            channel.BasicPublish(string.Empty, quene, properties, body);
        }

        /// <summary>
        /// 广播一个消息
        /// </summary>
        /// <typeparam name="TMessage">消息的类型</typeparam>
        /// <param name="exchange">交换机的名称</param>
        /// <param name="message">消息</param>
        public void Pubilsh<TMessage>(string exchange, TMessage message)
        {
            Throws.ArgumentNullException(exchange, nameof(exchange));
            Throws.ArgumentNullException(message, nameof(message));

            var channel = this.CreateChannel();

            //声明交换机
            channel.ExchangeDeclare(exchange, ExchangeType.Fanout, true);

            //设置属性
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            //获取消息的bytes
            var body = GetBytes(message);

            //发布消息
            channel.BasicPublish(exchange, string.Empty, properties, body);
        }

        private static byte[] GetBytes(object message)
        {
            return message.ToJsonBytes();
        }
    }
}