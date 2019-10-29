import Vue from 'vue';
import Vuex from 'vuex';
import axios from "axios";

Vue.use(Vuex);


export default new Vuex.Store({
  state: {
    baseURL: 'https://localhost:5001/api',
    BearerToken: '',
    userClaims: null
  },
  mutations: {
    UpdateBearer (state, payload) {
      state.BearerToken = payload.BearerToken
    },
    UpdateClaims (state, payload) {
      state.userClaims = payload.userClaims
    }
  },
  actions: {

  }
});