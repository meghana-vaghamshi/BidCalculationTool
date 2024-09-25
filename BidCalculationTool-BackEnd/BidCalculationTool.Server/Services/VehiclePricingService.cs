
using BidCalculationTool.Server.DbContext;
using BidCalculationTool.Server.DbContext.Entities;
using BidCalculationTool.Server.Models;

namespace BidCalculationTool.Server.Services
{
    // Service responsible for calculating the total vehicle pricing, including various fees
    public class VehiclePricingService : IVehiclePricingService
    {
        private readonly BidCalcDbContext _context;

        // Constructor to initialize the service with the database context
        public VehiclePricingService(BidCalcDbContext context)
        {
            _context = context;
        }

        //This method will Calculates the total cost of the vehicle, including fees like buyer, seller, association, and storage
        public VehiclePricingResult CalculateTotalCost(VehiclePricingRequest request)
        {
            try
            {
                ValidateRequest(request);
                
                var vehicleType = GetVehicleType(request.VehicleTypeId);
                var buyerFeeRule = GetBuyerFeeRule(request.VehicleTypeId);
                var sellerFeeRule = GetSellerFeeRule(request.VehicleTypeId);
                var storageFee = GetStorageFee();

                // Calculate the fees
                decimal basicBuyerFee = CalculateBasicBuyerFee(request.VehiclePrice, buyerFeeRule);
                decimal sellerSpecialFee = CalculateSellerSpecialFee(request.VehiclePrice, sellerFeeRule);
                decimal associationFee = GetAssociationFee(request.VehiclePrice);
                decimal totalStorageFee = storageFee.FeeAmount;

                // Calculate total cost
                decimal totalCost = request.VehiclePrice + basicBuyerFee + sellerSpecialFee + associationFee + totalStorageFee;

                // Return the result object
                return new VehiclePricingResult
                {
                    TotalCost = totalCost,
                    BasicBuyerFee = basicBuyerFee,
                    SellerSpecialFee = sellerSpecialFee,
                    AssociationFee = associationFee,
                    StorageFee = totalStorageFee
                };
            }
            catch(Exception ex)
            {
                throw new ApplicationException("An error occurred while calculating the total cost.", ex);
            }
        }

        // Validates the vehicle pricing request
        private void ValidateRequest(VehiclePricingRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
        }

        // Retrieves the vehicle type from the database
        private VehicleType GetVehicleType(int vehicleTypeId)
        {
            try
            {
                var vehicleType = _context.VehicleTypes.FirstOrDefault(vt => vt.Id == vehicleTypeId);
                if (vehicleType == null)
                {
                    throw new Exception("Invalid vehicle type.");
                }
                return vehicleType;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve vehicle type.", ex);
            }
        }

        // Retrieves the buyer fee rule from the database
        private BuyerFeeRule GetBuyerFeeRule(int vehicleTypeId)
        {
            try
            {
                var buyerFeeRule = _context.BuyerFeeRules.FirstOrDefault(bfr => bfr.VehicleTypeId == vehicleTypeId);
                if (buyerFeeRule == null)
                {
                    throw new Exception("Buyer fee rule not found.");
                }
                return buyerFeeRule;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve buyer fee rule.", ex);
            }
        }

        // Retrieves the seller fee rule from the database
        private SellerFeeRule GetSellerFeeRule(int vehicleTypeId)
        {
            try
            {
                var sellerFeeRule = _context.SellerFeeRules.FirstOrDefault(sfr => sfr.VehicleTypeId == vehicleTypeId);
                if (sellerFeeRule == null)
                {
                    throw new Exception("Seller fee rule not found.");
                }
                return sellerFeeRule;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve seller fee rule.", ex);
            }
        }

        // Retrieves the storage fee from the database
        private StorageFee GetStorageFee()
        {
            try
            {
                var storageFee = _context.StorageFees.FirstOrDefault();
                if (storageFee == null)
                {
                    throw new Exception("Storage fee not found.");
                }
                return storageFee;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve storage fee.", ex);
            }
        }

        // Calculates the basic buyer fee
        private decimal CalculateBasicBuyerFee(decimal vehiclePrice, BuyerFeeRule buyerFeeRule)
        {
            // Calculate the basic buyer fee as a percentage of the vehicle price
            decimal fee = (buyerFeeRule.Percentage / 100) * vehiclePrice;
            
            // Return the fee clamped between the min and max limits
            return Math.Clamp(fee, buyerFeeRule.MinFee, buyerFeeRule.MaxFee);            
        }

        // Calculates the seller's special fee
        private decimal CalculateSellerSpecialFee(decimal vehiclePrice, SellerFeeRule sellerFeeRule)
        {
            // Calculate the seller's special fee as a percentage of the vehicle price
            return (sellerFeeRule.Percentage / 100) * vehiclePrice;
        }

        private decimal GetAssociationFee(decimal vehiclePrice)
        {            
            // Query the association fee rules from the database based on vehicle price
            var associationFeeRule = _context.AssociationFeeRules
                .Where(afr => vehiclePrice >= afr.MinPrice && (afr.MaxPrice == null || vehiclePrice <= afr.MaxPrice))
                .FirstOrDefault();

            return associationFeeRule?.FeeAmount ?? 0; // Return the fee amount or 0 if no rule matches
        }

        // Fetches all vehicle types from the database
        public IEnumerable<VehicleType> GetVehicleTypes()
        {
            // Fetch vehicle types from the database
            return _context.VehicleTypes.Select(vt => new VehicleType
            {
                Id = vt.Id,
                TypeName = vt.TypeName
            }).ToList(); // Return as a list for the controller
        }
    }
}
