import api from '@/api/axios.js';
import Vue from 'vue';
// this is the controller that calls and updates the comments store.
const state = {
 basicSearch: null // this is the basic route.

}
const actions = {
    basicSearch({ commit }, payload){
    api.get(`comments/search?video_id=${payload.video_id}&search=${payload.search}&count=${payload.count}`)
        .then(response => {commit('SAVE_BASIC', JSON.parse(response.data) )})
        .catch(error => {
            throw new Error(`API ${error}`);
        });
    }
};
const mutations = {
    SAVE_BASIC(state, comments){
        state.basicSearch = comments;
    }
};

const getters = {};
const setters = {};

export default {
    state,
    actions,
    mutations,
    getters,
    setters
}