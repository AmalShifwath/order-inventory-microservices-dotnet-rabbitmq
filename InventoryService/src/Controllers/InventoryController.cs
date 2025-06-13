using Microsoft.AspNetCore.Mvc;
using InventoryService.Models;
using InventoryService.Services;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;

        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("add")]
        public IActionResult AddInventory([FromBody] InventoryModel inventoryModel)
        {
            if (inventoryModel == null)
            {
                return BadRequest("Invalid inventory data.");
            }

            _inventoryService.AddNewItem(inventoryModel);
            return CreatedAtAction(nameof(GetInventory), new { id = inventoryModel.ItemId }, inventoryModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetInventory(int id)
        {
            var inventoryItem = _inventoryService.GetInventoryDetails(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return Ok(inventoryItem);
        }

        [HttpPut("update/{id}")]
        public IActionResult UpdateInventory(int id, [FromBody] InventoryModel inventoryModel)
        {
            if (inventoryModel == null || inventoryModel.ItemId != id)
            {
                return BadRequest("Invalid inventory data.");
            }

            var updatedItem = _inventoryService.UpdateInventory(inventoryModel);
            if (updatedItem == null)
            {
                return NotFound();
            }

            return Ok(updatedItem);
        }
    }
}