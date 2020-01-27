<template>
    <div class="DefaultContainer">
        <Search />
        <Explorer />

    </div>
</template>

<script>
import { mapState } from 'vuex';
import Explorer from '@/components/explorer.vue';
import Search from '@/components/search.vue';

export default {
    name: 'DefaultContainer',
   
    computed: {
        ...mapState({userClaims: 'userClaims'
        })
        
        },
    components: { Explorer, Search }
};
</script>
