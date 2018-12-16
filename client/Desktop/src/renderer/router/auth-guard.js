import { store } from "../store";

export default (to, from, next) => {
  console.log(to, from, store);
  if (store.getters["user/isLoggedIn"]) {
    if (!store.getters["user/hasSettingsSetUp"] && to.name != "settings") {
      next({
        name: "settings"
      });
      return;
    }
    next();
  } else {
    next({
      name: "auth"
    });
  }
};
