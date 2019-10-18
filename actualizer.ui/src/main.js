import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import './registerServiceWorker';

import ECharts from 'vue-echarts'; // refers to components/ECharts.vue in webpack
import 'echarts/lib/chart/bar';
import 'echarts/lib/component/tooltip';

Vue.component('v-chart', ECharts);

Vue.config.productionTip = false;

new Vue({
  store,
  router,
  render: h => h(App)
}).$mount('#app');
