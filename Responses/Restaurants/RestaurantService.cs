using hongur.Application;
using hongur.Domains;
using hongur.Responses.Restaurants.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hongur.Responses.Restaurants;

public class RestaurantService : IRestaurantService
{
    public ApplicationDbContext _dbContext;

    public RestaurantService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Restaurant>> GetAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Restaurants.ToListAsync(cancellationToken);
    }

    public Task<Restaurant> UpdateAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Restaurant> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}