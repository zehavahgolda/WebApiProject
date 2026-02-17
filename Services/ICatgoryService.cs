using DTOs;

namespace Services
{
    public interface ICatgoryService
    {
        Task<List<CatogeryDto>> GetCatogries();
        //Task<CatogeryDto> GetCategoryById(int id);
    }
}