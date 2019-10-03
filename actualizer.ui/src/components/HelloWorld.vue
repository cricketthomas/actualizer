<template>
    <div id="wrapper">
      <div v-if="userClaims"> <h2> Hey {{ userClaims.given_name }}</h2> </div>
      <div v-else> Login..</div>
      <router-view/>
    </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      msg: "test",
      userClaims:null,

    }
  },
  async created () {
    axios.defaults.headers.common['Authorization'] = `Bearer ${await this.$auth.getAccessToken()}`
    console.log(`Bearer ${await this.$auth.getAccessToken()}`)
    try {
      this.x = await Object.entries(await this.$auth.getUser()).map(entry => ({ [entry[0]]:  entry[1]} ))
      this.userClaims = this.x.reduce(((k, v) => Object.assign(k, v)), {});

      //const response = await axios.get(`http://localhost:{serverPort}/api/messages`)
    } catch (error) {
      console.error(`Errors! ${error}`)
    }
  }
}
</script>
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>