import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import './registerServiceWorker';
import Buefy from 'buefy';
import 'buefy/dist/buefy.css';
import api from "./api/axios";
import VueApexCharts from 'vue-apexcharts';

Vue.use(Buefy);
Vue.use(VueApexCharts);

Vue.config.productionTip = false;


new Vue({
    store,
    router,
    render: h => h(App)
}).$mount('#app');
