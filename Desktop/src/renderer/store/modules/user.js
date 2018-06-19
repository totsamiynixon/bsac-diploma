import Vuex from "vuex";
import axios from "axios";

const state = {
  user: null
};

const mutations = {
  setUser(state, payload) {
    state.user = payload;
  }
};
const actions = {
  signUserUp({
    commit
  }, payload) {
    axios.post("/api/account/sign-up", payload)
      .then(result => {
        localStorage.setItem("token", result.data.token);
        axios.defaults.headers.common['Authorization'] = "Bearer " + result
          .data.token;
        commit("setUser", {
          id: result.data.id,
          name: result.data.name,
          roles: result.data.roles,
          settings: null
        });
        commit("features/settings/setSettings", null, {
          root: true
        })
      });
  },
  signUserIn({
    commit
  }, payload) {
    axios.post("/api/account/sign-in", payload)
      .then(result => {
        localStorage.setItem("token", result.data.token);
        axios.defaults.headers.common['Authorization'] = "Bearer " + result
          .data.token;
        commit("setUser", {
          id: result.data.id,
          name: result.data.name,
          roles: result.data.roles
        });
        commit("features/settings/setSettings", result.data.settings, {
          root: true
        })
      });
  },
  autoSignIn({
    commit
  }) {
    return new Promise((resolve, reject) => {
      var token = localStorage.getItem("token");

      if (!token || token == "undefined") {
        reject("Not logged in");
        return;
      }
      axios.defaults.headers.common['Authorization'] = "Bearer " + token;
      axios.get("/api/account/check-login").then(
        result => {
          commit("setUser", {
            id: result.data.id,
            name: result.data.name,
            roles: result.data.roles
          });
          console.log(result.data.settings);
          commit("features/settings/setSettings", result.data.settings, {
            root: true
          })
          resolve(result.data);
        }
      );
    });
  },
  logout({
    commit
  }) {
    return new Promise((resolve, reject) => {
      localStorage.removeItem("token");
      commit("setUser", null);
      resolve();
      return;
    })

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