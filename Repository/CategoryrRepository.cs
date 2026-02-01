using Microsoft.EntityFrameworkCore;
using Entity;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        Store_329391924Context _store_329391924Context;

        public CategoryRepository(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }
        public async Task<List<Category>> GetCategories()
        {
            return await _store_329391924Context.Categories.ToListAsync();
        }









    }
}
