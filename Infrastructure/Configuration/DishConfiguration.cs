using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishs");
            builder.HasKey(x => x.DishId);
            builder.Property(x => x.DishId).IsRequired().UseIdentityColumn();
            builder.HasOne<Category>(x => x.Category)
                .WithMany(x => x.Dishs)
                .HasForeignKey(x => x.CategoryID).IsRequired();
        }
    }
}
