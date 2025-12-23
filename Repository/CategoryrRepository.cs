using Microsoft.EntityFrameworkCore;
using Entity;
using System.Threading.Tasks;

namespace Repository
{
    public class CatogeryRepsitory : ICatogeryRepsitory
    {
        Store_329391924Context _store_329391924Context;

        public CatogeryRepsitory(Store_329391924Context store_329391924Context)
        {
            _store_329391924Context = store_329391924Context;
        }
        public async Task<List<Category>> GetCatogries()
        {
            return await _store_329391924Context.Categories.ToListAsync();
        }









    }
}
