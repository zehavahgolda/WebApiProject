using AutoMapper;
using DTOs;
using Entity    ;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;
        IMapper _imapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper imapper)
        {
            _categoryRepository = categoryRepository;
            _imapper = imapper;
        }

        public async Task<List<CategoryDto>> GetCategories()
        {
            List<Category> categoryList = await _categoryRepository.GetCategories();
            List<CategoryDto> categoryDto = _imapper.Map<List<Category>,List<CategoryDto>>(categoryList);
            return categoryDto;
        }

    }
}
