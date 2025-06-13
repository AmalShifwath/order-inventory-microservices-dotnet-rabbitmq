using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace OrderService.Messaging
{
    public class RabbitMqPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqPublisher(RabbitMqConnection rabbitMqConnection)
        {
            _connection = rabbitMqConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "order_created",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public async Task PublishOrderCreatedAsync(object order)
        {
            var message = JsonSerializer.Serialize(order);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: "",
                                  routingKey: "order_created",
                                  basicProperties: properties,
                                  body: body);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}