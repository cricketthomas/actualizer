<template>
  <div>
    <nav class="navbar is-fixed-top topNavBar">
      <!-- v-on:scroll="handleScroll"> -->
        <div class="navbar-brand ">
          <router-link class="navbar-item" to="/">
            <img src="@/assets/actualizerheader.png" alt="Actualizer logo" />
          </router-link>
          <span class="navbar-item is-hidden-desktop" style="margin-top: .5rem">
            <b-dropdown aria-role="list">
              <p class="tag is-primary" slot="trigger" role="button">
                <i class="fa fa-chevron-circle-down is-size-9"></i>
              </p>
              <b-dropdown-item v-if="authenticated" aria-role="listitem" @click="logout()">
                <strong class="has-text-danger">sign out</strong>
              </b-dropdown-item>
              <b-dropdown-item v-else aria-role="listitem" @click="login()">
                <strong class="has-text-success">sign in</strong>
              </b-dropdown-item>
            </b-dropdown>
          </span>
        </div>
        <div class="navbar-menu">
          <div class="navbar-end">
            <div class="navbar-item">
              <a class="button is-primary">
                <strong v-if="authenticated" @click="logout()" id="logout-button">sign out</strong>
                <strong v-else @click="login()" id="login-button">sign in</strong>
              </a>
            </div>
          </div>
        </div>
     
    </nav>
  </div>
</template>




<script>
export default {
  name: 'navbar',
  props: {
    authenticated: Boolean,
    logout: Function,
    login: Function
  },
  data() {
    return {
      prevScrollpos: window.pageYOffset
    };
  },
  methods: {
    handleScroll() {
      let currentScrollPos = window.pageYOffset;
      if (this.prevScrollpos > currentScrollPos) {
        document.getElementById('navbar').style.top = '0';
      } else {
        document.getElementById('navbar').style.top = '-50px';
      }
      this.prevScrollpos = currentScrollPos;
    }
  },
  created() {
   // window.addEventListener('scroll', this.handleScroll);
  },
  destroyed() {
   // window.removeEventListener('scroll', this.handleScroll);
  }
};
</script>
