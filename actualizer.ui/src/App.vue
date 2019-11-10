<template>
  <div id="app">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link>
    <router-link to="/" tag="button" id="home-button"> Home </router-link>
    <button v-if="authenticated" v-on:click="logout" id="logout-button">
      Logout
    </button>

    <button v-else v-on:click="login" id="login-button">Login</button>
    <br />
    <hr />
    <h2 v-if="authenticated">Hey {{ this.$store.state.userClaims.given_name }}</h2>

    <Search />
    <router-view />
  </div>
</template>

<script>
import UserClaims from "./components/UserClaims.vue";
import Search from "./components/SearchComments.vue";
import axios from "axios";
import { store } from "./store.js";
import { mapState } from "vuex";

export default {
  name: "app",

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
      
      if (!this.$store.state.userClaims) {
        this.$store.commit({
          type: "UpdateBearer",
          BearerToken: `${await this.$auth.getAccessToken()}`
        });
        this.$store.commit({
          type: "UpdateClaims",
          userClaims: await Object.entries(await this.$auth.getUser())
            .map(entry => ({ [entry[0]]: entry[1] }))
            .reduce((k, v) => Object.assign(k, v), {})
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
    UserClaims,
    Search
  }
};
</script>


<style>
</style>
