<template>
  <v-toolbar color="blue darken-3"
             dark
             app
             :clipped-left="$vuetify.breakpoint.mdAndUp"
             fixed>
    <v-btn v-show="showMenuIcon"
           @click.native="drawer()"
           icon>
      <v-icon>menu</v-icon>
    </v-btn>
    <v-toolbar-title style="width: 300px"
                     class="ml-0">
      <span class="hidden-sm-and-down">Профилактика</span>
    </v-toolbar-title>
    <v-spacer></v-spacer>
    <v-btn @click.prevent="toDefault"
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
  name: 'AllToolbar',
  data() {
    return {}
  },
  computed: {
    showMenuIcon() {
      return (
        this.$store.getters['sidebar/showMenuIcon'] &&
        this.$store.getters['user/isLoggedIn']
      )
    },
  },
  methods: {
    drawer() {
      this.$store.dispatch('sidebar/changeDrawerState')
    },
    logout() {
      this.$store.dispatch('user/logout')
    },
    toDefault() {
      this.$router.push({ name: 'default' })
    },
  }
}
</script>
