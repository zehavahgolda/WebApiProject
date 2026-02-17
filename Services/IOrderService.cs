using DTOs;
using Entity;

namespace Services
{
    public interface IOrderService
    {
        Task<OrderDto> addOrder(Order order);
        Task<OrderDto> GetOrderByid(int id);
        Task<IEnumerable<OrderDto>> GetAllOrders();
        Task<IEnumerable<OrderDto>> GetOrdersByUserId(int userId);
        Task UpdateStatus(int id, string status);
    }
}