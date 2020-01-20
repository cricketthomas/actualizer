import axios from 'axios';
import Vue from 'vue';

const axiosInstance = axios.create({
    baseURL: "https://localhost:5001/api/",
    timeout: 90000,
});

axiosInstance.interceptors.request.use(async config => {
        config.headers.Authorization = `Bearer ${await Vue.prototype.$auth.getAccessToken() }`;
        return config;
    },
    error => Promise.reject(error));

Vue.prototype.$http = axiosInstance;

export default axiosInstance;
