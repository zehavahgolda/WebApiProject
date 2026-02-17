using Entity;

namespace Repository
{
    public interface IOrderrRepository
    {
        Task<Order> AddOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}