using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace InventoryService.Messaging
{
    public class RabbitMqPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqPublisher(RabbitMqConnection rabbitMqConnection)
        {
            _connection = rabbitMqConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "inventory_queue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public async Task PublishInventoryEventAsync(object inventoryEvent)
        {
            var message = JsonSerializer.Serialize(inventoryEvent);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: "inventory_queue",
                                  basicProperties: null,
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