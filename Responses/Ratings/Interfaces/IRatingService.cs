using hongur.Domains.Ratings;
using hongur.Responses.Restaurants;

namespace hongur.Responses.Ratings.Interfaces;

public interface IRatingService
{
    public Task<IReadOnlyCollection<Rating>> GetAsync(CancellationToken cancellationToken);
    public Task<RestaurantRatingDto> GetRestaurantRatingsAsync(Guid restaurantId, CancellationToken cancellationToken);
    public Task<Rating> CreateAsync(Guid restaurantId, int score, CancellationToken cancellationToken);
    public Task<RatingDto> UpdateAsync(Guid id, int score, CancellationToken cancellationToken);
    public Task<Rating> DeleteAsync(Guid id, CancellationToken cancellationToken);
}