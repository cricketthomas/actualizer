<template>
  <div id="wrapper">
    <fieldset>
      <form @submit.prevent="SearchComments()">
        <label>
          Enter a search term:
          <input type="text" id="search" v-model="query.search" required />
        </label>
        <label>
          Enter Video ID:
          <input type="text" id="video_id" v-model="query.video_id" />
        </label>
        <label>
          Count:
          <input type="number" id="count" v-model.number="query.count" />
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
      results: "",
      query: {
        search: "",
        video_id: "",
        count: 10
      }
    };
  },
  methods: {
    async SearchComments() {
      //TODO: put this in a getter.

      this.$http.get(`comments/search?video_id=${this.query.video_id}&search=${this.query.search}&lang=en&count=${this.query.count}`)
        .then(response => {
          this.results = response.data;
        })
        .catch(e => {
          console.error(e);
        });

      console.log(
        `${this.url}/comments/search?video_id=${this.video_id}&lang=en&count=${this.count}`
      );
      //this.results = response;
      //console.log(response);
    },
    updateTodo() {
      this.$store.commit("updateTodo", this.updatedTodo);
      this.$store.dispatch("updateTodo"); // updates todo in the component
      this.$store.dispatch("getTodo");
    }
  }
};
</script>
<style scoped>
</style>

