<template>
  <div id="wrapper">
    <div v-if="this.$store.state.userClaims">
      <h2>Hey {{ this.$store.state.userClaims.given_name }}</h2>
    </div>
    <div v-else>Login..</div>

    <!-- <vue-frappe
      id="test"
      :labels="[
        '12am-3am',
        '3am-6am',
        '6am-9am',
        '9am-12pm',
        '12pm-3pm',
        '3pm-6pm',
        '6pm-9pm',
        '9pm-12am'
      ]"
      title="My Awesome Chart"
      type="axis-mixed"
      :height="300"
      :colors="['purple', '#ffa3ef', 'light-blue']"
      :dataSets="this.data"
    >
    </vue-frappe> -->
    <router-view />
  </div>
</template>

<script>
import axios from "axios";
import { store } from "@/store/index.js";

export default {
  data() {
    return {
      msg: "test",
      //userClaims: null,
      data: [
        {
          name: "Some Data",
          chartType: "bar",
          values: [25, 40, 30, 35, 8, 52, 17, -4]
        },
        {
          name: "Another Set",
          chartType: "bar",
          values: [25, 50, -10, 15, 18, 32, 27, 14]
        },
        {
          name: "Yet Another",
          chartType: "line",
          values: [15, 20, -3, -15, 58, 12, -17, 37]
        }
      ]
    };
  },
  async created() {
    //axios.defaults.headers.common["Authorization"] = `Bearer ${await this.$auth.getAccessToken()}`;
    // this.$store.commit({
    //   type: "UpdateBearer",
    //   BearerToken: `Bearer ${await this.$auth.getAccessToken()}`
    // });
    // this.$store.commit({
    //   type: "UpdateClaims",
    //   userClaims: await Object.entries(await this.$auth.getUser()).map(entry => ({[entry[0]]: entry[1] })).reduce((k, v) => Object.assign(k, v), {})
    // });
    // try {
    //   //this.userClaims = await Object.entries(await this.$auth.getUser()).map(entry => ({[entry[0]]: entry[1] })).reduce((k, v) => Object.assign(k, v), {})
    //   const response = await axios.get(
    //     `https://localhost:5001/api/Count/general/?v=sYQpx5Q5p0Y&s=purple&pageReqCount=1`
    //   );
    //   console.log(this.response);
    // } catch (error) {
    //   console.error(`Errors! ${error}`);
    // }
  },
  methods: {}
};
</script>
<style scoped>
</style>