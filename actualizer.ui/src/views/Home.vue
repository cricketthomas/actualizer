<template>
  <div class="home">

    <Search />
  </div>
</template>

<script>
// @ is an alias to /src
import UserClaims from '@/components/UserClaims.vue';
import Navbar from '@/components/navbar.vue';
import Search from '@/components/SearchComments.vue';

export default {
  name: 'home',
  components: {
    UserClaims,
    Navbar,
    Search
  }
}
</script>
