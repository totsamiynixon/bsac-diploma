import Vue from "vue";
import Vuex from "vuex";
import AdminCriteriaStore from "@/store/modules/admin/criteria.js";
import AdminExerciseStore from "@/store/modules/admin/exercise.js";
import SidebarStore from "@/store/modules/sidebar.js";
import UserStore from "@/store/modules/user.js";
import SharedStore from "@/store/modules/shared.js";
Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    admin: {
      namespaced: true,
      modules: {
        criteria: { 
          ...AdminCriteriaStore,
          namespaced: true
        },
        exercise: {
          ...AdminExerciseStore,
          namespaced: true
        }
      }
    },
    sidebar: SidebarStore,
    user: UserStore,
    shared: SharedStore
  }
});
