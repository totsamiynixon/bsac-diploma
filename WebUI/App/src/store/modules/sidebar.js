import Vuex from 'vuex'

const state = {
    items: [],
    drawer: false
}
  
  // getters
  const getters = {
    items: state => state.items,
    drawer: state => state.drawer
  }
  
  // actions
  const actions = {
    updateSidebarItems ({ commit, state }, payload) {
      commit('setItems', payload)
    },
    changeDrawerState ({ commit, state }, payload) {
      commit('changeDrawer',!state.drawer)
    },
    hideSidebar ({ commit, state }, payload) {
      commit('changeDrawer', false)
    },
    showSidebar ({ commit, state }, payload) {
      commit('changeDrawer', true)
    }
  }
  
  // mutations
  const mutations = {
    setItems (state, payload) {
      state.items = payload;
    },
    changeDrawer (state, payload) {
      state.drawer = payload;
    }
  }
  
  export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
  }


  