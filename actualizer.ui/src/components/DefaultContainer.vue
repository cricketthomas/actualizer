<template>
    <div class="DefaultContainer">
        <Navbar />
        <Explorer />
        <Search />
    </div>
</template>

<script>
import axios from 'axios';
import { store } from '@/store/index.js';
import { mapState } from 'vuex';

import Navbar from '@/components/navbar.vue';
import Explorer from '@/components/explorer.vue';
import Search from '@/components/search.vue';

export default {
    name: 'DefaultContainer',
    data() {
        return {
            authenticated: false
        };
    },
    created() {
        this.isAuthenticated();
    },

    watch: {
        // Everytime the route changes, check for auth status
        $route: 'isAuthenticated'
    },
    methods: {
        async isAuthenticated() {
            this.authenticated = await this.$auth.isAuthenticated();
            if (!this.$store.state.userClaims) {
                this.$store.commit({
                    type: 'user/UpdateClaims',
                    userClaims: await this.$auth.getUser()
                });
            }
        },

        login() {
            this.$auth.loginRedirect('/');
        },
        async logout() {
            await this.$auth.logout();
            await this.isAuthenticated();
            // Navigate back to home
            this.$router.push({ path: '/' });

            //TODO: make this an action.
            this.$store.commit({
                type: 'user/UpdateClaims',
                userClaims: null
            });
        }
    },
    computed: mapState(['userClaims']),
    components: { Explorer, Navbar, Search }
};
</script>
