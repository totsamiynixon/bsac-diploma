import Vue from "vue";
import Vuetify from "vuetify";
import AsyncComputed from "vue-async-computed";
import axios from "axios";
import "vuetify/dist/vuetify.css";

import App from "./App";
import { store } from "./store";
import router from "./router";
import { Notifier } from "./utils/notifications";
import { HttpManager } from "./utils/httpManager";
import { EventEmitter } from "events";

Vue.use(AsyncComputed);
Vue.use(Vuetify, {
  theme: {
    primary: "#1565c0",
    secondary: "#424242",
    accent: "#82B1FF",
    error: "#FF5252",
    info: "#2196F3",
    success: "#4CAF50",
    warning: "#FFC107"
  }
});

if (!process.env.IS_WEB) { 
  
  Vue.use(require("vue-electron"));
  new Notifier(
    Vue.prototype.$electron.ipcRenderer,
    store,
    Vue.prototype.$eventBus 
  ).initNotifications();
}

Vue.http = Vue.prototype.$http = axios.create();
Vue.config.productionTip = false;
Vue.app_settings = Vue.prototype.$app_settings = settings;
Vue.eventBus = Vue.prototype.$eventBus = new EventEmitter();
new HttpManager(Vue.prototype.$http, store).init();

new Vue({
  components: {
    App
  },
  router,
  store,
  template: "<App/>"
}).$mount("#app");
