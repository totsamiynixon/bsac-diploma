<template>
  <div id="alerts-container">
    <div class="alerts-holder">
      <v-alert v-for="(alert, index) in alerts"
               :key="index"
               :type="alert.type"
               transition="scale-transition"
               dismissible
               @input="onClose(alert)"
               :value="!alert.dismissed">
        {{alert.message}}
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
    alerts() {
      return this.$store.getters["shared/alerts"];
    }
  },
  methods: {
    onClose(alert) {
      this.$store.dispatch("shared/clearAlert", error);
    }
  }
};
</script>
<style scoped>
#alerts-container .alerts-holder {
  position: absolute;
  bottom: 0%;
  right: 0%;
  z-index: 10000;
}
</style>