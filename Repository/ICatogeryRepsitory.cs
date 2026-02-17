using Entity;

namespace Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCatogries();
        //Task<Category> GetByIdAsync(int id);
    }
    
}