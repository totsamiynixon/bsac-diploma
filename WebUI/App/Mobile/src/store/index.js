import Vue from "vue";
import Vuex from "vuex";
import UserStore from "./user.js";
import SharedStore from "./shared.js";
import SettingsStore from "./modules/settings.js";
Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    user: UserStore,
    shared: SharedStore,
    settings: SettingsStore
  }
});
