import Vuex from "vuex";
import axios from "axios";
const state = {
  loadedProfessions: [],
  currentProfessionData: null
};
const mutations = {
  setLoadedProfessions(state, payload) {
    state.loadedProfessions = payload;
  },
  setLoadedProfession(state, payload) {
    state.currentProfessionData = payload;
  },
  createProfession(state, payload) {
    state.loadedProfessions.push(payload);
  },
  updateProfession(state, payload) {
    var index = state.loadedProfessions.findIndex(profession => {
      return profession.id == payload.id;
    });
    if (index != -1)
      state.loadedProfessions[index] = payload;
  },
  removeProfession(state, payload) {
    var index = state.loadedProfessions.findIndex(val => {
      if (val.id == payload) {
        return true;
      }
    });
    if (index != -1) {
      state.loadedProfessions.splice(index, 1);
    }
  }
};
const actions = {
  loadProfessions({
    commit
  }) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios.get("/api/admin/profession/getAll").then(
      response => {
        commit("setLoadedProfessions", response.data);
        commit("shared/setLoading", false, {
          root: true
        });
      },
      error => {
        commit("shared/setLoading", false, {
          root: true
        });
        commit("shared/setError", error, {
          root: true
        });
      }
    );
  },
  loadProfession({
    commit
  }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    var params = payload == null ? null : {
      id: payload.id
    }
    axios.get("/api/admin/profession/get", { params: params }).then(
      response => {
        commit("setLoadedProfession", response.data);
        commit("shared/setLoading", false, {
          root: true
        });
      },
      error => {
        commit("shared/setLoading", false, {
          root: true
        });
        commit("shared/setError", error, {
          root: true
        });
      }
    );
  },
  addOrUpdateProfession({
    commit,
    getters
  }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios
      .post("/api/admin/profession/save", payload)
      .then(response => {
        if (payload.id == 0) {
          payload.id = response.data
          commit("createProfession", payload);
        } else {
          commit("updateProfession", payload);
        }
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
    // Reach out to firebase and store it
  },
  deleteProfession({
    commit,
    getters
  }, id) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios
      .delete("/api/admin/profession/delete", {
        params: { ids: [id] }
      })
      .then(data => {
        commit("removeProfession", id);
        commit("shared/setLoading", false, {
          root: true
        });
      })
      .catch(error => {
        commit("shared/setError", error, {
          root: true
        });
        commit("shared/setLoading", false, {
          root: true
        });
      });
  },
  setCurrentProfession({
    commit,
    getters
  }, profession) {
    commit("currentProfessionData", profession);
  }
};
const getters = {
  loadedProfessions(state) {
    return state.loadedProfessions;
  },
  currentProfessionData(state) {
    return state.currentProfessionData;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
