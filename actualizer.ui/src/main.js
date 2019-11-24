import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import './registerServiceWorker';
import Chart from 'vue2-frappe';
import axios from 'axios';


Vue.use(Chart);

Vue.config.productionTip = false;

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



Vue.prototype.$http = base

new Vue({
  store,
  router,
  Chart,
  render: h => h(App)
}).$mount('#app');

