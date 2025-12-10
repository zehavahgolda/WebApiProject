using Repository;
using Entity    ;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderrRepository _orderRepository;


        public OrderService(IOrderrRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetOrderByid(int id)
        {
            return await _orderRepository.GetOrderById(id);
        }

        public async Task<Order> addOrder(Order order)
        {
            return await _orderRepository.AddOrder(order);
        }


    }
}
