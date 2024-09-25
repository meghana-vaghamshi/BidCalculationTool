using BidCalculationTool.Server.Models;
using BidCalculationTool.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculationTool.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclePricingController : ControllerBase
    {
        private readonly IVehiclePricingService _calculationService;

        public VehiclePricingController(IVehiclePricingService calculationService)
        {
            _calculationService = calculationService;
        }

        /// <summary>
        /// Calculates the total price for a vehicle based on the provided request.
        /// </summary>
        /// <param name="request">The calculation request containing vehicle base price and type ID.</param>
        /// <returns>Returns the calculated total price or an error message.</returns>
        [HttpPost("calculate-bid-total")]
        public IActionResult CalculateTotalPrice([FromBody] VehiclePricingRequest request)
        {
            try
            {
                if (request == null || request.VehiclePrice <= 0 || request.VehicleTypeId <= 0)
                {
                    return BadRequest("Invalid input.");
                }

                var calculationResult = _calculationService.CalculateTotalCost(request);
                return Ok(calculationResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves all vehicle types from the database.
        /// </summary>
        /// <returns>Returns a list of vehicle types.</returns>
        [HttpGet("vehicle-types")]
        public IActionResult GetVehicleTypes()
        {
            try
            {
                var vehicleTypes = _calculationService.GetVehicleTypes();
                return Ok(vehicleTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
