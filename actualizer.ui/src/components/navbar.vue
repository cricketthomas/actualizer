<template>
    <div>
        <b-navbar>
            <template slot="brand">
                <b-navbar-item tag="router-link" :to="{ path: '/' }">
                    <img src="/img/icons/actualizer_web.png" alt="actualizer logo"
                /></b-navbar-item>
            </template>
            <template slot="start">
                <b-navbar-item tag="router-link" :to="{ path: '/' }">Home</b-navbar-item>
                <b-navbar-item tag="router-link" :to="{ path: '/about' }">About</b-navbar-item>
            </template>
            <template slot="end">
                <b-navbar-item tag="div">
                    <div class="buttons">
                        <a class="button is-primary">
                            <strong v-if="authenticated" @click="logout()" id="logout-button"> sign out</strong>
                            <strong v-else @click="login()" id="login-button">log in</strong>
                        </a>
                    </div>
                </b-navbar-item>
            </template>
        </b-navbar>
    </div>
</template>

<script>
import axios from 'axios';
import { store } from '@/store/index.js';
import { mapState } from 'vuex';

export default {
    name: 'navbar',
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
    components: {}
};
</script>
