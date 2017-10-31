using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Snow.Extensions;
using Snow.Log;

namespace Snow.MQ.RabbitMQ
{
    /// <summary>
    /// RabbitMQ
    /// </summary>
    public class RabbitMessageBus : IMessageBus
    {
        private static readonly ILog Log = LogManager.CreateLog<RabbitMessageBus>();
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
        /// <param name="queue">队列的名称</param>
        /// <param name="message">消息</param>
        public void Send<TMessage>(string queue, TMessage message)
        {
            Throws.ArgumentNullException(queue, nameof(queue));
            Throws.ArgumentNullException(message, nameof(message));

            var channel = this.CreateChannel();

            //声明队列
            channel.QueueDeclare(queue, true, false, false, null);

            //设置属性
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            //获取消息的bytes
            var body = GetBytes(message);

            //发布消息
            channel.BasicPublish(string.Empty, queue, properties, body);
        }


        /// <summary>
        /// 接收一个消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="queue"></param>
        /// <param name="callback"></param>
        public void Receive<TMessage>(string queue, Func<TMessage, bool> callback)
        {
            Throws.ArgumentNullException(queue, nameof(queue));
            Throws.ArgumentNullException(callback, nameof(callback));
            var channel = this.CreateChannel();

            //声明队列
            channel.QueueDeclare(queue, true, false, false, null);
            //每次取一条消息
            channel.BasicQos(0, 1, false);

            Received(channel, queue, callback);
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

        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="TMessage"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="callback"></param>
        public void Subscribe<TMessage>(string exchange, Func<TMessage, bool> callback)
        {
            Throws.ArgumentNullException(exchange, nameof(exchange));
            Throws.ArgumentNullException(callback, nameof(callback));

            var channel = this.CreateChannel();

            channel.ExchangeDeclare(exchange, ExchangeType.Fanout, true);

            var queue = channel.QueueDeclare(durable: true).QueueName;

            channel.QueueBind(queue, exchange, "");

            Received(channel, queue, callback);
        }

        private void Received<TMessage>(IModel channel, string queue, Func<TMessage, bool> callback)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                try
                {
                    var message = e.Body.JsonBytesToObject<TMessage>();
                    var result = callback(message);
                    if (result == true)
                    {
                        channel.BasicAck(e.DeliveryTag, false);
                    }
                    else
                    {
                        channel.BasicReject(e.DeliveryTag, true);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"{queue}:error", ex);
                }
            };
            channel.BasicConsume(queue, false, consumer);
        }

        private static byte[] GetBytes(object message)
        {
            return message.ToJsonBytes();
        }
    }
}