<template>
  <div class="home">

    <Search />
  </div>
</template>

<script>
// @ is an alias to /src
import explorer from '@/components/explorer.vue';
import Navbar from '@/components/navbar.vue';
import Search from '@/components/SearchComments.vue';

export default {
  name: 'home',
  components: {
    explorer,
    Navbar,
    Search
  }
}
</script>
