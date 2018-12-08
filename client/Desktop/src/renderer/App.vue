<template>
  <v-app>
    <app-alerts></app-alerts>
    <app-loading></app-loading>
    <app-sidebar></app-sidebar>
    <app-toolbar></app-toolbar>
    <v-content>
      <training-notifier :dialog.sync="notifierDialog"></training-notifier>
      <router-view/>
    </v-content>
  </v-app>
</template>

<script>
import AppToolbar from "@/components/Shared/Layout/Toolbar";
import AppSidebar from "@/components/Shared/Layout/Sidebar";
import TrainingNotifier from "@/components/Shared/Notifier";
export default {
  data() {
    return {
      notifierDialog: false
    };
  },
  components: {
    AppToolbar,
    AppSidebar,
    TrainingNotifier
  },
  mounted() {
    this.$electron.ipcRenderer.on(
      "notify-user-about-training",
      (event, payload) => {
        console.log("Пытаюсь открыть модалку");
        this.notifierDialog = true;
      }
    );
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
</style>
