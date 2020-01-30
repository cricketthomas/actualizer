const state = {
    isLoading: false
}

const actions = {
    ChangeLoading({ commit }) {
        console.log(`current state is ${state.isLoading}`)
        commit('TOGGLE_LOADING');
    }
}

const mutations = {
    TOGGLE_LOADING(state) {
        state.isLoading = !state.isLoading;
        console.log(`now the  state is ${state.isLoading}`)

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
}