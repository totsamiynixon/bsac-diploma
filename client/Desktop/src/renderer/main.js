import Vue from 'vue'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.css'
import AsyncComputed from 'vue-async-computed'

import Notifier from './utils/notifications'
import HttpManager from './utils/httpManager'
import ApiService from './utils/apiService'
import EventBus from './utils/eventBus'

import './assets/style/main.sass'
import './assets/style/animations.sass'
import './assets/fontawesome/css/fontawesome-all.css'

import App from './App'
import router from './router'
import store from './store'

Vue.config.productionTip = false
Vue.config.devtools = true

Vue.use(require('vue-electron'))
Vue.use(AsyncComputed)
Vue.use(Vuetify, {
  theme: {
    primary: '#1565c0',
    secondary: '#424242',
    accent: '#82B1FF',
    error: '#FF5252',
    info: '#2196F3',
    success: '#4CAF50',
    warning: '#FFC107',
  },
})

Vue.config.productionTip = false
Vue.app_settings = Vue.prototype.$app_settings = settings
Vue.eventBus = Vue.prototype.$eventBus = EventBus
Vue.apiService = Vue.prototype.$apiService = ApiService
HttpManager.init()
Notifier.initNotifications(Vue.prototype.$electron.ipcRenderer)

/* eslint-disable no-new */
new Vue({
  components: {
    App,
  },
  router,
  store,
  template: '<App/>',
}).$mount('#app')

/* Enable webpack hot reloading */
if (module.hot) {
  module.hot.accept()
}
