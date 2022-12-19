using hongur.Application;
using hongur.Domains.Ratings;
using hongur.Responses.Ratings.Interfaces;
using hongur.Responses.Restaurants;
using Microsoft.EntityFrameworkCore;

namespace hongur.Responses.Ratings;

public class RatingService : IRatingService
{
    public readonly ApplicationDbContext _dbContext;

    public RatingService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IReadOnlyCollection<Rating>> GetAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Ratings.ToListAsync(cancellationToken);
    }

    public async Task<RestaurantRatingDto> GetRestaurantRatingsAsync(Guid restaurantId, CancellationToken cancellationToken)
    {
        var restaurant = _dbContext.Restaurants.FirstOrDefault(restaurant => restaurant.Id == restaurantId);

        if (restaurant == null)
        {
            throw new KeyNotFoundException();
        }

        var restaurantRatings = await _dbContext.Ratings.Where(rating => rating.RestaurantId == restaurantId).ToListAsync(cancellationToken);

        var restaurantRatingDto = new RestaurantRatingDto
        {
            Ratings = restaurantRatings,
            Restaurant = restaurant.Name
        };

        if (!restaurantRatings.Any())
        {
            return restaurantRatingDto;
        }
        
        restaurantRatingDto.AverageRating = Math.Round((decimal)restaurantRatings.Sum(x => x.Score) / restaurantRatings.Count, 2);

        return restaurantRatingDto;
    }

    public async Task<Rating> CreateAsync(Guid restaurantId, int score, CancellationToken cancellationToken)
    {
        var restaurant = _dbContext.Restaurants.FirstOrDefault(restaurant => restaurant.Id == restaurantId);
        
        if (restaurant == null)
        {
            throw new KeyNotFoundException();
        }
        
        if (score <= 0 || score > 5)
        {
            throw new InvalidOperationException("Score can only be between 1 and 10");
        }

        var restaurantRating = _dbContext.Ratings.OrderByDescending(rating => rating.Created).FirstOrDefault();

        if (restaurantRating != null)
        {
            if (restaurantRating.Created.AddDays(1) >= DateTime.Now.Date)
            {
                throw new InvalidOperationException("You already have voted on this restaurant within 24 hours, come back tomorrow 🧍");
            }
        }

        var rating = new Rating
        {
            RestaurantId = restaurantId,
            Score = score,
        };

        await _dbContext.Ratings.AddAsync(rating, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return rating;
    }

    public async Task<RatingDto> UpdateAsync(Guid id, int score, CancellationToken cancellationToken)
    {
        var rating = _dbContext.Ratings.FirstOrDefault(rating => rating.Id == id);

        if (rating == null)
        {
            throw new KeyNotFoundException();
        }

        if (score <= 0 || score > 5)
        {
            throw new InvalidOperationException("Score can only be between 1 and 10");
        }

        rating.Score = score;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RatingDto
        {
            Id = rating.Id,
            Restaurant = rating.Restaurant.Name,
            Score = rating.Score
        };
    }

    public async Task<Rating> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var rating = _dbContext.Ratings.FirstOrDefault(rating => rating.Id == id);

        if (rating == null)
        {
            throw new KeyNotFoundException();
        }

        _dbContext.Ratings.Remove(rating);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return rating;
    }
}