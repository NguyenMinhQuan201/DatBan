using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    public class OrderTableConfiguration : IEntityTypeConfiguration<OrderTable>
    {
        public void Configure(EntityTypeBuilder<OrderTable> builder)
        {
            builder.ToTable("OrderTables");
            builder.HasKey(x => new {x.OrderID,x.TableID});
            builder.HasOne(t => t.Table).WithMany(pc => pc.OrderTables)
                .HasForeignKey(pc => pc.TableID);

            builder.HasOne(t => t.Order).WithMany(pc => pc.OrderTables)
              .HasForeignKey(pc => pc.OrderID);
        }
    }
}
