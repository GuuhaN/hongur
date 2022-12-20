namespace hongur.Domains.Ratings;

public class Rating : Entity
{
    public int Score { get; set; }
    
    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}