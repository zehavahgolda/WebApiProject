using Microsoft.EntityFrameworkCore;
using Entity;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderrRepository : IOrderrRepository
    {
        Store_329391924Context _store_329391924Context;
        public OrderrRepository(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }
        public async Task<Order> GetOrderById(int id)
        {
            return await _store_329391924Context.Orders.FindAsync(id);
        }

        public async Task<Order> AddOrder(Order order)
        {
            await _store_329391924Context.Orders.AddAsync(order);
            await _store_329391924Context.SaveChangesAsync();
            return order;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _store_329391924Context.Orders.ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserId(int userId)
        {
            return await _store_329391924Context.Orders
                .Include(o => o.User)              
                .Include(o => o.OrdeItems)         
                    .ThenInclude(oi => oi.Product)  
                .Where(order => order.UserId == userId)
                .ToListAsync();
        }


        public async Task UpdateStatus(int id, string status)
        {
          
            var order = await _store_329391924Context.Orders.FindAsync(id);
            if (order != null)
            {
                order.OrderStatus = status;
                await _store_329391924Context.SaveChangesAsync();
            }
        }
    }
}
