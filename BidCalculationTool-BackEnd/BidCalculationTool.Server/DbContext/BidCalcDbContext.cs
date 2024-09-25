using BidCalculationTool.Server.DbContext.Configurations;
using BidCalculationTool.Server.DbContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace BidCalculationTool.Server.DbContext
{
    public class BidCalcDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<BuyerFeeRule> BuyerFeeRules { get; set; }
        public DbSet<SellerFeeRule> SellerFeeRules { get; set; }
        public DbSet<AssociationFeeRule> AssociationFeeRules { get; set; }
        public DbSet<StorageFee> StorageFees { get; set; }

        public BidCalcDbContext(DbContextOptions<BidCalcDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VehicleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuyerFeeRuleConfiguration());
            modelBuilder.ApplyConfiguration(new SellerFeeRuleConfiguration());
            modelBuilder.ApplyConfiguration(new AssociationFeeRuleConfiguration());
            modelBuilder.ApplyConfiguration(new StorageFeeConfiguration());
        }
    }
}
