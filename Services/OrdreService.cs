using Repository;
using Entity    ;
using System.Threading.Tasks;
using AutoMapper;
using DTOs;

namespace Services
{
    public class OrderService : IOrderService
    {
        IOrderrRepository _orderRepository;
        IMapper _imapper;


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
            Order ord = _imapper.Map<Order>(order);
            Order addedOrderDto = await _orderRepository.AddOrder(ord);
            OrderDto orderDto = _imapper.Map<OrderDto>(addedOrderDto);
            return orderDto;
        }


    }
}
