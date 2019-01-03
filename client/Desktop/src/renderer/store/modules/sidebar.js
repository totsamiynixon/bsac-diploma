import Vuex from 'vuex'

const state = {
  items: [
    {
      icon: 'content_copy',
      text: 'Главная',
      route: {
        name: 'exercises',
      },
    },
    {
      icon: 'directions_run',
      text: 'Начать тренировку!',
      route: {
        name: 'training-list',
      },
    },
    {
      icon: 'settings',
      text: 'Настройки',
      route: {
        name: 'settings',
      },
    },
  ],
  showMenu: false,
  showMenuIcon: false,
  miniVariant: false,
}

// getters
const getters = {
  items: state => state.items,
  showMenu: state => state.showMenu,
  showMenuIcon: state => state.showMenuIcon,
  miniVariant: state => state.miniVariant,
}

// actions
const actions = {
  updateSidebarItems({ commit, state }, payload) {
    commit('setItems', payload)
  },
  changeDrawerState({ commit, state }, payload) {
    commit('changeDrawer', !state.showMenu)
  },
  showSidebar({ commit, state }, payload) {
    commit('changeDrawer', true)
  },
  disableSidebar({ commit, state }, payload) {
    commit('changeDrawer', false)
    commit('changeMenuIconDisplayState', false)
  },
  enableSidebar({ commit, state }, payload) {
    commit('changeDrawer', true)
    commit('changeMenuIconDisplayState', true)
  },
  changeMiniVariant({ commit, state }, payload) {
    commit('changeMiniVariant', !state.miniVariant)
  },
}

// mutations
const mutations = {
  setItems(state, payload) {
    state.items = payload
  },
  changeDrawer(state, payload) {
    state.showMenu = payload
  },
  changeMenuIconDisplayState(state, payload) {
    state.showMenuIcon = payload
  },
  changeMiniVariant(state, payload) {
    state.miniVariant = payload
  },
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations,
}
