using hongur.Domains;

namespace hongur.Responses.Restaurants.Interfaces;

public interface IRestaurantService
{
    public Task<IReadOnlyCollection<Restaurant>> GetAsync(CancellationToken cancellationToken);
    public Task<Restaurant> CreateAsync(RestaurantDto dto, CancellationToken cancellationToken);
    public Task<RestaurantDto> UpdateAsync(Guid id, RestaurantDto dto, CancellationToken cancellationToken);
    public Task<Restaurant> DeleteAsync(Guid id, CancellationToken cancellationToken);
}