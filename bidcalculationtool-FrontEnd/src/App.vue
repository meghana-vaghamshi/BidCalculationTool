<script setup>
    import GreetingBanner from './components/GreetingBanner.vue';
    import VehicleBidCalculator from './components/VehicleBidCalculator.vue';
</script>

<template>
    <header>
        <div class="wrapper">         
            <GreetingBanner />
        </div>
    </header>

    <main>
        <VehicleBidCalculator />
    </main>
</template>
