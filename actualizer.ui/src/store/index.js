import Vue from 'vue';
import Vuex from 'vuex';
// Modules:
import user from './modules/user';
import comments from './modules/comments';
import analytics from './modules/analytics';
Vue.use(Vuex);


export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  getters: {},
  modules: {
    user,
    comments,
    analytics
  }
});