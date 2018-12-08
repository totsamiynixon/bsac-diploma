<template>
  <v-app>
    <app-alerts></app-alerts>
    <app-loading></app-loading>
    <app-sidebar></app-sidebar>
    <app-toolbar></app-toolbar>
    <v-content>
      <transition name="fade"
                  mode="out-in">
        <router-view />
      </transition>
    </v-content>
  </v-app>
</template>

<script>
import AppToolbar from "@/components/Shared/Layout/Toolbar";
import AppSidebar from "@/components/Shared/Layout/Sidebar";
export default {
  data() {
    return {
      drawer: true
    };
  },
  components: {
    AppToolbar,
    AppSidebar
  },
  name: "App"
};
</script>

<style>
.fade-enter-active,
.fade-leave-active {
  transition-duration: 0.5s;
  transition-property: opacity;
  transition-timing-function: ease;
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}
</style>
