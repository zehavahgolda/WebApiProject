using Entity;

namespace Repository
{
    public interface IOrderrRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrdersByUserId(int userId);
        Task UpdateStatus(int id, string status);
    }
}