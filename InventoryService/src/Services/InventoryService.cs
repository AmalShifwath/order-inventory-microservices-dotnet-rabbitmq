using InventoryService.Models;

namespace InventoryService.Services;

public class InventoryServices
{
    private readonly List<ProductModel> _inventory;

    public InventoryServices()
    {
        _inventory = new List<ProductModel>();
    }

    public void AddNewItem(ProductModel item)
    {
        _inventory.Add(item);
    }
    public IEnumerable<ProductModel> GetAllInventoryItems()
    {
        return _inventory;
    }

    // public void UpdateInventoryItem(InventoryModel updatedItem)
    // {
    //     var existingItem = _inventory.FirstOrDefault(i => i.ItemId == updatedItem.ItemId);
    //     if (existingItem != null)
    //     {
    //         existingItem.Name = updatedItem.Name;
    //         existingItem.Quantity = updatedItem.Quantity;
    //         existingItem.Price = updatedItem.Price;
    //     }
    // }
}