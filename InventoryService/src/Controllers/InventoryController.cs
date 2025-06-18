using Microsoft.AspNetCore.Mvc;
using InventoryService.Models;
using InventoryService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using InventoryService.Messaing;
using System.Text.Json;


namespace InventoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryServices _inventoryService;
        private readonly ProductDbContext _context;
        private readonly IRabbitMqUtil _rabbitUtil;

        public InventoryController(ProductDbContext context, InventoryServices inventoryService, IRabbitMqUtil rabbitUtil)
        {
            _context = context;
            _inventoryService = inventoryService;
            _rabbitUtil = rabbitUtil;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddNewItem([FromBody] ProductModel product)
        {
            if (product == null)
            {
                return BadRequest("Invalid product data.");
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // var productString = JsonSerializer.Serialize(new
            // {
            //     product.ProductModelId,
            //     product.Name,
            //     product.Quantity
            // });

            var productString = JsonSerializer.Serialize(product);

            await _rabbitUtil.publishMessageQueue("inventory.product", productString);

            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductModelId }, product);
        }

        [HttpGet]
        [Route("/product-{id}")]
        public async Task<ActionResult<ProductModel>> GetProductById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet]
        [Route("/allproducts")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAllProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateInventory([FromBody] UpdateInventoryDto updateInfo)
        {

            if (updateInfo == null)
                return BadRequest("Invalid body.");
            
            var targetProduct = await _context.Products.FindAsync(updateInfo.productId);                 
            
            if (targetProduct == null)
                return NotFound("Invalid/No product data.");
            
            int updatedCount, currentCount = targetProduct.Quantity;
            if (updateInfo.deficit)
            {
                updatedCount = currentCount - updateInfo.count;
            }
            else
            {
                updatedCount = currentCount + updateInfo.count;
            }

            if (updatedCount != currentCount)
            {
                targetProduct.Quantity = updatedCount;
                await _context.SaveChangesAsync();
            }
            
            return Ok(targetProduct);

        }


        // [HttpPut("update/{id}")]
        // public IActionResult UpdateInventory(int id, [FromBody] InventoryModel inventoryModel)
        // {
        //     if (inventoryModel == null || inventoryModel.ItemId != id)
        //     {
        //         return BadRequest("Invalid inventory data.");
        //     }

        //     var updatedItem = _inventoryService.UpdateInventory(inventoryModel);
        //     if (updatedItem == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(updatedItem);
        // }
    }
}