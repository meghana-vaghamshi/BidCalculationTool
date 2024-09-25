using BidCalculationTool.Server.Controllers;
using BidCalculationTool.Server.Models;
using BidCalculationTool.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BidCalculationTool.Tests
{
    public class VehiclePricingControllerTests
    {
        private readonly Mock<IVehiclePricingService> _mockCalculationService;
        private readonly VehiclePricingController _controller;

        public VehiclePricingControllerTests()
        {
            // Arrange: Initialize the mock service and the controller
            _mockCalculationService = new Mock<IVehiclePricingService>();
            _controller = new VehiclePricingController(_mockCalculationService.Object);
        }

        /// <summary>
        /// Helper method to test the vehicle pricing calculation logic.
        /// </summary>
        private void ExecutePricingTest(decimal vehiclePrice, int vehicleTypeId, VehiclePricingResult expectedResult)
        {
            // Arrange: Set up the request object
            var vehiclePricingRequest = new VehiclePricingRequest
            {
                VehiclePrice = vehiclePrice,
                VehicleTypeId = vehicleTypeId
            };

            // Mock the service method to return the expected result
            _mockCalculationService.Setup(service => service.CalculateTotalCost(vehiclePricingRequest))
                .Returns(expectedResult);

            // Act: Call the controller method
            var result = _controller.CalculateTotalPrice(vehiclePricingRequest);

            // Assert: Validate the response
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualResult = Assert.IsType<VehiclePricingResult>(okResult.Value);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CalculateTotalPrice_CommonVehiclePrice398_VerifySuccessAndQuoteValue()
        {
            // Test case for Common vehicle price $398.00
            var expectedResult = new VehiclePricingResult
            {
                TotalCost = 550.76m,
                BasicBuyerFee = 39.80m,
                SellerSpecialFee = 7.96m,
                AssociationFee = 5.00m,
                StorageFee = 100.00m
            };

            ExecutePricingTest(398.00m, 1, expectedResult);
        }

        [Fact]
        public void CalculateTotalPrice_CommonVehiclePrice501_VerifySuccessAndQuoteValue()
        {
            // Test case for Common vehicle price $501.00
            var expectedResult = new VehiclePricingResult
            {
                TotalCost = 671.02m,
                BasicBuyerFee = 50.00m,
                SellerSpecialFee = 10.02m,
                AssociationFee = 10.00m,
                StorageFee = 100.00m
            };

            ExecutePricingTest(501.00m, 1, expectedResult);
        }

        [Fact]
        public void CalculateTotalPrice_CommonVehiclePrice57_VerifySuccessAndQuoteValue()
        {
            // Test case for Common vehicle price $57.00
            var expectedResult = new VehiclePricingResult
            {
                TotalCost = 173.14m,
                BasicBuyerFee = 10.00m,
                SellerSpecialFee = 1.14m,
                AssociationFee = 5.00m,
                StorageFee = 100.00m
            };

            ExecutePricingTest(57.00m, 1, expectedResult);
        }

        [Fact]
        public void CalculateTotalPrice_LuxuryVehiclePrice1800_VerifySuccessAndQuoteValue()
        {
            // Test case for Luxury vehicle price $1800.00
            var expectedResult = new VehiclePricingResult
            {
                TotalCost = 2167.00m,
                BasicBuyerFee = 180.00m,
                SellerSpecialFee = 72.00m,
                AssociationFee = 15.00m,
                StorageFee = 100.00m
            };

            ExecutePricingTest(1800.00m, 2, expectedResult); // Assuming 2 is the ID for Luxury
        }

        [Fact]
        public void CalculateTotalPrice_CommonVehiclePrice1100_VerifySuccessAndQuoteValue()
        {
            // Test case for Common vehicle price $1100.00
            var expectedResult = new VehiclePricingResult
            {
                TotalCost = 1287.00m,
                BasicBuyerFee = 50.00m,
                SellerSpecialFee = 22.00m,
                AssociationFee = 15.00m,
                StorageFee = 100.00m
            };

            ExecutePricingTest(1100.00m, 1, expectedResult);
        }

        [Fact]
        public void CalculateTotalPrice_LuxuryVehiclePrice1000000_VerifySuccessAndQuoteValue()
        {
            // Test case for Luxury vehicle price $1,000,000.00
            var expectedResult = new VehiclePricingResult
            {
                TotalCost = 1040320.00m,
                BasicBuyerFee = 200.00m,
                SellerSpecialFee = 40000.00m,
                AssociationFee = 20.00m,
                StorageFee = 100.00m
            };

            ExecutePricingTest(1000000.00m, 2, expectedResult);
        }
    }
}