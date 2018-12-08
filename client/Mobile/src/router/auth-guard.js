import {store} from '../store'

export default (event, page) => {
  if (store.getters['user/user']) {
    app.f7.router.navigate(page);
  } else {
    store.dispatch("user/autoSignIn").then((resolve)=>{
      app.f7.router.navigate(page);
    },(reject)=>{
        app.f7.router.navigate("/auth/sign-in");
    })
  }
}
