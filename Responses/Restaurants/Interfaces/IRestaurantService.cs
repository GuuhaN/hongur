using hongur.Domains;

namespace hongur.Responses.Restaurants.Interfaces;

public interface IRestaurantService
{
    public Task<IReadOnlyCollection<Restaurant>> GetAsync(CancellationToken cancellationToken);
    public Task<Restaurant> UpdateAsync(Guid id, CancellationToken cancellationToken);
    public Task<Restaurant> DeleteAsync(Guid id, CancellationToken cancellationToken);
}