import Vuex from "vuex"
const state = {
  loading: false,
  alerts: []
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
  showAlert(state, payload) {
    payload.dismissed = false;
    state.alerts.push(payload);
    setTimeout(() => {
      payload.dismissed = true;
    }, 3000);
  },
  clearAlert(state, payload) {
    var index = state.alerts.findIndex(value => {
      return value == payload;
    });
    if (index != -1) {
      state.alerts.splice(index, 1);
    }
  },
  clearAlerts(state) {
    state.alerts = [];
  }
};
const actions = {
  loading({
    commit,
    state
  }, payload) {
    commit("setLoading", payload);
  },
  alert({
    commit,
    state
  }, payload) {
    commit("showAlert", payload);
  },
  error({
    commit,
    state
  }, payload) {
    let alert = {
      ...payload,
      type: "error"
    }
    commit("showAlert", alert);
  },
  warning({
    commit,
    state
  }, payload) {
    let alert = {
      ...payload,
      type: "warning"
    }
    commit("showAlert", payload);
  },
  success({
    commit,
    state
  }, payload) {
    let alert = {
      ...payload,
      type: "success"
    }
    commit("showAlert", payload);
  },
  clearAlert({
    commit,
    state
  }, payload) {
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