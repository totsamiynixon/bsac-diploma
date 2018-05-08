/*import { store } from "../store";

export default (to, from, next) => {
  let currentProject = store.getters["projects/currentProject"] != null;
  if (currentProject != null && currentProject.id == to.params.id) {
    next();
  } else {
    store.dispatch("projects/loadProject", { id: to.params.id }).then(() => {
      next();
    });
  }
};*/
