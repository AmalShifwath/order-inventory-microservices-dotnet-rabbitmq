using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using OrderService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace OrderService.Messaing;

public class RabbitMqUtil : IRabbitMqUtil
{
    private readonly ConnectionFactory _factory;
    private readonly IServiceScopeFactory _scopeFactory;


    public RabbitMqUtil(IServiceScopeFactory scopeFactory)
    {
        _factory = new ConnectionFactory() { HostName = "localhost" };
        _scopeFactory = scopeFactory;
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

        Console.WriteLine($"--> Published message to exchange [trigger]: {eventData}");

        await Task.CompletedTask;
    }

    public async Task consumeMessageQueue(string routingKey)
    {
        Console.WriteLine($"--> consumer called");
        var connection = _factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "product_exchange", type: ExchangeType.Direct);

        var queueName = "product_update_queue";
        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        channel.QueueBind(queue: queueName, exchange: "product_exchange", routingKey: "inventory.product");

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"--> Received message: {message}");

            await HandleReceivedProductMessage(message);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

        Console.WriteLine("--> Consumer is listening on queue: product_update_queue");

        await Task.Delay(Timeout.Infinite);

        Console.WriteLine($"--> consumer stopped listening");
    }

    private async Task HandleReceivedProductMessage(string message)
    {
        var product = JsonSerializer.Deserialize<ProductModel>(message);
        if (product == null) return;

        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();

        var existing = await dbContext.Products.FindAsync(product.ProductModelId);
        if (existing == null)
        {
            await dbContext.Products.AddAsync(product);
        }
        else
        {
            existing.Quantity = product.Quantity;
            dbContext.Products.Update(existing);
        }

        await dbContext.SaveChangesAsync();

        await Task.CompletedTask;
    }
}