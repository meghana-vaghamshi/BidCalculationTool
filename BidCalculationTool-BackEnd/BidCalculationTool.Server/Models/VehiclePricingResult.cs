namespace BidCalculationTool.Server.Models
{
    public class VehiclePricingResult
    {
        // Total cost including all fees
        public decimal TotalCost { get; set; }

        // Basic buyer fee calculated
        public decimal BasicBuyerFee { get; set; }

        // Seller's special fee calculated
        public decimal SellerSpecialFee { get; set; }

        // Association fee calculated
        public decimal AssociationFee { get; set; }

        // Fixed storage fee
        public decimal StorageFee { get; set; }
    }
}
