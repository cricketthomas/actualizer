import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import './registerServiceWorker';
import Chart from 'vue2-frappe';
import axios from 'axios';
import Buefy from 'buefy'
import 'buefy/dist/buefy.css'

Vue.use(Buefy)
Vue.use(Chart);

Vue.config.productionTip = false;
//todo add interceptor then put that in the prototype.
try{
  var token = JSON.parse(localStorage.getItem('okta-token-storage')).accessToken.accessToken;

}catch{
  console.info("no token in local storage");
}

if(token == undefined || token == null){
  token = "";
}

const base = axios.create({
  baseURL: 'https://localhost:5001/api',
  headers: {
    Authorization: `Bearer ${token}`
  }
});

// const Actualizerconfig = axios.create({
//   baseURL: 'https://localhost:5001/api'
// });

// async function MakeInterceptor(config){
//   axios.interceptors.request.use(function (config) {
//     config.headers.Authorization = `Bearer ${Vue.prototype.$auth.getAccessToken()}`;
//     return config;
//   }, function (error) {
//     return Promise.reject(error);
//   });
// }


// Vue.prototype.$http = MakeInterceptor(Actualizerconfig);


Vue.prototype.$http = base

new Vue({
  store,
  router,
  Chart,
  render: h => h(App)
}).$mount('#app');

