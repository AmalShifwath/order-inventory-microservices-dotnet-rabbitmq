namespace OrderService.Models;

public class CustomerModel
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public Guid ProductId { get; set; }
    public string Email { get; set; }
    
    // Navigation property to link orders to the customer
    public List<OrderModel> itemsInCart { get; set; } = new List<OrderModel>();
}