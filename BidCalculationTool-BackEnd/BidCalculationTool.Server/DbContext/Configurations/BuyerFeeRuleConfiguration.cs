using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BidCalculationTool.Server.DbContext.Entities;

namespace BidCalculationTool.Server.DbContext.Configurations
{
    public class BuyerFeeRuleConfiguration : IEntityTypeConfiguration<BuyerFeeRule>
    {
        public void Configure(EntityTypeBuilder<BuyerFeeRule> builder)
        {
            builder.HasKey(bfr => bfr.BuyerFeeRuleId); // Primary Key
            builder.Property(bfr => bfr.Percentage).IsRequired();
            builder.Property(bfr => bfr.MinFee).IsRequired();
            builder.Property(bfr => bfr.MaxFee).IsRequired();

            builder.HasOne(bfr => bfr.VehicleType)
                .WithMany() // One-to-many relationship
                .HasForeignKey(bfr => bfr.VehicleTypeId); // Foreign key

            builder.HasData(
            new BuyerFeeRule { BuyerFeeRuleId = 1, VehicleTypeId = 1, Percentage = 10, MinFee = 10, MaxFee = 50 },
            new BuyerFeeRule { BuyerFeeRuleId = 2, VehicleTypeId = 2, Percentage = 10, MinFee = 25, MaxFee = 200 }
            );
        }
    }
}
