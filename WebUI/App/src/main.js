import Vue from "vue";
import App from "./App";
import router from "./router";
import Vuetify from "vuetify";
import { store } from "./store";
import axios from "axios";
import "vuetify/dist/vuetify.min.css";
//Components
import AlertsCmp from "./components/Shared/Alerts.vue";
import LoadingCmp from "./components/Shared/Loading.vue";


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
//Components init
Vue.component("app-alerts", AlertsCmp);
Vue.component("app-loading", LoadingCmp);
Vue.config.productionTip = false;


if(process.env.NODE_ENV == 'production') {
  axios.defaults.baseURL = "/";
 }
 else {
  axios.defaults.baseURL = "http://localhost:57327/";
 }
/* eslint-disable no-new */
new Vue({
  el: "#app",
  router,
  store,
  render: h => h(App),
  created() {
  }
});

