import Vue from 'vue';
import App from './App.vue';
import store from './store';
import router from './router';
import VueWordCloud from 'vuewordcloud';
import Buefy from 'buefy';
import VueApexCharts from 'vue-apexcharts';
import VueCompositionApi from '@vue/composition-api';
import './registerServiceWorker';
import 'buefy/dist/buefy.css';
import '../node_modules/bulmaswatch/darkly/bulmaswatch.min.css';

import countUp from './scripts/countUp.js';

Vue.use(VueApexCharts);
Vue.component('apexchart', VueApexCharts);
Vue.use(VueCompositionApi);
Vue.component(VueWordCloud.name, VueWordCloud);
Vue.use(Buefy, {
    materialDesignIcons: false,
    defaultIconPack: 'fa'
});

Vue.config.productionTip = false;
Vue.prototype.countUp = countUp;

new Vue({
    store,
    router,
    render: h => h(App)
}).$mount('#app');