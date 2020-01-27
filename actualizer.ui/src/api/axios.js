import axios from 'axios';
import Vue from 'vue';
import store from '../store/index.js';

const axiosInstance = axios.create({
    baseURL: "https://localhost:5001/api/",
    timeout: 30000
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
            // stop the loading screen bro..
            store.state.comments.isLoading = false

            let errorCode = parseInt(error.toString().match(/(\d+)/)[0])
            let specialMessage = '';
            switch (errorCode) {
                case 400:
                    specialMessage = 'is that the right video id..(error YG 400)'
                    break;
                case 500:
                    specialMessage = 'somethings going down with my code on the backend..(error 500)'
                    break;
                case 401:
                    specialMessage = 'you need to login..(error 401)'
                    break;
                case 403:
                    specialMessage = "you don't have permissions to do that..(error 403)"
                    break;
                default:
                    specialMessage = `${error}`
                    break;
            }
            Vue.prototype.$buefy.toast.open({
                duration: 4000,
                message: `${specialMessage}`,
                position: 'is-bottom-left',
                type: 'is-danger'
            })
        }

)
Vue.prototype.$http = axiosInstance;

export default axiosInstance;