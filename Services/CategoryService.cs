using Repository;
using Entity    ;
using System.Threading.Tasks;

namespace Services
{
    public class CatgoryService :  ICatgoryService
    {

        ICatogeryRepsitory _catgoryRepsitory;
    
        public CatgoryService(ICatogeryRepsitory catgoryService)
        {
            _catgoryRepsitory = catgoryService;
        }

        public async Task<List<Category>> GetCatogries()
        {
            return await _catgoryRepsitory.GetCatogries();
        }




    }
}
