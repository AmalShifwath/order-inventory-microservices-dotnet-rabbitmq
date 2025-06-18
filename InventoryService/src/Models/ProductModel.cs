namespace InventoryService.Models;

public class ProductModel
{
    public Guid ProductModelId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}