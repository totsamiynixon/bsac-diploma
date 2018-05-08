<template>
<div id="errors-container">
  <div class="errors-holder">
  <v-alert  v-for="(error, index) in errors" :key="index" error  transition="scale-transition" dismissible @input="onClose(error)" :value="!error.dismissed">
    {{error.message}}
  </v-alert>
  </div>
  </div>
</template>

<script>
export default {
  data() {
    return {};
  },
  computed: {
    errors() {
      return this.$store.getters["shared/errors"];
    }
  },
  methods: {
    onClose(error) {
      this.$emit("dismissed");
      this.$store.dispatch("shared/clearError", error);
    }
  }
};
</script>
<style scoped>
#errors-container .errors-holder {
  position: absolute;
  bottom: 0%;
  right: 0%;
  z-index: 10000;
}
</style>