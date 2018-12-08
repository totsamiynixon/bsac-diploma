<template>
  <v-flex class="px-2 py-2">
    <profession-settings :professions="professions"></profession-settings>
    <preferred-time-settings></preferred-time-settings>
  </v-flex>
</template>
<script>
import ProfessionSettings from "./Profession";
import PreferredTimeSettings from "./PreferredTime";
export default {
  data: function() {
    return {
      professions: []
    };
  },
  methods: {},
  created() {
    this.$http.get("/api/settings/initData").then(response => {
      this.professions = response.data.professions;
      this.$store.dispatch(
        "features/settings/installSettings",
        response.data.settings
      );
    });
  },
  components: {
    ProfessionSettings,
    PreferredTimeSettings
  }
};
</script>
