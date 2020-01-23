<template>
    <div id="app">
        <navbar />
        <explorer />
        <router-view />
    </div>
</template>

<script>
import navbar from '@/components/navbar.vue';
import explorer from '@/components/explorer.vue';

export default {
    name: 'app',
    components: {
        navbar,
        explorer
    }
};
</script>