using Microsoft.EntityFrameworkCore;
using Entity;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        Store_329391924Context _store_329391924Context;

        public ProductRepository(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }
        public async Task<List<Product>> GetProducts(int? Product_Id,string? name,float? price,int? Catogery_Id,string? description)
        {
            return await _store_329391924Context.Products.ToListAsync();
        }

        
    }
}
