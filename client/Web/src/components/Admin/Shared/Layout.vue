<template>
  <v-flex>
     <transition name="fade"
                  mode="out-in">
        <router-view />
      </transition>
  </v-flex>
</template>


<script>
export default {
  data() {
    return {
      sidebarItems: [
        {
          icon: "content_copy",
          text: "Критерии",
          route: {
            name: "criterias"
          }
        },
        {
          icon: "content_copy",
          text: "Упражнения",
          route: {
            name: "exercises"
          }
        },
        {
          icon: "content_copy",
          text: "Профессии",
          route: {
            name: "professions"
          }
        }
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


