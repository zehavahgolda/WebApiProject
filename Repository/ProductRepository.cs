using Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        Store_329391924Context _store_329391924Context;

        public ProductRepository(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }
        
       public async Task<(List<Product> Items, int TotalCount)> GetProducts(string? name, int?[] categories, int? minPrice, int? maxPrice, string? description, int? position, int? skip)
        {
            var query = _store_329391924Context.Products.Where(product =>
                 (description == null ? true : product.Description.Contains(description))
                 && (minPrice == null ? true : product.Price >= minPrice)
                 && (maxPrice == null ? true : product.Price <= maxPrice)
                 && (name == null ? true : product.ProductName.Contains(name))
                 && (categories == null || categories.Length == 0 ? true : categories.Contains(product.CategoryId)));
            var totalCount = await query.CountAsync();
            var products = await query
                 .OrderBy(p => p.ProductName)
                 .Skip(((position ?? 1) - 1) * (skip ?? 8)) 
                 .Take(skip ?? 8)
                 .Include(p => p.Category)
                 .ToListAsync();
            return (products, totalCount);
        }


    }
}
