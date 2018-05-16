<template>
  <v-toolbar color="blue darken-3"
             dark
             app
             :clipped-left="$vuetify.breakpoint.mdAndUp"
             fixed>
    <v-toolbar-title style="width: 300px"
                     class="ml-0 pl-3">
      <v-toolbar-side-icon v-show="showMenuIcon"
                           @click.stop="drawer()"></v-toolbar-side-icon>
      <span class="hidden-sm-and-down">Профилактика</span>
    </v-toolbar-title>
    <v-text-field flat
                  solo-inverted
                  prepend-icon="search"
                  label="Поиск"
                  class="hidden-sm-and-down"></v-text-field>
    <v-spacer></v-spacer>
    <v-btn :to="{name:'default'}"
           icon>
      <v-icon>apps</v-icon>
    </v-btn>
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
      this.$router.push("/");
    }
  },
  computed: {
    showMenuIcon() {
      return this.$store.getters["sidebar/showMenuIcon"];
    }
  },
  name: "AllToolbar"
};
</script>
