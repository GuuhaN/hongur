using hongur.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace hongur.Responses
{
    public abstract class EntityConfig<TEntity> where TEntity : Entity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(entity => entity.Modified)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETUTCDATE()")
                .HasMaxLength(255)
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            builder.Property(entity => entity.Created)
                .HasColumnType("DATETIME")
                .HasDefaultValueSql("GETUTCDATE()")
                .HasMaxLength(255)
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();
        }
    }
}
