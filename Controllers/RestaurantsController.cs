using hongur.Domains;
using hongur.Responses.Ratings.Interfaces;
using hongur.Responses.Restaurants;
using hongur.Responses.Restaurants.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hongur.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly ILogger<RestaurantsController> _logger;
    private readonly IRestaurantService _restaurantService;
    private readonly IRatingService _ratingService;

    public RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantService restaurantService, IRatingService ratingService)
    {
        _logger = logger;
        _restaurantService = restaurantService;
        _ratingService = ratingService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<Restaurant>),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        return Ok(await _restaurantService.GetAsync(cancellationToken));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(Restaurant),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> CreateAsync([FromBody]RestaurantDto restaurant, CancellationToken cancellationToken)
    {
        return Ok(await _restaurantService.CreateAsync(restaurant, cancellationToken));
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(Restaurant),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]RestaurantDto restaurant, CancellationToken cancellationToken)
    {
        return Ok(await _restaurantService.UpdateAsync(id, restaurant, cancellationToken));
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(Restaurant),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _restaurantService.DeleteAsync(id, cancellationToken));
    }

    [HttpGet("{id:guid}/ratings")]
    [ProducesResponseType(typeof(IReadOnlyCollection<RestaurantRatingDto>),200)]
    public async Task<IActionResult> GetRatingsAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _ratingService.GetRestaurantRatingsAsync(id, cancellationToken));
    }
}