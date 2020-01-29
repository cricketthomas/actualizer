import api from '@/api/axios.js';

// this is the controller that calls and updates the comments store.
const state = {
    // isLoading: false,
    results: null
}
const actions = {
    search({ commit }, payload) {
        // state.isLoading = true;
        let uri = `comments/${payload.searchType}?video_id=${payload.video_id}&search=${payload.search}&count=${payload.count}`;
        if (payload.searchType === 'bulk') {
            uri = `comments/${payload.searchType}?video_id=${payload.video_id}&search=${payload.search}`;
        }
        api.get(uri).then(response => {
            commit('SAVE_COMMENTS', response.data);

        }).catch(error => {
            //this.state.isLoading = false;
            throw new Error(`API ${error}`);
        });
    },
};
const mutations = {
    SAVE_COMMENTS(state, data) {
        state.results = data;
        // state.isLoading = false;
    }
};

const getters = {
    results(state) {
        return state.results
    },
   // isLoading: () => state.isLoading
};
const setters = {};

export default {
    state,
    actions,
    mutations,
    getters,
    setters
}