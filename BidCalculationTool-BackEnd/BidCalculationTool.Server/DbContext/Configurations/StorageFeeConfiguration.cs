using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BidCalculationTool.Server.DbContext.Entities;

namespace BidCalculationTool.Server.DbContext.Configurations
{
    public class StorageFeeConfiguration : IEntityTypeConfiguration<StorageFee>
    {
        public void Configure(EntityTypeBuilder<StorageFee> builder)
        {
            builder.HasKey(sf => sf.StorageFeeId); // Primary Key
            builder.Property(sf => sf.FeeAmount).IsRequired();

            // Seeding initial data based on requirement
            builder.HasData(
                new StorageFee { StorageFeeId = 1, FeeAmount = 100 }
            );
        }
    }
}
