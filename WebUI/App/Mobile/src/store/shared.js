import Vuex from "vuex"
const state = {
  loading: false,
  errors: []
};
var isLoadingCanceled = false;
const mutations = {
  setLoading(state, payload) {
    if (payload == true) {
      isLoadingCanceled = false;
      setTimeout(() => {
        if (!isLoadingCanceled) {
          state.loading = true;
          return;
        }
      }, 500);
    } else {
      isLoadingCanceled = true
      state.loading = payload;
    }
  },
};
const actions = {
  startLoading({
    commit,
    state
  }) {
    commit("setLoading", true)
  },
  stopLoading({
    commit,
    state
  }) {
    commit("setLoading", false)
  }
};

const getters = {
  loading(state) {
    return state.loading;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
