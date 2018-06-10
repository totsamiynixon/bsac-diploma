import Vue from "vue";
import Vuex from "vuex";
import SidebarStore from "./modules/sidebar.js";
import UserStore from "./modules/user.js";
import SharedStore from "./modules/shared.js";
import SettingsStore from "./modules/features/settings";
Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    sidebar: SidebarStore,
    user: UserStore,
    shared: SharedStore,
    features: {
      namespaced: true,
      modules: {
        settings: SettingsStore
      }
    }
  },
  strict: process.env.NODE_ENV !== 'production'
});