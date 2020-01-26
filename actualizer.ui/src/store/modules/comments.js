import api from '@/api/axios.js';

// this is the controller that calls and updates the comments store.
const state = {
    isLoading: false,
    results: {},
}
const actions = {
    search({ commit }, payload) {
        state.isLoading = true;
        let uri = `comments/${payload.searchType}?video_id=${payload.video_id}&search=${payload.search}&count=${payload.count}`;
        if(payload.searchType === 'bulk'){
            uri = `comments/${payload.searchType}?video_id=${payload.video_id}&search=${payload.search}`;
        }
        console.log(uri)

        api.get(uri).then(response => { commit('SAVE_COMMENTS', JSON.parse(response.data)); 
            }).catch(error => { throw new Error(`API ${error}`); });
    },
};
const mutations = {
    SAVE_COMMENTS(state, comments) {
        state.results = comments;
        state.isLoading = false;
    }
};

const getters = {
    results(state) {
        return state.results
    },
    isLoading: () => state.isLoading
};
const setters = {};

export default {
    state,
    actions,
    mutations,
    getters,
    setters
}