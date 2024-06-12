using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Tables");
            builder.HasKey(x => x.TableID);
            builder.Property(x => x.TableID).IsRequired().UseIdentityColumn();
            builder.HasOne<Area>(x => x.Area)
                .WithMany(x => x.Tables)
                .HasForeignKey(x => x.AreaID).IsRequired();
        }
    }
}
