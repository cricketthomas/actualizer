<template>
    <div id="wrapper">
      <p>Enter a search term:</p>
      <input type="text">
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