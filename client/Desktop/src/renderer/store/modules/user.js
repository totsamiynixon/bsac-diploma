import Vuex from "vuex";
import axios from "axios";

const state = {
  user: null
};

var user_auth = JSON.parse(localStorage.getItem("user_auth"));
if (
  user_auth != null &&
  new Date(user_auth.expires) > new Date().getUTCDate()
) {
  state.user = {
    id: user_auth.id,
    name: user_auth.name,
    roles: user_auth.roles
  };
  axios.defaults.headers.common["Authorization"] = "Bearer " + user_auth.token;
}

const mutations = {
  setUser(state, payload) {
    state.user = {
      id: payload.id,
      name: payload.name,
      roles: payload.roles
    };
    localStorage.setItem("user_auth", JSON.stringify(payload));
    axios.defaults.headers.common["Authorization"] = "Bearer " + payload.token;
  },
  clearUser(state) {
    state.user = null;
    localStorage.removeItem("user_auth");
    axios.defaults.headers.common["Authorization"] = "";
  }
};

const actions = {
  signUserUp({ commit }, payload) {
    axios.post("/api/account/sign-up", payload).then(result => {
      localStorage.setItem("token", result.data.token);
      const newUser = {
        id: result.data.id,
        name: result.data.name,
        roles: result.data.roles,
        token: result.data.token,
        expires: result.data.expires
      };
      commit("setUser", newUser);
      commit("features/settings/setSettings", null, {
        root: true
      });
    });
  },
  signUserIn({ commit }, payload) {
    axios.post("/api/account/sign-in", payload).then(result => {
      commit("setUser", {
        id: result.data.id,
        name: result.data.name,
        roles: result.data.roles,
        token: result.data.token,
        expires: result.data.expires
      });
      commit("features/settings/setSettings", result.data.settings, {
        root: true
      });
    });
  },
  logout({ commit }) {
    return new Promise((resolve, reject) => {
      commit("clearUser");
      resolve();
      return;
    });
  }
};

const getters = {
  user(state) {
    return state.user;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
