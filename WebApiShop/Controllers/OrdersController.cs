using DTOs;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            _logger.LogInformation("Rating: Get all orders called.");
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<OrderDto>> Post([FromBody] Order order)
        {
            _logger.LogInformation($"Rating: Post order called for User {order.UserId}.");

            OrderDto _orderdto = await _orderService.addOrder(order);

            if (_orderdto == null)
            {
                _logger.LogWarning("Order creation failed - service returned null.");
                return BadRequest();
            }
            return CreatedAtAction(nameof(Get), new { id = _orderdto.OrderId }, _orderdto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            _logger.LogInformation($"Rating: Get order by ID {id} called.");
            OrderDto order = await _orderService.GetOrderByid(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetByUserId(int userId)
        {
            _logger.LogInformation($"Rating: Get orders by User ID {userId} called.");
            var orders = await _orderService.GetOrdersByUserId(userId);

            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            _logger.LogInformation($"Rating: Update status for order {id} to {status}.");
            await _orderService.UpdateStatus(id, status);
            return NoContent();
        }
    }
}