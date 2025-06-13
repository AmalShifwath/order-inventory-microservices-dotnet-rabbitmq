using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
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