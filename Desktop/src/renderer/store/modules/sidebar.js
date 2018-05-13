import Vuex from 'vuex'

const state = {
  items: [{
    icon: "content_copy",
    text: "Главная",
    route: {
      name: "exercises"
    }
  }],
  showMenu: false,
  showMenuIcon: true
}

// getters
const getters = {
  items: state => state.items,
  showMenu: state => state.showMenu,
  showMenuIcon: state => state.showMenuIcon
}

// actions
const actions = {
  updateSidebarItems({
    commit,
    state
  }, payload) {
    commit('setItems', payload)
  },
  changeDrawerState({
    commit,
    state
  }, payload) {
    commit('changeDrawer', !state.showMenu)
  },
  showSidebar({
    commit,
    state
  }, payload) {
    commit('changeDrawer', true)
  },
  disableSidebar({
    commit,
    state
  }, payload) {
    commit('changeDrawer', false);
    commit('changeMenuIconDisplayState', false);
  },
  enableSidebar({
    commit,
    state
  }, payload) {
    commit('changeDrawer', true);
    commit('changeMenuIconDisplayState', true);
  },
}

// mutations
const mutations = {
  setItems(state, payload) {
    state.items = payload;
  },
  changeDrawer(state, payload) {
    state.showMenu = payload;
  },
  changeMenuIconDisplayState(state, payload) {
    state.showMenuIcon = payload;
  }
}

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}