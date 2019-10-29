<template>
  <div id="wrapper">
    <fieldset>
      <form @submit.prevent="searchGET()">
        <label>
          Enter a search term:
          <input type="text" id="search" v-model="search" required />
        </label>
        <label>
          Enter Video ID:
          <input type="text" id="video_id" v-model="video_id" />
        </label>
        <label>
          Count:
          <input type="number" id="count" v-model="count" />
        </label>
        <button>Submit</button>
      </form>
    </fieldset>

    {{ results }}
  </div>
</template>

<script>
import axios from "axios";


export default {
  data() {
    return {
      url: this.$store.state.baseURL,
      userClaims: null,
      search: "",
      video_id: "",
      count: 1,
      results:""
      
    };
  },
  methods: {
    async searchGET() {
      axios.defaults.headers.common["Authorization"] = `Bearer ${this.$store.state.BearerToken}`;

      const response = await axios.get(`${this.url}/Search?video_id=${this.video_id}&lang=en&count=${this.count}`);
      console.log(`${this.url}/Search?video_id=${this.video_id}&lang=en&count=${this.count}`)
      this.results = response;
      console.log(response);
    },
    updateTodo() {
      this.$store.commit("updateTodo", this.updatedTodo);
      this.$store.dispatch("updateTodo");
      // updates todo in the component
      this.$store.dispatch("getTodo");
    }
  }
};
</script>
<style scoped>
</style>

