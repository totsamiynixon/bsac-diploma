<template>
  <v-app>
    <app-alerts></app-alerts>
    <app-loading></app-loading>
    <app-sidebar></app-sidebar>
    <app-toolbar></app-toolbar>
    <v-content>
      <training-notifier></training-notifier>
      <router-view />
    </v-content>
  </v-app>
</template>

<script>
import AppToolbar from "@/components/Shared/Layout/Toolbar";
import AppSidebar from "@/components/Shared/Layout/Sidebar";
import AppAlerts from "@/components/Shared/Alerts";
import AppLoading from "@/components/Shared/Loading";
import TrainingNotifier from "@/components/Shared/Notifier";

import { HttpManager } from "./utils/httpManager.js";
import { Notifier } from "./utils/notifications.js";
export default {
  data() {
    return {};
  },
  components: {
    AppToolbar,
    AppSidebar,
    AppAlerts,
    AppLoading,
    TrainingNotifier
  },
  created() {
    const httpManager = new HttpManager(this.$http, this.$store);
    httpManager.init();
    const notifier = new Notifier(this.$electron.ipcRenderer, this.$store);
    notifier.initNotifications();
    this.$store.subscribe((mutation, state) => {
      if (mutation.type == "user/setToken") {
        if (state.user.token == null) {
          this.$router.push({ name: "auth" });
        }
      } else if (mutation.type == "user/clearUser") {
        this.$router.push({ name: "auth" });
      } else if (mutation.type == "user/setUser") {
        this.$router.push({ name: "features" });
      }
    });
    this.$store.dispatch("user/restoreToken");
  },
  name: "App"
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons");
/* Global CSS */
.media-holder {
  position: relative;
  height: 0;
  padding-bottom: 56.25%;
  width: 100%;
}

.media-holder iframe {
  position: absolute;
  height: 100%;
  width: 100%;
  left: 0;
  top: 0;
}

.fade-enter-active,
.fade-leave-active {
  transition-duration: 0.3s;
  transition-property: opacity;
  transition-timing-function: ease;
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}
</style>
