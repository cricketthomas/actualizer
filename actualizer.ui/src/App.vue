<template>
    <div id="app">
        <navbar />
        <router-view />
    </div>
</template>

<script>
import navbar from '@/components/navbar.vue';
import axios from 'axios';
export default {
    name: 'app',
    components: {
        navbar
    }
};
</script>