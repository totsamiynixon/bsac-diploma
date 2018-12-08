import Vuex from "vuex";
import axios from "axios";
const state = {
  loadedCriterias: [],
  currentCriteria: null
};
const mutations = {
  setLoadedCriterias(state, payload) {
    state.loadedCriterias = payload;
  },
  createCriteria(state, payload) {
    state.loadedCriterias.push(payload);
  },
  updateCriteria(state, payload) {
    var index = state.loadedCriterias.findIndex(criteria => {
      return criteria.id == payload.id;
    });
    if (index != -1)
      state.loadedCriterias[index] = payload;
  },
  currentCriteria(state, payload) {
    if (payload == null) {
      payload = {
        id: 0,
        name: ""
      };
    }
    state.currentCriteria = payload;
  },
  removeCriteria(state, payload) {
    var index = state.loadedCriterias.findIndex(val => {
      if (val.id == payload) {
        return true;
      }
    });
    if (index != -1) {
      state.loadedCriterias.splice(index, 1);
    }
  }
};
const actions = {
  loadCriterias({
    commit
  }) {;
    commit("shared/setLoading", true, {
      root: true
    });
    axios.get("/api/admin/criteria/getAll").then(
      response => {
        commit("setLoadedCriterias", response.data);
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
  addOrUpdateCriteria({
    commit,
    getters
  }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    const criteria = {
      name: payload.name,
      id: payload.id
    };
    axios
      .post("/api/admin/criteria/save", criteria)
      .then(response => {
        if (criteria.id == 0) {
          criteria.id = response.data.id;
          commit("createCriteria", criteria);
        } else {
          commit("updateCriteria", criteria);
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
  deleteCriteria({
    commit,
    getters
  }, id) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios
      .delete("/api/admin/criteria/delete", {
        params: { ids: [id] }
      })
      .then(data => {
        commit("removeCriteria", id);
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
  setCurrentCriteria({
    commit,
    getters
  }, criteria) {
    commit("currentCriteria", criteria);
  }
};
const getters = {
  loadedCriterias(state) {
    return state.loadedCriterias;
  },
  currentCriteria(state) {
    return state.currentCriteria;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
