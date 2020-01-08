<template>
    <div>
        
    <b-navbar>
        <template slot="brand">
            <b-navbar-item tag="router-link" :to="{ path: '/' }">
                <img src="https://raw.githubusercontent.com/buefy/buefy/dev/static/img/buefy-logo.png" alt=""></b-navbar-item>
        </template>
        <template slot="start">
            <b-navbar-item tag="router-link" :to="{ path: '/' }">Home</b-navbar-item>
            <b-navbar-item tag="router-link" :to="{ path: '/about' }">About</b-navbar-item>
        </template>
        <template slot="end">
            <b-navbar-item tag="div">
                <div class="buttons">
                    <a class="button is-primary">
                        <strong v-if="authenticated" v-on:click="logout" id="logout-button"> sign out, {{ this.$store.state.userClaims.given_name }} </strong>
                        <strong v-else v-on:click="login" id="login-button">log in</strong>
                    </a>
                </div>
            </b-navbar-item>
        </template>
    </b-navbar>
    </div>
</template>

<script>
import axios from "axios";
import { store } from "../store.js";
import { mapState } from "vuex";

export default {
  name: "navbar",
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
    $route: "isAuthenticated"
  },
  methods: {
    async isAuthenticated() {
      this.authenticated = await this.$auth.isAuthenticated();
      console.log(await this.$auth.getUser());
      if (!this.$store.state.userClaims) {
        this.$store.commit({
          type: "UpdateBearer",
          BearerToken: `${await this.$auth.getAccessToken()}`
        });
        this.$store.commit({
          type: "UpdateClaims",
          userClaims: await this.$auth.getUser()
        });
      }
    },

    login() {
      this.$auth.loginRedirect("/");
    },
    async logout() {
      await this.$auth.logout();
      await this.isAuthenticated();
      // Navigate back to home
      this.$router.push({ path: "/" });
      
      //TODO: make this an action.
         this.$store.commit({
          type: "UpdateBearer",
          BearerToken: ''
        });
         this.$store.commit({
          type: "UpdateClaims",
          userClaims: null
        });
    }
  },

  computed: mapState(["baseURL", "BearerToken", "userClaims"]),
  components: {
  }
};
</script>
