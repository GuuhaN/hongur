using hongur.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hongur.Responses.Restaurants;

public class RestaurantConfig : EntityConfig<Restaurant>, IEntityTypeConfiguration<Restaurant>
{
    public new void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        base.Configure(builder);

        builder
            .Property(entity => entity.Name)
            .HasMaxLength(255);
        
        builder
            .Property(entity => entity.Address)
            .HasMaxLength(255);

        builder
            .Property(entity => entity.Postcode)
            .HasMaxLength(255);
    }
}