namespace hongur.Requests;

public class CreateRatingRequest
{
    public Guid RestaurantId { get; set; }
    public int Score { get; set; }
}