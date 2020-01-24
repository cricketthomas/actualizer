import api from '@/api/axios';

const state = {
    sentiment: {},
    keyPhrases: {},
    entity: {}
};

const actions = {
    VaderSentiment({
        commit
    }, payload) {
        // eslint-disable-next-line no-irregular-whitespace
        api.post(`TextAnalytics​/vader​/${payload.score_type}​/${payload.stopword}`)
            .then(response => {
                commit('SAVE_VADER', JSON.parse(response.data));
            })
            .catch(error => {
                throw new Error(`API ${error}`);
            });
}
};
const mutations = {
    SAVE_VADER(state, vaderResponse) {
        state.sentiment = vaderResponse;
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
};