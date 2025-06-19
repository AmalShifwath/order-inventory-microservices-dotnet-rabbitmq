using RabbitMQ.Client;
using System.Text;

namespace InventoryService.Messaing;

public class RabbitMqUtil : IRabbitMqUtil
{
   private readonly ConnectionFactory _factory;

    public RabbitMqUtil()
    {
        _factory = new ConnectionFactory() { HostName = "localhost" };
    }

    public async Task publishMessageQueue(string routingKey, string eventData)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "product_exchange", type: ExchangeType.Direct);
        
        var body = Encoding.UTF8.GetBytes(eventData);

        channel.BasicPublish(
            exchange: "product_exchange",
            routingKey: routingKey,
            basicProperties: null,
            body: body
        );

        Console.WriteLine($"--> Published message to exchange with routing key: {routingKey} [trigger]: {eventData}");

        await Task.CompletedTask;
    }
}