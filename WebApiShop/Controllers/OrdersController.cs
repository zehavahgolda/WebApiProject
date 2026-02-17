using Microsoft.AspNetCore.Mvc;
using Services;
using Repository;
using Entity;
using DTOs;
using System.Threading.Tasks;

namespace WebApiShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        
        public async Task<IEnumerable<OrderDto>> Get()
        {
            return await _orderService.GetAllOrders();
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> Post([FromBody] Order order)
        {
            OrderDto _orderdto = await _orderService.addOrder(order);

            if (_orderdto == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { Id = order.OrderId}, order);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> Get(int id)
        {
            OrderDto order = await _orderService.GetOrderByid(id);
            if (order == null)
                return NoContent();
            return Ok(order);
        }
    }
}
