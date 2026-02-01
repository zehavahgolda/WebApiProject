using DTOs;

namespace Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetCategories();
    }
}