namespace InventoryService.Messaing;

public interface IRabbitMqUtil
{
    Task publishMessageQueue(string routingKey, string eventData);
}