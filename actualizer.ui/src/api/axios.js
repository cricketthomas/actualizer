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




axiosInstance.interceptors.response.use(async config => {
            return config;
        },
        error => {
           Vue.prototype.$buefy.snackbar.open({
                duration: 5000,
                message: `Somethings up..${error}`,
                type: 'is-danger',
                position: 'is-bottom-left',
                actionText: 'Login',
                queue: false,
                onAction: () => {
                    Vue.prototype.$buefy.toast.open({
                        message: 'Action pressed',
                        queue: false
                    })
                }
            })



        }

)
Vue.prototype.$http = axiosInstance;

export default axiosInstance;