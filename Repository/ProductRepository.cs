using Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System;
using Microsoft.AspNetCore.Hosting;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        Store_329391924Context _store_329391924Context;


        public ProductRepository(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }


        public async Task<Product> GetProductById(int id)
        {
            return await _store_329391924Context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<Product> AddProduct(Product product)
        {

            if (string.IsNullOrWhiteSpace(product.ProductName))
                throw new ArgumentException("Product name is required");

            if (product.CategoryId == null || product.CategoryId == 0)
                throw new ArgumentException("Category is required");

            product.Description = product.Description ?? "";
            product.ImgUrl = product.ImgUrl ?? "";
            product.Color = product.Color ?? "";
            product.Material = product.Material ?? "";

            await _store_329391924Context.Products.AddAsync(product);
            await _store_329391924Context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            product.ProductId = id;
            product.Description = product.Description ?? "";
            product.ImgUrl = product.ImgUrl ?? "";
            _store_329391924Context.Products.Update(product);
            await _store_329391924Context.SaveChangesAsync();
            return product;
        }


        public async Task DeleteProduct(int id)
        {
            var product = await _store_329391924Context.Products.FindAsync(id);
            if (product != null)
            {
                _store_329391924Context.Products.Remove(product);
                await _store_329391924Context.SaveChangesAsync();
            }
        }
        public async Task<(IEnumerable<Product> products, int total)> GetProducts(int[]? categoryId, string? q, double? minPrice,
      double? maxPrice, string? color, string? material, bool? inStock, bool? isActive, string? sort, int? skip, int? position)
        {       int pageSize = (skip.HasValue && skip.Value > 0) ? skip.Value : 12;
            int page = (position.HasValue && position.Value > 0) ? position.Value : 1;

            await _store_329391924Context.Database.EnsureCreatedAsync();


            var query = _store_329391924Context.Products.Include(p => p.Category).AsQueryable();
            if (isActive.HasValue)
            {
                query = query.Where(p => p.IsActive == isActive.Value);
            }
            else
            {
                query = query.Where(p => p.IsActive == true);
            }
            if (categoryId != null && categoryId.Length > 0)
            {
                query = query.Where(p => p.CategoryId != null && categoryId.Contains(p.CategoryId.Value));
            }

            
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(color))
                query = query.Where(p => p.Color != null && p.Color == color);

            if (!string.IsNullOrWhiteSpace(material))
                query = query.Where(p => p.Material != null && p.Material == material);
            if (inStock == true)
                query = query.Where(p => p.Quantity > 0);
            if (!string.IsNullOrWhiteSpace(q))
            {
                q = q.Trim();
                query = query.Where(p =>
                    p.ProductName.Contains(q) ||
                    p.Description.Contains(q) ||
                    (p.Color != null && p.Color.Contains(q)) ||
                    (p.Material != null && p.Material.Contains(q))
                );
            }
            if (string.Equals(sort, "desc", StringComparison.OrdinalIgnoreCase))
                query = query.OrderByDescending(p => p.Price);
            else
                query = query.OrderBy(p => p.Price);


            int total = await query.CountAsync();

            List<Product> products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(p => p.Category)
                .ToListAsync();

            return (products, total);
        }
    }
}