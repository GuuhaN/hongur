using hongur.Domains.Ratings;

namespace hongur.Responses.Restaurants;

public class RestaurantRatingDto
{
    public string Restaurant { get; set; }
    public decimal AverageRating { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}