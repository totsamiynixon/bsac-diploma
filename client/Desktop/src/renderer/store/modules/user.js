import apiService from '../../utils/apiService'
const state = {
  id: null,
  userName: null,
  roles: [],
  settings: null,
  token: null,
}

const mutations = {
  set(state, payload) {
    state.id = payload.id
    state.userName = payload.userName
    state.roles = payload.roles
    state.settings = payload.settings
  },
  setToken(state, payload) {
    if (payload != null) {
      localStorage.setItem(
        'userAuth',
        JSON.stringify({
          token: payload.token,
          expires: payload.expires,
        }),
      )
      state.token = payload.token
    } else {
      state.token = null
    }
  },
  drop(state) {
    state.id = null
    state.userName = null
    state.roles = null
    state.settings = null
    state.token = null
    localStorage.removeItem('userAuth')
  },
  setSettings(state, payload) {
    state.settings = payload
  },
  setPreferredTrainingTime(state, payload) {
    state.settings.preferredTrainingTime = payload
  },
  setProfession(state, payload) {
    state.settings.profession = payload
  },
}

const actions = {
  // User setup
  setup({ commit }) {
    apiService.getFullUserInfo().then(info => {
      commit('set', info)
    })
  },
  // Authentication
  signUserUp({ dispatch, commit }, payload) {
    apiService
      .signUp(payload.email, payload.password, payload.confirmPassword)
      .then(result => {
        commit('setToken', {
          token: result.token,
          expires: result.expires,
        })
        dispatch('setup')
      })
  },
  signUserIn({ commit, dispatch }, payload) {
    apiService.signIn(payload.email, payload.password).then(result => {
      commit('setToken', {
        token: result.token,
        expires: result.expires,
      })
      dispatch('setup')
    })
  },
  tryRestoreUser({ commit, dispatch }) {
    const userAuth = JSON.parse(localStorage.getItem('userAuth'))
    if (
      userAuth == null ||
      new Date(userAuth.expires) < new Date().getUTCDate()
    ) {
      commit('setToken', null)
      return
    }
    commit('setToken', userAuth)
    dispatch('setup')
  },
  failAuth({ commit }) {
    commit('drop')
  },
  logout({ commit }) {
    commit('drop')
  },
  // Settings
  changeProfession({ commit }, payload) {
    apiService.changeProfession(payload.id).then(result => {
      commit('setProfession', {
        id: payload.id,
        name: payload.name,
      })
    })
  },
  changePreferredTime({ commit }, payload) {
    apiService.savePreferredTime(payload).then(result => {
      commit('setPreferredTrainingTime', payload)
    })
  },
}

const getters = {
  isLoggedIn(state) {
    return state != null && state.id != null && state.token != null
  },
  hasSettingsSetUp(state) {
    return state != null && state.settings && state.settings.profession != null
  },
  profession(state) {
    if (!state) {
      return null
    }
    return state.settings.profession
  },
  trainingTime(state) {
    if (!state || !state.settings) {
      return []
    }
    return state.settings.preferredTrainingTime || []
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
