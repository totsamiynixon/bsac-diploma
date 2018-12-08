<template>
  <v-toolbar color="blue darken-3"
             dark
             app
             :clipped-left="$vuetify.breakpoint.mdAndUp"
             fixed>
    <v-toolbar-title style="width: 300px"
                     class="ml-0 pl-3">
      <v-toolbar-side-icon @click.stop="drawer()" v-show="items.length > 0"></v-toolbar-side-icon>
      <span class="hidden-sm-and-down">Профилактика</span>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn icon
           @click.prevent="logout">
      <v-icon>notifications</v-icon>
    </v-btn>
  </v-toolbar>
</template>

<script>
export default {
  data() {
    return {};
  },
  methods: {
    drawer() {
      this.$store.dispatch("sidebar/changeDrawerState");
    },
    logout() {
      this.$store.dispatch("user/logout");
      this.$router.push("/auth");
    }
  },
  computed: {
    items(){
          return this.$store.getters['sidebar/items']
      }
  },
  name: "AllToolbar"
};
</script>
