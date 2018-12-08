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
  },
  clearUser(state) {
    state.user = null;
    localStorage.removeItem("user_auth");
  }
};

const actions = {
  signUserUp({ commit }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios
      .post("/api/account/sing-up", payload)
      .then(user => {
        const newUser = {
          id: result.data.id,
          name: result.data.name,
          roles: result.data.roles,
          token: result.data.token,
          expires: result.data.expires
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
  signUserIn({ commit }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios
      .post("/api/account/sign-in", payload)
      .then(result => {
        var token = localStorage.setItem("token", result.data.token);
        commit("setUser", {
          id: result.data.id,
          name: result.data.name,
          roles: result.data.roles,
          token: result.data.token,
          expires: result.data.expires
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
      });
  },
  logout({ commit }) {
    commit("clearUser");
    commit("sidebar/setItems", [], {
      root: true
    });
    commit("sidebar/changeDrawer", false, {
      root: true
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
