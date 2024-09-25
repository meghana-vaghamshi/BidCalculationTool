namespace BidCalculationTool.Server.DbContext.Entities
{
    public class AssociationFeeRule
    {
        public int AssociationFeeRuleId { get; set; } // Primary Key
        public decimal MinPrice { get; set; }
        public decimal? MaxPrice { get; set; } // Nullable for open-ended ranges
        public decimal FeeAmount { get; set; }
    }
}
