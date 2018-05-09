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
      },500);
    } else {
      isLoadingCanceled = true
      state.loading = payload;
    }
  },
  setError(state, payload) {
    payload.dismissed = false;
    state.errors.push(payload);
    setTimeout(() => {
      payload.dismissed = true;
    }, 3000);
  },
  clearError(state, payload) {
    var index = state.errors.findIndex(value => {
      return value == payload;
    });
    if (index != -1) {
      state.errors.splice(index, 1);
    }
  },
  clearErrors(state) {
    state.errors = [];
  }
};
const actions = {
  loading(state) {
    return state.loading;
  },
  error(state) {
    return state.error;
  },
  clearError({ commit, state }, payload) {
    commit("clearError", payload);
  }
};

const getters = {
  loading(state) {
    return state.loading;
  },
  errors(state) {
    return state.errors;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
