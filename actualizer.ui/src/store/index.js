import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);


export default new Vuex.Store({
  state: {
    baseURL: 'https://localhost:5001/api',
    BearerToken: null,
    userClaims: null,
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

  },
  getters:{
      token: state => state.BearerToken,
  }
});