import Vuex from "vuex";
import axios from "axios";

const state = {
  profession: {
    id: 0,
    name: ""
  },
  preferredTainingTime: []
};

const mutations = {
  setProfession(state, payload) {
    state.profession = payload;
  },
  setPreferredTrainingTime(state, payload) {
    state.preferredTainingTime = payload;
  },
  setSettings(state, payload) {
    state = payload;
  }
};
const actions = {
  changeProfession({
    commit
  }, payload) {
    axios.post("/api/settigns/profession", payload)
      .then(response => {
        commit("setProfession", payload);
      })
      .catch(error => {});
  },
  changePreferredTrainingTime({
    commit
  }, payload) {
    return new Promise((resolve, reject) => {
      axios.post("/api/settings/preferredTimes", payload)
        .then(result => {
          commit("setProfession", payload);
          resolve();
        })
        .catch(error => {
          regect(error);
        })
    })
  },
  getSettings({
    commit
  }) {
    axios.get("/api/settings").then(
      result => {
        commit("setSettings", result.data)
      },
      error => {}
    );
  }
};

const getters = {
  profession(state) {
    return Object.assign({}, state.profession);
  },
  preferredTainingTime(state) {
    return state.preferredTainingTime.slice();
  },
  all(state) {
    return Object.assign({}, state.settings);
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
