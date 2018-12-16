export function HttpManager($http, $store) {
  this.init = function() {
    $http.defaults.baseURL = settings.base_url;
    $store.subscribe((mutation, state) => {
      if (mutation.type == "user/setToken") {
        $http.defaults.headers.common["Authorization"] =
          "Bearer " + state.user.token;
      }
      if (mutation.type == "user/clearUser") {
        $http.defaults.headers.common["Authorization"] = "";
      }
    });
    $http.interceptors.request.use(
      config => {
        if (!config.ignoreLoading) {
          $store.dispatch("shared/loading", true);
        }
        return config;
      },
      error => {
        $store.dispatch("shared/error", error);
        $store.dispatch("shared/loading", false);
      }
    );

    $http.interceptors.response.use(
      response => {
        $store.dispatch("shared/loading", false);
        return response;
      },
      error => {
        $store.dispatch("shared/loading", false);
        const status = error.response ? error.response.status : null;

        if (status === 401) {
          $store.dispatch("user/failAuth");
          $store.dispatch("shared/alert", {
            message: "Ошибка аутентификации!"
          });
          return;
        }
        $store.dispatch("shared/error", error);
      }
    );
  };
}
