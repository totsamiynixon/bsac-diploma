<template>
  <v-flex>
      <router-view></router-view>
  </v-flex>
</template>


<script>
export default {
  data() {
    return {};
  },
  created() {
    this.$store.dispatch("sidebar/updateSidebarItems", sidebarItems);
    this.$store.dispatch("sidebar/showSidebar");
  }
};

const sidebarItems = [
  { icon: "content_copy", text: "Проекты", route: { name: "default" } },
  { icon: "history", text: "Frequently contacted" },
  { icon: "content_copy", text: "Duplicates" },
  {
    icon: "keyboard_arrow_up",
    "icon-alt": "keyboard_arrow_down",
    text: "Labels",
    model: true,
    children: [{ icon: "add", text: "Create label" }]
  },
  {
    icon: "keyboard_arrow_up",
    "icon-alt": "keyboard_arrow_down",
    text: "More",
    model: false,
    children: [
      { text: "Import" },
      { text: "Export" },
      { text: "Print" },
      { text: "Undo changes" },
      { text: "Other contacts" }
    ]
  },
  { icon: "settings", text: "Settings" },
  { icon: "chat_bubble", text: "Send feedback" },
  { icon: "help", text: "Help" },
  { icon: "phonelink", text: "App downloads" },
  { icon: "keyboard", text: "Go to the old version" }
];
</script>

