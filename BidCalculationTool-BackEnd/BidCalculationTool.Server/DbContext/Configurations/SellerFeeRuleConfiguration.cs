using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BidCalculationTool.Server.DbContext.Entities;

namespace BidCalculationTool.Server.DbContext.Configurations
{
    public class SellerFeeRuleConfiguration : IEntityTypeConfiguration<SellerFeeRule>
    {
        public void Configure(EntityTypeBuilder<SellerFeeRule> builder)
        {
            builder.HasKey(sfr => sfr.SellerFeeRuleId); // Primary Key
            builder.Property(sfr => sfr.Percentage).IsRequired();

            builder.HasOne(sfr => sfr.VehicleType)
                .WithMany() // One-to-many relationship
                .HasForeignKey(sfr => sfr.VehicleTypeId); // Foreign key

            builder.HasData(
            new SellerFeeRule { SellerFeeRuleId = 1, VehicleTypeId = 1, Percentage = 2 },
            new SellerFeeRule { SellerFeeRuleId = 2, VehicleTypeId = 2, Percentage = 4 }
            );
        }
    }
}
