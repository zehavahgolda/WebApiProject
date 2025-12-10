using Entity;

namespace Services
{
    public interface ICatgoryService
    {
        Task<List<Category>> GetCatogries();
    }
}