import {
  store
} from '../store'

export default (to, from, next) => {
  if (store.getters['user/user']) {
    if (store.getters['features/settings/settings'] == null && to.name !=
      "settings") {
      next({
        name: "settings"
      });
      return;
    }
    next();
  } else {
    store.dispatch("user/autoSignIn").then((resolve) => {
      next()
    }, (reject) => {
      next("/auth/signin");
    })
  }
}