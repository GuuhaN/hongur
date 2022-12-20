using hongur.Domains.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hongur.Responses.Ratings;

public class RatingConfig : EntityConfig<Rating>, IEntityTypeConfiguration<Rating>
{
    public new void Configure(EntityTypeBuilder<Rating> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(entity => entity.Restaurant)
            .WithMany(entity => entity.Ratings);
    }
}