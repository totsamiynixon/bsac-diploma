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
    commit("shared/setLoading", true, {
      root: true
    });
    axios.post("/api/account/sing-up", payload)
      .then(user => {
        const newUser = {
          id: user.id,
          roles: user.roles
        };
        commit("setUser", newUser);
        commit("shared/setLoading", false, {
          root: true
        });
      })
      .catch(error => {
        commit("shared/setLoading", false, {
          root: true
        });
        commit("shared/setError", error, {
          root: true
        });
      });
  },
  signUserIn({
    commit
  }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios.post("/api/account/sign-in", payload)
      .then(result => {
        var token = localStorage.setItem("token", result.data.token);
        commit("setUser", {
          id: result.data.id,
          name: result.data.name,
          roles: result.data.roles
        });
        commit("shared/setLoading", false, {
          root: true
        });
      })
      .catch(error => {
        commit("shared/setLoading", false, {
          root: true
        });
        commit("shared/setError", error, {
          root: true
        });

      })
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
      commit("shared/setLoading", true, {
        root: true
      });
      axios.defaults.headers.common['Authorization'] = "Bearer " + token;
      axios.get("/api/account/check-login").then(
        user => {
          commit("setUser", user);
          resolve(user);
          commit("shared/setLoading", false, {
            root: true
          });
        },
        error => {
          reject(error);
          commit("shared/setLoading", false, {
            root: true
          });
          commit("shared/setError", error, {
            root: true
          });
        }
      );
    });
  },
  logout({
    commit
  }) {
    localStorage.removeItem("token");
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
