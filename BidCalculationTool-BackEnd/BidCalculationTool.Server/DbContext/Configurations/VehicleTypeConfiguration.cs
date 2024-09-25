using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BidCalculationTool.Server.DbContext.Entities;

namespace BidCalculationTool.Server.DbContext.Configurations
{
    public class VehicleTypeConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder.HasKey(vt => vt.Id); // Primary Key
            builder.Property(vt => vt.TypeName)
                .IsRequired()
                .HasMaxLength(50); // Configure column length and required field

            builder.HasData(
                new VehicleType { Id = 1, TypeName = "Common" },
                new VehicleType { Id = 2, TypeName = "Luxury" }
            );
        }
    }
}
