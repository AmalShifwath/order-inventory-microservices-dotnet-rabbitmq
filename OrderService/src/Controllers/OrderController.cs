using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;
using Microsoft.EntityFrameworkCore; // for ToListAsync()
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderServices _orderService;
        private readonly CustomerDbContext _context;

        public OrderController(OrderServices orderService)
        {
            _orderService = orderService;
        }

        public OrderController(CustomerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task PostCustomer(CustomerModel Customer)
        {
            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();
        }
    
        [HttpGet]
        [Route("customers")] // Get all customers
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

         [HttpGet]
        [Route("products")] // Get all products
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("orders")] // Get all products
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }



        // [HttpPost]
        // [Route("create")]
        // public IActionResult CreateOrder([FromBody] OrderModel order)
        // {
            
        // }

        // [HttpGet]
        // [Route("{id}")]
        // public IActionResult GetOrder(int id)
        // {
        //     var order = _orderService.GetOrderDetails(id);
        //     if (order == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(order);
        // }

        // [HttpPut]
        // [Route("{id}")]
        // public IActionResult UpdateOrder(int id, [FromBody] OrderModel order)
        // {
        //     if (id != order.OrderId)
        //     {
        //         return BadRequest();
        //     }

        //     var updatedOrder = _orderService.UpdateOrder(order);
        //     if (updatedOrder == null)
        //     {
        //         return NotFound();
        //     }
        //     return Ok(updatedOrder);
        // }
    }
}