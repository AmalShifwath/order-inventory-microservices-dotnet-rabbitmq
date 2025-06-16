using System;
using System.Collections.Generic;
using System.Linq;

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

    // public OrderModel GetOrderDetails(Guid orderId)
    // {
    //     return _orders.FirstOrDefault(o => o.OrderId == orderId);
    // }

    public IEnumerable<OrderModel> GetAllOrders()
    {
        return _orders;
    }

    // public void UpdateOrder(OrderModel updatedOrder)
    // {
    //     var existingOrder = GetOrderDetails(updatedOrder.OrderId);
    //     if (existingOrder != null)
    //     {
    //         existingOrder.ProductId = updatedOrder.ProductId;
    //         existingOrder.Quantity = updatedOrder.Quantity;
    //         existingOrder.Status = updatedOrder.Status;
    //         // Logic to publish order updated event to RabbitMQ can be added here
    //     }
    // }
}
