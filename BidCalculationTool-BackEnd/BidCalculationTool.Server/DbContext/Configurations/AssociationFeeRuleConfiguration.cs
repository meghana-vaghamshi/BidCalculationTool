using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BidCalculationTool.Server.DbContext.Entities;

namespace BidCalculationTool.Server.DbContext.Configurations
{
    public class AssociationFeeRuleConfiguration : IEntityTypeConfiguration<AssociationFeeRule>
    {
        public void Configure(EntityTypeBuilder<AssociationFeeRule> builder)
        {
            builder.HasKey(afr => afr.AssociationFeeRuleId); // Primary Key
            builder.Property(afr => afr.MinPrice).IsRequired();
            builder.Property(afr => afr.MaxPrice);
            builder.Property(afr => afr.FeeAmount).IsRequired();

            builder.HasData(
            new AssociationFeeRule { AssociationFeeRuleId = 1, MinPrice = 1, MaxPrice = 500, FeeAmount = 5 },
            new AssociationFeeRule { AssociationFeeRuleId = 2, MinPrice = 501, MaxPrice = 1000, FeeAmount = 10 },
            new AssociationFeeRule { AssociationFeeRuleId = 3, MinPrice = 1001, MaxPrice = 3000, FeeAmount = 15 },
            new AssociationFeeRule { AssociationFeeRuleId = 4, MinPrice = 3001, MaxPrice = null, FeeAmount = 20 }
        );
        }
    }
}
