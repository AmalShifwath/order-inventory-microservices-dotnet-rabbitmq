using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;
using Microsoft.EntityFrameworkCore; // for ToListAsync()

namespace OrderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly CustomerDbContext _context;

        public OrderController(OrderService orderService)
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
            await _context.SaveChanegesAsync();
        }
    
        [HttpGet]
        [Route("customers")] // Get all customers
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            return customers;
        }

        [HttpGet]
        [Route("orders")] // Get all products
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }



        [HttpPost]
        [Route("create")]
        public IActionResult CreateOrder([FromBody] OrderModel order)
        {
            var result = _orderService.PlaceOrder(order);
            return CreatedAtAction(nameof(GetOrder), new { id = result.OrderId }, result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetOrder(int id)
        {
            var order = _orderService.GetOrderDetails(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderModel order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var updatedOrder = _orderService.UpdateOrder(order);
            if (updatedOrder == null)
            {
                return NotFound();
            }
            return Ok(updatedOrder);
        }
    }
}