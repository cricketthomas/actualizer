<template>
  <div>
    <nav class="navbar is-fixed-bottom bottom" role="navigation">
      <div class="navbar-brand ">
        <router-link class="navbar-item is-expanded is-block has-text-centered" to="/dashboard/search">
          <i class="fa fa-search"></i>
          <p class="is-size-9 items">Search</p>
        </router-link>
        <router-link class="navbar-item is-expanded is-block has-text-centered" to="/dashboard/analytics">
          <i class="fa fa-chart-area"></i>
          <p class="is-size-9 items">Analyze</p>
        </router-link>
        <router-link class="navbar-item is-expanded is-block has-text-centered" to="/dashboard/settings">
          <i class="fa fa-cog"></i>
          <p class="is-size-9 items">Settings</p>
        </router-link>
        <router-link class="navbar-item is-expanded is-block has-text-centered" to="/dashboard/about">
          <i class="fa fa-info"></i>
          <p class="is-size-9 items">About</p>
        </router-link>
        <!-- <router-link class="navbar-item is-expanded is-block has-text-centered" to="/" exact>
          <i class="fa fa-home"></i>
          <p class="is-size-9 items">Home</p>
        </router-link> -->
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: 'Tabbar',
  data() {
    return {};
  }
};
</script>
<style lang="scss">
.bottom {
  //background: rgba($color: #000000, $alpha: .6);
  border-bottom-right-radius: 0 !important;
  border-bottom-left-radius: 0 !important;
}
.items {
  margin-bottom: .9rem;
}
</style>
