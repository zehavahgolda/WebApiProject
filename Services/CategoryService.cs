using AutoMapper;
using DTOs;
using Entity    ;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class CatgoryService : ICatgoryService
    {
        ICatogeryRepsitory _catgoryRepsitory;
        IMapper _imapper;

        public CatgoryService(ICatogeryRepsitory catgoryService, IMapper imapper)
        {
            _catgoryRepsitory = catgoryService;
            _imapper = imapper;
        }

        public async Task<List<CatogeryDto>> GetCatogries()
        {
            List<Category> catogeryList = await _catgoryRepsitory.GetCatogries();
            List<CatogeryDto> catogeryDto = _imapper.Map<List<CatogeryDto>>(catogeryList);
            return catogeryDto;
        }

    }
}
