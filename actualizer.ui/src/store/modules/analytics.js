/* eslint-disable no-console */
/* eslint-disable no-irregular-whitespace */
import api from '@/api/axios';

const state = {
    sentiment: {
        dayAggregate: {},
        monthAggregate: {},
        likeAggregate: {},
        textanalyticsbase: null
    },

    keywords: null,
    entity: {}
};

const actions = {
    VaderSentiment({
        commit
    }, payload) {
        let uri = `TextAnalytics/actualizer/${payload.score_type}/${payload.stopword}​`;
        api.post(uri.toString(), payload.documents)
            .then(response => {
                commit('SAVE_VADER', response.data);
            })
            .catch(error => {
                throw new Error(`API ${error}`);
            });
    },
    ActualizerKeyword({commit }, payload ){
        let uri = `TextAnalytics/actualizer/keyphrases?wordthreshold=${payload.threshold}&stopword=${payload.stopword}`;
         api.post(uri.toString(), payload.documents)
             .then(response => {
                 commit('SAVE_PHRASES', response.data);
             })
             .catch(error => {
                 throw new Error(`API ${error}`);
             });
    }

};
const mutations = {
    SAVE_VADER(state, vaderResponse) {
        state.sentiment = vaderResponse;
        // dirty af
        state.sentiment.likeAggregate = vaderResponse.likeAggregate;
        state.sentiment.dayAggregate = vaderResponse.dayAggregate;
        state.sentiment.monthAggregate = vaderResponse.monthAggregate;
        state.sentiment.textanalyticsbase = vaderResponse.textanalyticsbase;

    },
    SAVE_PHRASES(state, keywords) {
        state.keywords = keywords;
    }
};
const getters = {
    daySentiment: (state) => state.dayAggregate,
    keywordsLength: (state) => state.keywords !== null ? state.keywords.length : null

};
const setters = {};

export default {
    state,
    actions,
    mutations,
    getters,
    setters
};