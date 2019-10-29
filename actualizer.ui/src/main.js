import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import './registerServiceWorker';
import Chart from 'vue2-frappe';
import axios from 'axios'


Vue.use(Chart);

Vue.config.productionTip = false;


new Vue({
  store,
  router,
  Chart,
  render: h => h(App)
}).$mount('#app');

