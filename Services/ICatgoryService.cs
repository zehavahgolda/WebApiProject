using DTOs;
using DTOs.DTOs;

namespace Services
{
    public interface ICatgoryService
    {
        Task<List<CatogeryDto>> GetCatogries();
        
    }
}