<template>
    <div class="bidbox">
        <div class="bidbox--fields">
            <div class="field-form">
                <input type="number"
                       v-model="vehicleBasePrice"
                       class="field-form"
                       :placeholder="vehicleBasePricePlaceholder"
                       @focus="clearPlaceholder"
                       @input="validatePrice"
                       @blur="validateFinalPrice" />
                <div v-if="vehicleBasePriceError" class="error">{{ vehicleBasePriceError }}</div>
            </div>

            <div class="field-form">
                <select v-model="selectedVehicleTypeId" @change="calculateTotalIfNeeded">
                    <option value="0" disabled>Select Vehicle Type</option>
                    <option v-for="vehicleType in vehicleTypes" :key="vehicleType.Id" :value="vehicleType.Id">
                        {{ vehicleType.TypeName }}
                    </option>
                </select>
                <div v-if="vehicleTypeError" class="error">{{ vehicleTypeError }}</div>
            </div>

            <div v-if="showFeeDetails">
                <div class="field-form">
                    <label>Vehicle Price: </label> <span>${{ vehicleBasePrice }}</span>
                </div>
                <div class="field-form">
                    <label>Basic Buyer Fee: </label> <span>${{ buyerFee }}</span>
                </div>
                <div class="field-form">
                    <label>Special Fee: </label> <span>${{ specialFee }}</span>
                </div>
                <div class="field-form">
                    <label>Association Fee: </label> <span>${{ associationFee }}</span>
                </div>
                <div class="field-form">
                    <label>Storage Fee: </label> <span>${{ storageFee }}</span>
                </div>
                <div class="field-form total">
                    <label>Total: </label> <span>${{ totalCost }}</span>
                </div>
                <div class="field-form">
                    <input type="button"
                           class="field-button"
                           value="Reset Calculator"
                           @click="resetCalculator" />
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="js">
       
    const API_URL = process.env.VUE_APP_API_URL;    
    export default {
        data() {
            return {
                vehicleBasePrice: null,
                vehicleBasePricePlaceholder: 'Vehicle Base Price',
                vehicleBasePriceError: '',
                selectedVehicleTypeId: 0,
                vehicleTypes: [],
                vehicleTypeError: '',
                totalCost: 0,
                buyerFee: 0,
                specialFee: 0,
                storageFee: 0,
                associationFee: 0,
                showFeeDetails: false,               
            };
        },
        methods: {
            async fetchVehicleTypes() {
                try {
                    const response = await fetch(`${API_URL}api/VehiclePricing/vehicle-types`);

                    if (!response.ok) {
                        throw new Error(`API error: ${response.statusText}`);
                    }

                    const data = await response.json();

                    if (!Array.isArray(data) || !data.length) {
                        throw new Error("Invalid or empty vehicle types list.");
                    }

                    this.vehicleTypes = data;
                } catch (error) {
                    console.error('Error fetching vehicle types:', error);
                    this.vehicleTypeError = 'Failed to load vehicle types. Please try again later.';
                }
            },
            validatePrice() {
                const validPricePattern = /^\d*(\.\d{0,2})?$/;

                if (!validPricePattern.test(this.vehicleBasePrice)) {
                    this.vehicleBasePrice = this.vehicleBasePrice.toString().slice(0, -1);
                }
            },
            validateFinalPrice() {
                if (this.vehicleBasePrice === null || this.vehicleBasePrice === '') {
                    this.vehicleBasePriceError = ''; // Clear error if no input
                    this.resetCalculator();
                    return;
                }

                if (this.vehicleBasePrice < 0) {
                    this.vehicleBasePriceError = "Vehicle base price cannot be negative.";
                    this.vehicleBasePrice = null;
                } else if (this.vehicleBasePrice === 0) {                   
                    this.resetCalculator();
                    this.vehicleBasePriceError = "Vehicle base price cannot be zero.";
                } else {
                    this.vehicleBasePriceError = '';
                    this.calculateTotalIfNeeded();
                }
            },
            clearPlaceholder() {
                this.vehicleBasePricePlaceholder = '';
            },
            calculateTotalIfNeeded() {
                if (this.vehicleBasePrice > 0 && this.selectedVehicleTypeId > 0) {
                    this.calculateTotal();
                }
            },
            async calculateTotal() {
                if (this.vehicleBasePrice > 0 && this.selectedVehicleTypeId > 0) {
                    try {
                        const response = await fetch(`${API_URL}api/VehiclePricing/calculate-bid-total`, {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json' },
                            body: JSON.stringify({
                                VehiclePrice: this.vehicleBasePrice,
                                vehicleTypeId: this.selectedVehicleTypeId,
                            }),
                        });

                        if (!response.ok) {
                            throw new Error(`API error: ${response.statusText}`);
                        }

                        const result = await response.json();

                        if (result && this.isValidCalculationResponse(result)) {
                            this.updateFees(result);
                            this.showFeeDetails = true;
                        } else {
                            throw new Error("Invalid response from the API.");
                        }
                    } catch (error) {
                        console.error('Error calculating total:', error);
                    }
                } else {
                    this.resetCalculator();
                }
            },
            isValidCalculationResponse(result) {
                return (
                    result.TotalCost !== undefined &&
                    result.BasicBuyerFee !== undefined &&
                    result.SellerSpecialFee !== undefined &&
                    result.AssociationFee !== undefined &&
                    result.StorageFee !== undefined
                );
            },
            updateFees(result) {
                this.buyerFee = result.BasicBuyerFee;
                this.specialFee = result.SellerSpecialFee;
                this.associationFee = result.AssociationFee;
                this.storageFee = result.StorageFee;
                this.totalCost = result.TotalCost;
            },
            resetCalculator() {
                this.vehicleBasePrice = null;
                this.vehicleBasePricePlaceholder = 'Vehicle Base Price';
                this.selectedVehicleTypeId = 0;               
                this.showFeeDetails = false;
                this.vehicleBasePriceError = '';
                this.vehicleTypeError = '';
            },           
        },
        created() {
            this.fetchVehicleTypes();
        },
    };
</script>

<style scoped>
  
</style>
