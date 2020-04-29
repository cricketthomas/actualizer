<template>
  <div>
    <div class="status-bar-hack is-hidden-widescreen is-hidden-desktop"></div>
    <TopNavbar :authenticated="authenticated" :login="login" :logout="logout" />
    <section class="section">
      <div class="columns">
        <aside class="column is-2 is-narrow-mobile is-fullheight section is-hidden-mobile is-hidden-touch">
          <p class="menu-label">Navigation</p>
          <ul class="menu-list">
            <!-- <li>
              <router-link to="/" exact>
                <span class="icon"> <i class="fa fa-home"></i> </span>Home
              </router-link>
            </li> -->
            <li>
              <router-link to="search">
                <span class="icon">
                  <i class="fa fa-table"></i>
                </span>
                Search
              </router-link>
              <ul>
                <li>
                  <router-link to="analytics">
                    <span class="icon is-small">
                      <i class="fa fa-link"></i>
                    </span>
                    Analytics
                  </router-link>
                </li>
                <li>
                  <router-link to="settings">
                    <span class="icon is-small">
                      <i class="fa fa-cog"></i>
                    </span>
                    Settings
                  </router-link>
                </li>
              </ul>
            </li>
            <li>
              <router-link to="about" class>
                <span class="icon">
                  <i class="fa fa-info"></i>
                </span>
                About
              </router-link>
            </li>
          </ul>
        </aside>
        <main class="column dashboard">
          <div class="level">
            <div class="level-left">
              <div class="level-item">
               <slot><div class="title">{{ header }}</div></slot>
              </div>
            </div>
            <div class="level-right">
              <div class="level-item" v-if="buttonGroupShow">
               <button type="button" class="button is-small remove-click-attributes">
                  <strong>Query: &nbsp; {{ comments.results.metadata.search }}</strong>
                </button>
                <button type="button" class="button is-small remove-click-attributes">
                  <strong>Video ID:&nbsp; {{ comments.results.metadata.video_id }}</strong>
                </button>
                <button type="button" class="button is-small remove-click-attributes">
                  <strong># Retrieved: &nbsp; {{ comments.results.metadata.count }}</strong>
                </button>  
              </div>
              <div v-else class="content">
                <button type="button" class="button is-small">
                  <strong>Search for comments</strong>
                </button>
              </div>
            </div>
          </div>
          <div class="container">
              <b-loading class="loading" :is-full-page="isFullPage" :active.sync="loading.isLoading" :can-cancel="true">
                <b-icon id="icon" pack="fas" icon="sync-alt" size="is-medium" custom-class="fa-spin"></b-icon>
              </b-loading>
            <router-view/>
          </div>
          <Tabbar class="is-hidden-desktop is-fixed-bottom"></Tabbar>
        </main>
      </div>
    </section>
  </div>
</template>

<script>
import { mapState } from 'vuex';

import TopNavbar from '@/components/TopNavbar.vue';
import Tabbar from '@/components/Tabbar.vue';

export default {
  name: 'Container',
  data() {
    return {
      isFullPage: true,
      authenticated: false,
      header: ''
    };
  },
  computed: {
    ...mapState({
      user: 'user',
      loading: 'loading',
      comments: 'comments'
    }),

    buttonGroupShow(){
      return this.comments.results !== null ? true : false;
    }
  },
  async created() {
    this.isAuthenticated();
    this.header = this.$route.name.charAt(0).toUpperCase() + this.$route.name.slice(1, this.$route.name.length);
  },
  watch: {
    // Everytime the route changes, check for auth status
    $route: 'isAuthenticated',
    '$route.name': function(name) {
      this.header = name.charAt(0).toUpperCase() + name.slice(1, name.length);
    },
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
      this.$router.push({
        path: '/'
      });

      //TODO: make this an action.
      this.$store.commit({
        type: 'user/UpdateClaims',
        userClaims: null
      });
    }
  },
  components: {
    TopNavbar,
    Tabbar
  }
};
</script>
