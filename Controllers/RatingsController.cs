using hongur.Domains.Ratings;
using hongur.Requests;
using hongur.Responses.Ratings.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hongur.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class RatingsController : ControllerBase
{
    private readonly IRatingService _ratingService;
    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<Rating>),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        return Ok(await _ratingService.GetAsync(cancellationToken));
    }

    [HttpPost]
    [ProducesResponseType(typeof(IReadOnlyCollection<Rating>),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateRatingRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _ratingService.CreateAsync(request.RestaurantId, request.Score, cancellationToken));
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(IReadOnlyCollection<Rating>),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> UpdateAsync(Guid id, CreateRatingRequest request, CancellationToken cancellationToken)
    {
        return Ok(await _ratingService.UpdateAsync(id, request.Score, cancellationToken));
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(IReadOnlyCollection<Rating>),200)]
    [ProducesResponseType(typeof(void),404)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _ratingService.DeleteAsync(id, cancellationToken));
    }
}