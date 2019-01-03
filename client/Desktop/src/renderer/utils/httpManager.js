import axios from 'axios'
import store from '../store'
export default {
  init: () => {
    axios.defaults.baseURL = settings.base_url
    store.subscribe((mutation, state) => {
      if (mutation.type === 'user/setToken') {
        axios.defaults.headers.common.Authorization = `Bearer ${
          state.user.token
        }`
      }
      if (mutation.type === 'user/clearUser') {
        axios.defaults.headers.common.Authorization = ''
      }
    })
    axios.interceptors.request.use(
      config => {
        if (!config.ignoreLoading) {
          store.dispatch('shared/loading', true)
        }
        return config
      },
      error => {
        store.dispatch('shared/error', error)
        store.dispatch('shared/loading', false)
      },
    )

    axios.interceptors.response.use(
      response => {
        store.dispatch('shared/loading', false)
        return response
      },
      error => {
        store.dispatch('shared/loading', false)
        const status = error.response ? error.response.status : null

        if (status === 401) {
          store.dispatch('user/failAuth')
          store.dispatch('shared/alert', {
            message: 'Ошибка аутентификации!',
          })
          return
        }
        store.dispatch('shared/error', error)
      },
    )
  },
}
