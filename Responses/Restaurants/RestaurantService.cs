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

    public async Task<Restaurant> CreateAsync(RestaurantDto entity, CancellationToken cancellationToken)
    {
        if (entity == null)
        {
            throw new KeyNotFoundException();
        }

        var restaurant = new Restaurant
        {
            Name = entity.Name,
            Address = entity.Address,
            PlaceId = entity.PlaceId
        };

        await _dbContext.Restaurants.AddAsync(restaurant, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return restaurant;
    }

    public async Task<RestaurantDto> UpdateAsync(Guid id, RestaurantDto dto, CancellationToken cancellationToken)
    {
        var restaurant = await _dbContext.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            throw new KeyNotFoundException();
        }
        
        restaurant.Name = dto.Name;
        restaurant.Address = dto.Address;

        _dbContext.Update(restaurant);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return dto;
    }

    public async Task<Restaurant> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var restaurant = await _dbContext.Restaurants.FindAsync(id);

        if (restaurant == null)
        {
            throw new KeyNotFoundException();
        }

        _dbContext.Restaurants.Remove(restaurant);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return restaurant;
    }
}