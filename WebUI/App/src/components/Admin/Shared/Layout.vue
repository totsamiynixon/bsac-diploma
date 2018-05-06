<template>
  <v-flex>
      <router-view></router-view>
  </v-flex>
</template>


<script>
export default {
  data() {
    return {
      sidebarItems: [
        { icon: "content_copy", text: "Критерии", route: {name: "criterias"} },
       /*   { icon: "history", text: "Упражнения", route: {name: "exercises"} },
       { icon: "content_copy", text: "Duplicates" },
        {
          icon: "keyboard_arrow_up",
          "icon-alt": "keyboard_arrow_down",
          text: "Labels",
          model: true,
          children: [{ icon: "add", text: "Create label" }]
        } */
      ]
    };
  },
  created() {
    this.$store.dispatch("sidebar/updateSidebarItems", this.sidebarItems);
    this.$store.dispatch("sidebar/showSidebar");
  }
};
</script>


