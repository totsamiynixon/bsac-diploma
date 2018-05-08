import Vuex from "vuex";
import axios from "axios";
const state = {
  loadedExercises: [],
  currentExerciseData: null
};
const mutations = {
  setLoadedExercises(state, payload) {
    state.loadedExercises = payload;
  },
  setLoadedExercise(state, payload) {
    state.currentExerciseData = payload;
  },
  createExercise(state, payload) {
    state.loadedExercises.push(payload);
  },
  updateExercise(state, payload) {
    var index = state.loadedExercises.findIndex(exercise => {
      return exercise.id == payload.id;
    });
    if (index != -1)
      state.loadedExercises[index] = payload;
  },
  removeExercise(state, payload) {
    var index = state.loadedExercises.findIndex(val => {
      if (val.id == payload) {
        return true;
      }
    });
    if (index != -1) {
      state.loadedExercises.splice(index, 1);
    }
  }
};
const actions = {
  loadExercises({
    commit
  }) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios.get("/api/admin/exercise/getAll").then(
      response => {
        commit("setLoadedExercises", response.data);
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
  loadExercise({
    commit
  }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    var params = payload == null ? null : {
      id:payload.id
    }
    axios.get("/api/admin/exercise/get", { params:params }).then(
      response => {
        commit("setLoadedExercise", response.data);
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
  addOrUpdateExercise({
    commit,
    getters
  }, payload) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios
      .post("/api/admin/exercise/save", payload)
      .then(response => {
        if (exercise.id == 0) {
          exercise.id = response.data
          commit("createExercise", exercise);
        } else {
          commit("updateExercise", exercise);
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
  deleteExercise({
    commit,
    getters
  }, id) {
    commit("shared/setLoading", true, {
      root: true
    });
    axios
      .delete("/api/admin/exercise/delete", {
        ids: [id]
      })
      .then(data => {
        commit("removeExercise", id);
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
  setCurrentExercise({
    commit,
    getters
  }, exercise) {
    commit("currentExerciseData", exercise);
  }
};
const getters = {
  loadedExercises(state) {
    return state.loadedExercises;
  },
  currentExerciseData(state) {
    return state.currentExerciseData;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
