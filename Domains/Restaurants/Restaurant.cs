using hongur.Domains.Ratings;

namespace hongur.Domains;

public class Restaurant : Entity
{
    public string Name { get; set; }
    public string Address { get; set; }
    
    public virtual ISet<Rating> Ratings { get; set; }
}