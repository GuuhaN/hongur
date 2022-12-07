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
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        return Ok(await _restaurantService.GetAsync(cancellationToken));
    }
}