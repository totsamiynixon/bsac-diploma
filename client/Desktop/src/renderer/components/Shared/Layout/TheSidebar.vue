<template>
  <v-navigation-drawer fixed
                       :clipped="true"
                       :mini-variant="miniVariant"
                       @input="drawerChange"
                       app
                       :value="showMenu">
    <v-layout justify-space-between
              wrap
              column
              style="height:100%;">
      <v-list dense>
        <template v-for="item in items">
          <v-list-tile :to="item.route"
                       :key="item.text">
            <v-list-tile-action>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-tile-action>
            <v-list-tile-content>
              <v-list-tile-title>
                {{ item.text }}
              </v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </template>
      </v-list>
      <v-spacer></v-spacer>
      <v-divider></v-divider>
      <v-btn icon
             @click="changeMiniVariant()">
        <v-icon v-if="!miniVariant">arrow_left</v-icon>
        <v-icon v-if="miniVariant">arrow_right</v-icon>
      </v-btn>
    </v-layout>
  </v-navigation-drawer>
</template>

<script>
export default {
  name: 'AppSidebar',
  data() {
    return {}
  },
  computed: {
    showMenu: {
      get() {
        return this.$store.getters['sidebar/showMenu']
      },
      set(value) {
        this.$store.commit('sidebar/changeDrawer', value)
      },
    },
    items() {
      return this.$store.getters['sidebar/items']
    },
    miniVariant() {
      return this.$store.getters['sidebar/miniVariant']
    },
  },
  methods: {
    drawerChange(value) {
      this.$store.commit('sidebar/changeDrawer', value)
    },
    changeMiniVariant() {
      this.$store.dispatch('sidebar/changeMiniVariant')
    },
  },
}
</script>

<style>
.navigation-drawer > .list .list__tile.list__tile--active {
  background-color: #1565c0 !important;
}
.navigation-drawer > .list .list__tile.list__tile--active > * {
  color: #fff;
}
</style>


