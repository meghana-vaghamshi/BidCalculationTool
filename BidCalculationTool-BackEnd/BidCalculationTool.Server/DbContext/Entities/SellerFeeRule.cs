namespace BidCalculationTool.Server.DbContext.Entities
{
    public class SellerFeeRule
    {
        public int SellerFeeRuleId { get; set; } // Primary Key
        public int VehicleTypeId { get; set; } // Foreign Key
        public decimal Percentage { get; set; }

        public VehicleType VehicleType { get; set; } // Navigation Property
    }
}
