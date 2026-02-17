using Entity;
using Repository;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _ratingRepository.AddRatingAsync(rating);
        }
    }
}
