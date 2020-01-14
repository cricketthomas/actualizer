import Vue from 'vue';
import App from './App.vue';
import store from '../store';
import router from './router';
import './registerServiceWorker';
import Chart from 'vue2-frappe';
import axios from 'axios';
import Buefy from 'buefy';
import 'buefy/dist/buefy.css';

Vue.use(Buefy);
Vue.use(Chart);

Vue.config.productionTip = false;

const axiosInstance = axios.create({
  baseURL: "https://localhost:5001/api/",
  timeout: 90000,
});

axiosInstance.interceptors.request.use(async config => {   
    config.headers.Authorization = `Bearer ${await Vue.prototype.$auth.getAccessToken()}`;
    return config;
  },
  error => Promise.reject(error)
);

Vue.prototype.$http = axiosInstance;

new Vue({
    store,
    router,
    Chart,
    render: h => h(App)
}).$mount('#app');
