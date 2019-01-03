const state = {
  loading: false,
  alerts: []
};
let isLoadingCanceled = false;
const mutations = {
  setLoading(state, payload) {
    state.loading = payload;
  },
  showAlert(state, payload) {
    payload.dismissed = false;
    state.alerts.push(payload);
    setTimeout(() => {
      payload.dismissed = true;
    }, 3000);
  },
  clearAlert(state, payload) {
    const index = state.alerts.findIndex(value => value === payload);
    if (index !== -1) {
      state.alerts.splice(index, 1);
    }
  },
  clearAlerts(state) {
    state.alerts = [];
  }
};
const actions = {
  loading({ commit, state }, payload) {
    if (payload === true) {
      isLoadingCanceled = false;
      setTimeout(() => {
        if (!isLoadingCanceled) {
          commit("setLoading", payload);

        }
      }, 200);
    } else {
      isLoadingCanceled = true;
      commit("setLoading", payload);
    }
  },
  alert({ commit, state }, payload) {
    commit("showAlert", payload);
  },
  error({ commit, state }, payload) {
    const alert = {
      ...payload,
      type: "error"
    };
    commit("showAlert", alert);
  },
  warning({ commit, state }, payload) {
    const alert = {
      ...payload,
      type: "warning"
    };
    commit("showAlert", payload);
  },
  success({ commit, state }, payload) {
    const alert = {
      ...payload,
      type: "success"
    };
    commit("showAlert", payload);
  },
  clearAlert({ commit, state }, payload) {
    commit("clearAlert", payload);
  }
};

const getters = {
  loading(state) {
    return state.loading;
  },
  alerts(state) {
    return state.alerts;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
