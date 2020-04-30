
// This was a horrible idea. Never put loading in the state of your app. 
// Why did i think this was a good idea?

const state = {
    isLoading: false
}

const actions = {
    ChangeLoading({ commit }) {
        // eslint-disable-next-line no-console
        commit('TOGGLE_LOADING');
    }
}

const mutations = {
    TOGGLE_LOADING(state) {
        state.isLoading = !state.isLoading;
        //console.log(`now the  state is ${state.isLoading}`)
    }
};

const getters = {
    isLoading: () => state.isLoading
}

export default {
    state,
    actions,
    getters,
    mutations
};