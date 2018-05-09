import Vue from "vue";
import Vuex from "vuex";
import UserStore from "./user.js";
import SharedStore from "./shared.js";
Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    user: UserStore,
    shared: SharedStore
  }
});
