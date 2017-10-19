using System;
using RabbitMQ.Client;

namespace Snow.MQ
{
    /// <summary>
    /// RabbitMQ
    /// </summary>
    public class MessageQuene : IMessageQuene
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public MessageQuene(MessageQueneOptions options)
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
        public IModel CreateChannel()
        {
            if (this._connection == null)
            {
                this._connection = this._connectionFactory.CreateConnection();
            }
            var channel = this._connection.CreateModel();
            this._connection.AutoClose = true;
            return channel;
        }
    }
}