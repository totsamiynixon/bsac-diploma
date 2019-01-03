<template>
  <transition name="fade"
                  mode="out-in">
        <router-view />
      </transition>
</template>

<script>
export default {
  data() {
    return {};
  },
  created() {
    this.$store.dispatch("sidebar/enableSidebar");
  }
};
</script>
