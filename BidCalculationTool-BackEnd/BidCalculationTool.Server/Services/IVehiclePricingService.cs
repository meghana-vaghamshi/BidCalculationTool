
using BidCalculationTool.Server.DbContext.Entities;
using BidCalculationTool.Server.Models;

namespace BidCalculationTool.Server.Services
{
    public interface IVehiclePricingService
    {
        VehiclePricingResult CalculateTotalCost(VehiclePricingRequest request);
        IEnumerable<VehicleType> GetVehicleTypes();
    }
}
