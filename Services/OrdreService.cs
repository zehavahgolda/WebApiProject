using AutoMapper;
using DTOs;
using Entity;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderrRepository _orderRepository;
        private readonly IMapper _imapper;

        public OrderService(IOrderrRepository orderRepository, IMapper imapper)
        {
            _orderRepository = orderRepository;
            _imapper = imapper;
        }

        public async Task<OrderDto> GetOrderByid(int id)
        {
            Order order = await _orderRepository.GetOrderById(id);
            OrderDto orderDto = _imapper.Map<OrderDto>(order);
            return orderDto;
        }

        public async Task<OrderDto> addOrder(Order order)
        {
            Order addedOrder = await _orderRepository.AddOrder(order);
            OrderDto orderDto = _imapper.Map<OrderDto>(addedOrder);
            return orderDto;
        }


        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            IEnumerable<Order> orders = await _orderRepository.GetAllOrders();
            IEnumerable<OrderDto> ordersDto = _imapper.Map<IEnumerable<OrderDto>>(orders);
            return ordersDto;
        }
    }
}