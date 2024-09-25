namespace BidCalculationTool.Server.Models
{
    public class VehiclePricingRequest
    {
        // The base price of the vehicle
        public decimal VehiclePrice { get; set; }

        // The ID of the vehicle type (Common or Luxury)
        public int VehicleTypeId { get; set; }

    }
}
