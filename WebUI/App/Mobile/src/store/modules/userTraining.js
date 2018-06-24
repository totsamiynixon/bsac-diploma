import Vuex from "vuex";
import axios from "axios"
const state = {
  userTraining: null
};

const mutations = {
  setUserTraining(state, payload) {
    state.userTraining = payload;
    state.userTraining.exercises = state.userTraining.exercises.map(f => {
      f.isPassed = false;
      return f;
    })
  },
  completeExercise(state, payload) {
    var index = state.userTraining.exercises.findIndex(s => s.id == payload);
    if (index != -1) {
      state.userTraining.exercises[index].isPassed = true;
    }
  },
  completeTraining(state, payload) {
    state.userTraining.IsPassed = true;
  }
};
const actions = {
  completeTraining({
    commit,
    state
  }) {
    return new Promise((resolve, reject) => axios.post(
      "/api/userTrainings/complete", {
        userTrainingId: state.userTraining.id
      }).then((response) => {
      commit("completeTraining");
      resolve(response);
    }, error => {
      reject(error);
    }))
  },
  completeExercise({
    commit,
    state
  }, id) {
    return new Promise((resolve, reject) => {
      commit("completeExercise", id);
      resolve();
    }, error => {
      reject(error);
    })
  }
};

const getters = {
  userTraining(state) {
    return state.userTraining;
  },
  exercise: state => id => {
    if (!state.userTraining) {
      return null;
    }
    let index = state.userTraining.exercises.findIndex(s => s.id == id);
    if (index == -1) {
      return null;
    }
    let result = {};
    result.exercise = state.userTraining.exercises[index];
    null;
    if (result.exercise) {
      let index = state.userTraining.exercises.indexOf(result.exercise);
      if (state.userTraining.exercises.length == (index + 1)) {
        result.nextExerciseId = null;
      } else {
        result.nextExerciseId = state.userTraining.exercises[index + 1].id;
      }
      result.totalCount = state.userTraining.exercises.length;
      result.currentIndex = index + 1;
    }
    return result;

  },
  firstExercise: (state, getters) => {
    var id = state.userTraining.exercises[0].id || 0;
    return getters.exercise(id);

  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
