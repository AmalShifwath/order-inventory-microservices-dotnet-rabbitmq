using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using Shared.Models;

namespace OrderService.Messaging
{
    public class RabbitMqSubscriber
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqSubscriber(RabbitMqConnection rabbitMqConnection)
        {
            _connection = rabbitMqConnection.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "order_events", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void ListenForOrderEvents(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var orderEvent = JsonConvert.DeserializeObject<OrderEventModel>(message);
                HandleOrderEvent(orderEvent);
            };

            _channel.BasicConsume(queue: "order_events", autoAck: true, consumer: consumer);

            while (!cancellationToken.IsCancellationRequested)
            {
                Thread.Sleep(100);
            }
        }

        private void HandleOrderEvent(OrderEventModel orderEvent)
        {
            // Handle the order event (e.g., update order status, notify other services, etc.)
            Console.WriteLine($"Received order event: {orderEvent.OrderId}, Status: {orderEvent.Status}");
        }
    }
}