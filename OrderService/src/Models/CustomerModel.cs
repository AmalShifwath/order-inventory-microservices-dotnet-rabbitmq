namespace OrderService.Models;

public class CustomerModel
{
    public int CustomerModelId { get; set; }
    public string Name { get; set; }
    public Guid ProductModelId { get; set; }
    public string Email { get; set; }
    public int itemsInCart { get; set; }
}