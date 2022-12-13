using hongur.Domains;
using hongur.Responses.Restaurants;
using hongur.Responses.Restaurants.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace hongur.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantsController : ControllerBase
{
    private ILogger<RestaurantsController> _logger;
    private IRestaurantService _restaurantService;

    public RestaurantsController(ILogger<RestaurantsController> logger, IRestaurantService restaurantService)
    {
        _logger = logger;
        _restaurantService = restaurantService;
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
}