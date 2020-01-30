<template>
    <div class="DefaultContainer">
                <b-button @click="toggleLoading">toogle loading</b-button>

        <b-loading :is-full-page="isFullPage" :active.sync="loading.isLoading" :can-cancel="false"></b-loading>
        <Search />
        <Explorer />
    </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex';
import Explorer from '@/components/explorer.vue';
import Search from '@/components/search.vue';

export default {
    name: 'DefaultContainer',
    data() {
        return {
            isFullPage: true,
        };
    },
    computed: {
        ...mapState({
            user: 'user',
            loading: 'loading'
        }),
 
    },
    methods: {
        toggleLoading() {
            this.$store.dispatch('loading');
        }
    },
    components: { Explorer, Search }
};
</script>
