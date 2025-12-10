using Entity;

namespace Repository
{
    public interface ICatogeryRepsitory
    {
        Task<List<Category>> GetCatogries();
    }
}