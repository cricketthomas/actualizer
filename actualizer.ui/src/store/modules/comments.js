import api from '@/api/axios.js';
import Vue from 'vue';
// this is the controller that calls and updates the comments store.
const state = {
    results: null
}
const actions = {
    search({ commit }, payload) {
        let uri = `comments/${payload.searchType}?video_id=${payload.video_id}&search=${payload.search}&count=${payload.count}`;
        if (payload.searchType === 'bulk') {
            uri = `comments/${payload.searchType}?video_id=${payload.video_id}&search=${payload.search}`;
        }
        api.get(uri).then(response => {
            commit('SAVE_COMMENTS', response.data);
        }).catch(error => {
            throw new Error(`API ${error}`);
        });
    },
    emptyComments({commit}){
        commit('CLEAR_COMMENTS');
    }
};
const mutations = {
    SAVE_COMMENTS(state, data) {
        state.results = data;
    },
    CLEAR_COMMENTS(state) {
        state.results = null;
        Vue.prototype.$buefy.toast.open({
            duration: 3000,
            message: "comments => 🗑",
            position: 'is-top',
            type: 'is-success'
        });
    }
};

const getters = {
    results(state) {
        return state.results;
    },
};
const setters = {};

export default {
    state,
    actions,
    mutations,
    getters,
    setters
}