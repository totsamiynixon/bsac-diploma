<template>
  <v-app>
    <the-app-alerts></the-app-alerts>
    <the-app-loading></the-app-loading>
    <the-app-sidebar></the-app-sidebar>
    <the-app-toolbar></the-app-toolbar>
    <v-content>
      <the-training-notifier></the-training-notifier>
      <router-view />
    </v-content>
  </v-app>
</template>

<script>
import TheAppToolbar from "@/components/Shared/Layout/TheToolbar";
import TheAppSidebar from "@/components/Shared/Layout/TheSidebar";
import TheAppAlerts from "@/components/Shared/TheAlerts";
import TheAppLoading from "@/components/Shared/TheLoading";
import TheTrainingNotifier from "@/components/Shared/TheNotifier";

export default {
    components: {
    TheAppToolbar,
    TheAppSidebar,
    TheAppAlerts,
    TheAppLoading,
    TheTrainingNotifier
  },
  data() {
    return {};
  },
  created() {
    this.$store.subscribe((mutation, state) => {
      if (mutation.type === "user/setToken") {
        if (state.user.token == null) {
          this.$router.push({ name: "auth" });
        }
      } else if (mutation.type === "user/drop") {
        this.$router.push({ name: "auth" });
      } else if (mutation.type === "user/set") {
        this.$router.push({ name: "features" });
      }
    });
    this.$store.dispatch("user/tryRestoreUser");
  },
  name: "App"
};
</script>

<style>
</style>
