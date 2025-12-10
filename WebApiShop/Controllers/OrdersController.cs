using Microsoft.AspNetCore.Mvc;
using Services;
using Repository;
using Entity;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
    
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] Order order)
        {
            Order _order = await _orderService.addOrder(order);

            if (_order == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { Id = order.OrderId}, order);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            Order order = await _orderService.GetOrderByid(id);
            if (order == null)
                return NoContent();
            return Ok(order);
        }
    }
}
