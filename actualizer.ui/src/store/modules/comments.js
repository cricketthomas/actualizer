import api from '@/api/axios.js';

// this is the controller that calls and updates the comments store.
const state = {
    basicSearch: null, // this is the basic route.
    bulkSearch: null
}
const actions = {
    basicSearch({ commit }, payload){
        api.get(`comments/search?video_id=${payload.video_id}&search=${payload.search}&count=${payload.count}`)
            .then(response => {commit('SAVE_BASIC', JSON.parse(response.data) )})
            .catch(error => {
                throw new Error(`API ${error}`);
            });
        },

        bulkSearch({commit}, payload){
        api.get(`comments/bulk?video_id=${payload.video_id}&search=${payload.search}&count=${payload.count}`)
            .then(response => {
                commit('SAVE_BULK', JSON.parse(response.data));
            })
            .catch(error => {
                throw new Error(`API ${error}`);
            });
        }
};
const mutations = {
    SAVE_BASIC(state, comments){
        state.basicSearch = comments;
        console.log(comments)
    },
     SAVE_BULK(state, comments) {
         state.basicSearch = comments;
     }
};

const getters = {
    commentsObj(state) {
        return state.basicSearch
    },
    bulkComments: () => state.bulkSearch
};
const setters = {};

export default {
    state,
    actions,
    mutations,
    getters,
    setters
}