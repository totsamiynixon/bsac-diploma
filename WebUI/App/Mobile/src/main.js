// Import Vue
import Vue from 'vue';

// Import F7
import Framework7 from 'framework7/dist/framework7.esm.bundle.js';

// Import F7 Vue Plugin
import Framework7Vue from 'framework7-vue/dist/framework7-vue.esm.bundle.js';

// Import F7 Styles
import Framework7Styles from 'framework7/dist/css/framework7.css';

// Import Icons and App Custom Styles
import IconsStyles from './assets/css/icons.css';
import AppStyles from './assets/css/app.css';

// Import Routes
import Routes from './router'

// Import App Component
import App from './app';

import { store } from "./store";

// Init F7 Vue Plugin
Vue.use(Framework7Vue, Framework7)

// Init App
var app = new Vue({
  el: '#app',
  template: '<app/>',
  // Init Framework7 by passing parameters here
  framework7: {
    id: 'io.framework7.testapp', // App bundle ID
    name: 'Framework7', // App name
    theme: 'auto', // Automatic theme detection
    // App routes
    routes: Routes,
  },
  store,
  // Register App Component
  components: {
    app: App
  }
});
