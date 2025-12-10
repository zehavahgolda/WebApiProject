using Entity;

namespace Services
{
    public interface IOrderService
    {
        Task<Order> addOrder(Order order);
        Task<Order> GetOrderByid(int id);
    }
}