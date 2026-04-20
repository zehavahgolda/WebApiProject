using AutoMapper;
using DTOs;
using DTOs.DTOs;
using Entity    ;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class CatgoryService : ICatgoryService
    {
        ICategoryRepository _catgoryRepsitory;
        IMapper _imapper;

        public CatgoryService(ICategoryRepository catgoryService, IMapper imapper)
        {
            _catgoryRepsitory = catgoryService;
            _imapper = imapper;
        }

        public async Task<List<CatogeryDto>> GetCatogries()
        {
            List<Category> catogeryList = await _catgoryRepsitory.GetCatogries();
            List<CatogeryDto> catogeryDto = _imapper.Map<List<Category>,List<CatogeryDto>>(catogeryList);
            return catogeryDto;
        }

        
     

    }
}
