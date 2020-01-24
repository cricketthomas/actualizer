<template>
    <div id="app">
        <DefaultContainer />
        <router-view />
    </div>
</template>

<script>
import DefaultContainer from '@/components/DefaultContainer.vue';

export default {
    name: 'app',
    components: {
        DefaultContainer
        
    }
};
</script>