using DTOs;
using Entity;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDto> addOrder(Order order);
        Task<OrderDto> GetOrderByid(int id);
        Task<IEnumerable<OrderDto>> GetAllOrders();
    }
}