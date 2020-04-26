import Vue from 'vue';
import Vuex from 'vuex';
// Modules:
import user from './modules/user';
import comments from './modules/comments';
import analytics from './modules/analytics';
import loading from './modules/loading';



import VuexPersist from 'vuex-persist';




const vuexLocalStorage = new VuexPersist({
  key: 'vuex' ,// The key to store the state on in the storage provider.
  storage: window.localStorage, // or window.sessionStorage or localForage
  // Function that passes the state and returns the state with only the objects you want to store.
  reducer: state => ({
    user: state.user,
    comments: state.comments
  }),
  // Function that passes a mutation and lets you decide if it should update the state in localStorage.
  // filter: mutation => (true)
});




Vue.use(Vuex);

export default new Vuex.Store({
  state: {},
  mutations: {},
  actions: {},
  getters: {},
  modules: {
    user,
    comments,
    analytics,
    loading
  },
  plugins: [vuexLocalStorage.plugin]
});