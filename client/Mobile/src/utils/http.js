import axios from "axios"
import {
  store
} from "../store"
export const httpConfig = {
  init() {
    if (process.env.NODE_ENV === 'production') {
      axios.defaults.baseURL =
        "http://18.188.233.49/BSAC_Diploma_Server/";
    }
    axios.interceptors.request.use((config) => {
      store.dispatch("shared/startLoading");
      return config;
    }, (error) => {
      store.dispatch("shared/stopLoading");
      return Promise.reject(error);
    });

    axios.interceptors.response.use(function(response) {
      store.dispatch("shared/stopLoading");
      return response;
    }, function(error) {
      store.dispatch("shared/stopLoading");
      return Promise.reject(error);
    });
  }
}
