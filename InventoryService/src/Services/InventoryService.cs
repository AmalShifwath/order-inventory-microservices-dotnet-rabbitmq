using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Services
{
    public class InventoryService
    {
        private readonly List<InventoryModel> _inventory;

        public InventoryService()
        {
            _inventory = new List<InventoryModel>();
        }

        public void AddNewItem(InventoryModel item)
        {
            _inventory.Add(item);
        }

        public InventoryModel GetInventoryDetails(int itemId)
        {
            return _inventory.FirstOrDefault(i => i.ItemId == itemId);
        }

        public IEnumerable<InventoryModel> GetAllInventoryItems()
        {
            return _inventory;
        }

        public void UpdateInventoryItem(InventoryModel updatedItem)
        {
            var existingItem = _inventory.FirstOrDefault(i => i.ItemId == updatedItem.ItemId);
            if (existingItem != null)
            {
                existingItem.Name = updatedItem.Name;
                existingItem.Quantity = updatedItem.Quantity;
                existingItem.Price = updatedItem.Price;
            }
        }
    }
}