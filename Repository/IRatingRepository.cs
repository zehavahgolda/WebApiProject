using Entity;
using System.Threading.Tasks;

public interface IRatingRepository
{
    Task AddRatingAsync(Rating rating);
}
