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