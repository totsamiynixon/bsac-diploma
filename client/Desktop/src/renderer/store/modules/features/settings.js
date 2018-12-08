import Vuex from "vuex";
import axios from "axios";
import {
  ipcRenderer
} from "electron"
const state = {
  settings: null
};


const mutations = {
  setSettings(state, payload) {
    state.settings = payload;
  },
  setProfession(state, payload) {
    if (!state.settings) {
      state.settings = {};
    }
    state.settings.profession = payload;

  },
  setPreferredTrainingTime(state, payload) {
    if (!state.settings) {
      state.settings = {};
    }
    state.settings.preferredTrainingTime = payload;
  }
};
const actions = {
  changeProfession({
    commit
  }, payload) {
    axios.post("/api/settings/saveProfession", {
        professionId: payload.id
      })
      .then(result => {
        commit("setProfession", {
          id: payload.id,
          name: payload.name,
        });
      });
  },
  changePreferredTime({
    commit
  }, payload) {
    axios.post("/api/settings/savePrefferedTime", payload)
      .then(result => {
        commit("setPreferredTrainingTime", payload);
      });
  },
  installSettings({
    commit
  }, payload) {
    commit("setSettings", payload);
  }
};

const getters = {
  settings(state) {
    return state.settings;
  },
  profession(state) {
    if (!state.settings) {
      return null;
    }
    return state.settings.profession;
  },
  preferredTime(state) {
    if (!state.settings) {
      return null;
    }
    return state.settings.preferredTrainingTime;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};