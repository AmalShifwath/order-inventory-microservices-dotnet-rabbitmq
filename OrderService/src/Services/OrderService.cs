namespace OrderService.Services;
public class OrderServices
{
    private readonly List<OrderModel> _orders;

    public OrderServices()
    {
        _orders = new List<OrderModel>();
    }

    public void PlaceOrder(OrderModel order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }

        _orders.Add(order);
        // Logic to publish order created event to RabbitMQ can be added here
    }

    public IEnumerable<OrderModel> GetAllOrders()
    {
        return _orders;
    }

}
