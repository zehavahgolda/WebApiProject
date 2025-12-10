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




    }
}
