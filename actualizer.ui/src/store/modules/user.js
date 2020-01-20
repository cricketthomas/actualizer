import api from '@/api/axios.js';

const state = {
    userClaims: {},
    authenticated: false
};
const getters = {};
const actions = {}
const mutations = {
    UpdateClaims(state, payload) {
        state.userClaims = payload.userClaims
    }
};
export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}