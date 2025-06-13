using RabbitMQ.Client;
using System;

namespace Shared.Messaging
{
    public class RabbitMqConnection : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqConnection(string hostname, string username, string password)
        {
            var factory = new ConnectionFactory()
            {
                HostName = hostname,
                UserName = username,
                Password = password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public IModel CreateChannel()
        {
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}