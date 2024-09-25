namespace BidCalculationTool.Server.DbContext.Entities
{
    public class BuyerFeeRule
    {
        public int BuyerFeeRuleId { get; set; } // Primary Key
        public int VehicleTypeId { get; set; } // Foreign Key
        public decimal Percentage { get; set; }
        public decimal MinFee { get; set; }
        public decimal MaxFee { get; set; }

        public VehicleType VehicleType { get; set; } // Navigation Property
    }
}
