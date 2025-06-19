namespace OrderService.Messaing;

public interface IRabbitMqUtil
{
    Task publishMessageQueue(string routingKey, string eventData);

    Task consumeMessageQueue(string routingKey);
}