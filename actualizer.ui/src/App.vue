<template>
  <div id="app">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link>
    <router-link to="/" tag="button" id='home-button'> Home </router-link>
    <button v-if='authenticated' v-on:click='logout' id='logout-button'> Logout </button>
    <button v-else v-on:click='login' id='login-button'> Login </button>
    <Search/>

<hr>
<v-chart :options="chartOptionsBar"/>
<hr>


    <router-view/>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'
import Search from './components/SearchComments.vue'
import axios from 'axios';


import ECharts from 'vue-echarts'
import 'echarts/lib/chart/line'
import 'echarts/lib/component/polar'

export default {
  components: {
    'v-chart': ECharts
  },
  name: 'app',
  data: function () {
    
    return {
      chartOptionsBar: {
  xAxis: {
    data: ['Q1', 'Q2', 'Q3', 'Q4']
  },
  yAxis: {
    type: 'value'
  },
  series: [
    {
      type: 'bar',
      data: [63, 75, 24, 92]
    }
  ]
},
      authenticated: false
    }
  },
  created () {
    this.isAuthenticated()
  },
  watch: {
    // Everytime the route changes, check for auth status
    '$route': 'isAuthenticated'
  },
  methods: {
    async isAuthenticated () {
      this.authenticated = await this.$auth.isAuthenticated()
    },
    login () {
      this.$auth.loginRedirect('/')
    },
    async logout () {
      await this.$auth.logout()
      await this.isAuthenticated()
      // Navigate back to home
      this.$router.push({ path: '/' })
    }
  }, 
    components: {
    HelloWorld,
    Search
  },
}
</script>


<style>

</style>
