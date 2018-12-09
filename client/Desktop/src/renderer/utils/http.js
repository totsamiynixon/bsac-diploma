import axios from "axios";
import { store } from "../store";
export const httpConfig = {
  init() {
    axios.defaults.baseURL = settings.base_url;
    axios.interceptors.request.use(
      config => {
        if (!config.ignoreLoading) {
          store.dispatch("shared/loading", true);
        }
        return config;
      },
      error => {
        store.dispatch("shared/error", error);
        store.dispatch("shared/loading", false);
      }
    );

    axios.interceptors.response.use(
      response => {
        store.dispatch("shared/loading", false);
        return response;
      },
      error => {
        store.dispatch("shared/error", error);
        store.dispatch("shared/loading", false);
      }
    );
  }
};
