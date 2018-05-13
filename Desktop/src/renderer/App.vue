<template>
  <v-app>
    <app-alerts></app-alerts>
    <app-loading></app-loading>
    <app-sidebar></app-sidebar>
    <app-toolbar></app-toolbar>
    <v-content>
      <router-view/>
    </v-content>
  </v-app>
</template>

<script>
import AppToolbar from "@/components/Shared/Layout/Toolbar";
import AppSidebar from "@/components/Shared/Layout/Sidebar";
import AuthGuard from "./router/auth-guard.js";
export default {
  data() {
    return {};
  },
  components: {
    AppToolbar,
    AppSidebar
  },
  beforeEnter: AuthGuard,
  name: "App"
};
</script>

<style>
@import url("https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons");
/* Global CSS */
</style>
