namespace BidCalculationTool.Server.DbContext.Entities
{
    public class StorageFee
    {
        public int StorageFeeId { get; set; } // Primary Key
        public decimal FeeAmount { get; set; }
    }
}
