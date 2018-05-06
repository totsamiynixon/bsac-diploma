import {store} from '../store'

export default (to, from, next) => {
  if (store.getters['user/user']) {
    next()
  } else {
    store.dispatch("user/autoSignIn").then((resolve)=>{
      next()
    },(reject)=>{
      next("auth/signin")
    })
  }
}
