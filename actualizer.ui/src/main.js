import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import './registerServiceWorker';
import Buefy from 'buefy';
import 'buefy/dist/buefy.css';
import VueApexCharts from 'vue-apexcharts';

Vue.use(Buefy, {
    materialDesignIcons: false,
    defaultIconPack: 'fa'
    
});
Vue.use(VueApexCharts);

Vue.config.productionTip = false;


new Vue({
    store,
    router,
    render: h => h(App)
}).$mount('#app');
