<template>
  <v-flex>
      <router-view></router-view>
  </v-flex>
</template>


<script>
export default {
  props: ["id"],
  data() {
    return {
    }
  },
  created() {
    this.$store.dispatch("sidebar/hideSidebar");
  }
};
</script>


