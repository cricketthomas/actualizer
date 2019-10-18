<template>
    <div id="wrapper">
      <div v-if="userClaims"> <h2> Hey {{ userClaims.given_name }}</h2> </div>
      <div v-else> Login..</div>
      <button @click="test()">print test</button>
      <router-view/>
    </div>
</template>

<script>
import axios from 'axios';


export default {
  data() {
    return {
      msg: "test",
      userClaims: null,
    
    }
  },
  async created () {
    axios.defaults.headers.common['Authorization'] = `Bearer ${await this.$auth.getAccessToken()}`
    console.log(`Bearer ${await this.$auth.getAccessToken()}`)
    try {
      this.x = await Object.entries(await this.$auth.getUser()).map(entry => ({ [entry[0]]:  entry[1]} ))
      this.userClaims = this.x.reduce(((k, v) => Object.assign(k, v)), {});
      const response = await axios.get(`https://localhost:5001/api/Count/general/?v=sYQpx5Q5p0Y&s=purple&pageReqCount=1`)
      console.log(this.response)
    } catch (error) {
      console.error(`Errors! ${error}`)
    }
  },
  methods:{
    async test(){
        const response = await axios.get(
        `https://localhost:5001/api/values`
      ) 
      console.log(response)
    }

  }
}
</script>
<style scoped>

</style>