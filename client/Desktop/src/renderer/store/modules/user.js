import Vuex from "vuex";
import Vue from "vue";
const state = {
  id: null,
  userName: null,
  roles: [],
  settings: null,
  token: null
};

const mutations = {
  setUser(state, payload) {
    state.id = payload.id;
    state.userName = payload.userName;
    state.roles = payload.roles;
    state.settings = payload.settings;
  },
  setToken(state, payload) {
    if (payload != null) {
      localStorage.setItem(
        "user_auth",
        JSON.stringify({
          token: payload.token,
          expires: payload.expires
        })
      );
      state.token = payload.token;
    } else {
      state.token = null;
    }
  },
  clearUser(state) {
    state.id = null;
    state.userName = null;
    state.roles = null;
    state.settings = null;
    state.token = null;
    localStorage.removeItem("user_auth");
  },
  setSettings(state, payload) {
    state.settings = payload;
  },
  setPreferredTrainingTime(state, payload) {
    state.settings.preferredTrainingTime = payload;
  },
  setProfession(state, payload) {
    state.settings.profession = payload;
  }
};

const actions = {
  //User setup
  setup({ commit }) {
    Vue.http.get("/api/account/fullinfo").then(response => {
      commit("setUser", response.data);
    });
  },
  //Authentication
  signUserUp({ commit }, payload) {
    Vue.http.post("/api/account/sign-up", payload).then(result => {
      commit("setToken", {
        token: result.data.token,
        expires: result.data.expires
      });
      dispatch("setup");
    });
  },
  signUserIn({ commit, dispatch }, payload) {
    Vue.http
      .post("/api/account/sign-in", {
        email: payload.userName,
        password: payload.password
      })
      .then(result => {
        commit("setToken", {
          token: result.data.token,
          expires: result.data.expires
        });
        dispatch("setup");
      });
  },
  restoreToken({ commit, dispatch }) {
    var user_auth = JSON.parse(localStorage.getItem("user_auth"));
    if (
      user_auth == null ||
      new Date(user_auth.expires) < new Date().getUTCDate()
    ) {
      commit("setToken", null);
      return;
    }
    commit("setToken", user_auth);
    dispatch("setup");
  },
  failAuth({ commit }) {
    commit("clearUser");
  },
  logout({ commit }) {
    commit("clearUser");
  },
  //Settings
  changeProfession({ commit }, payload) {
    Vue.http
      .post("/api/settings/saveProfession", {
        professionId: payload.id
      })
      .then(result => {
        commit("setProfession", {
          id: payload.id,
          name: payload.name
        });
      });
  },
  changePreferredTime({ commit }, payload) {
    Vue.http.post("/api/settings/savePrefferedTime", payload).then(result => {
      commit("setPreferredTrainingTime", payload);
    });
  }
};

const getters = {
  isLoggedIn(state) {
    return state != null && state.id != null && state.token != null;
  },
  hasSettingsSetUp(state) {
    return state != null && state.settings != null;
  },
  profession(state) {
    if (!state) {
      return null;
    }
    return state.settings.profession;
  },
  trainingTime(state) {
    if (!state || !state.settings) {
      return [];
    }
    return state.settings.preferredTrainingTime || [];
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
