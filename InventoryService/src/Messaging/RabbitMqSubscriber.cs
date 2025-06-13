using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InventoryService.Messaging
{
    public class RabbitMqSubscriber : IHostedService
    {
        private readonly ILogger<RabbitMqSubscriber> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName = "inventory_queue";

        public RabbitMqSubscriber(ILogger<RabbitMqSubscriber> logger, RabbitMqConnection rabbitMqConnection)
        {
            _logger = logger;
            _connection = rabbitMqConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"Received message: {message}");
                // Process the message here
            };
            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}