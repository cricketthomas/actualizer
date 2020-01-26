<template>
    <div id="app">
        <Navbar :authenticated="authenticated" :login="login" :logout="logout"/>
        <router-view />
    </div>
</template>

<script>
import Navbar from '@/components/navbar.vue';

export default {
    name: 'app',
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
    components: {
        Navbar
    }
};
</script>