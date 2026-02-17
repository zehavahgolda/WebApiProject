using Entity;
using System.Threading.Tasks;

public interface IRatingService
{
    Task AddRatingAsync(Rating rating);
}
