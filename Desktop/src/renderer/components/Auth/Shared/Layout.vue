<template>
  <router-view/>
</template>

<script>
export default {
  data() {
    return {};
  },
  created() {
    this.$store.dispatch("sidebar/disableSidebar");
  }
};
</script>
