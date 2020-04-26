/* eslint-disable no-console */
import axios from 'axios';
import Vue from 'vue';
import store from '../store/index.js';

const axiosInstance = axios.create({
  baseURL: `${process.env.VUE_APP_API_URL}`,
  timeout: 30000
});


axiosInstance.interceptors.request.use(
  async config => {
    config.headers.Authorization = `Bearer ${await Vue.prototype.$auth.getAccessToken()}`;
    store.dispatch('ChangeLoading');
    return config;
  },
  error => {
    store.dispatch('ChangeLoading');
    return Promise.reject(error);
  }
);


axiosInstance.interceptors.response.use(
  async config => {
    store.dispatch('ChangeLoading');
    return config;
  },
  error => {
    // stop the loading screen bro..
    store.dispatch('ChangeLoading');

    let errorCode = parseInt(error.toString().match(/(\d+)/)[0]);
    let specialMessage = '';
    switch (errorCode) {
      case 400:
        specialMessage = 'is that the right video id..(error YG 400)';
        break;
      case 500:
        specialMessage = 'somethings going down with my code on the backend..(error 500)';
        break;
      case 401:
        specialMessage = 'you need to login..(error 401)';
        break;
      case 403:
        specialMessage = "you don't have permissions to do that..(error 403)";
        break;
      default:
        specialMessage = `${error}`;
        break;
    }
    Vue.prototype.$buefy.toast.open({
      duration: 3000,
      message: `${specialMessage}`,
      position: 'is-top',
      type: 'is-danger'
    });
  }
);
Vue.prototype.$http = axiosInstance;

export default axiosInstance;
