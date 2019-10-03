<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
  </div>
</template>

<!-- Add "scoped" attribute to limit CSS to this component only -->

<script>
import axios from 'axios';

export default {
  data() {
    return {
      test: "s",
      msg: "test"
    }
  },
  async created () {
    axios.defaults.headers.common['Authorization'] = `Bearer ${await this.$auth.getAccessToken()}`
    try {
      //const response = await axios.get(`http://localhost:{serverPort}/api/messages`)
      this.test = "sucess."
    } catch (e) {
      console.error(`Errors! ${e}`)
    }
  }
}
</script>
<style scoped>