using DTOs;

namespace Services
{
    public interface ICatgoryService
    {
        Task<List<CatogeryDto>> GetCatogries();
    }
}