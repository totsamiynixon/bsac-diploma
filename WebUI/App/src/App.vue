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

