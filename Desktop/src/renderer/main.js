import Vue from 'vue'
import axios from 'axios'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.css'

import App from './App'
import router from './router'
import {
  store
} from './store'
import {
  httpConfig
} from "./utils/http"
import {
  NotificationsRunner
} from "./utils/notifications"
import AsyncComputed from 'vue-async-computed'

import AlertsCmp from "./components/Shared/Alerts"
import LoadingCmp from "./components/Shared/Loading"

Vue.use(AsyncComputed)
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
Vue.component("app-alerts", AlertsCmp);
Vue.component("app-loading", LoadingCmp);


if (!process.env.IS_WEB) Vue.use(require('vue-electron'))
Vue.http = Vue.prototype.$http = axios;
Vue.config.productionTip = false;

httpConfig.init();

/* eslint-disable no-new */
new Vue({
  components: {
    App
  },
  router,
  store,
  template: '<App/>'
}).$mount('#app');

NotificationsRunner.initNotifications();

Storage.prototype.setObject = function(key, value) {
  this.setItem(key, JSON.stringify(value));
}

Storage.prototype.getObject = function(key) {
  var value = this.getItem(key);
  return value && JSON.parse(value);
}