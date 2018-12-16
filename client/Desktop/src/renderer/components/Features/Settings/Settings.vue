<template>
  <v-flex class="px-2 py-2">
    <profession-settings></profession-settings>
    <preferred-time-settings></preferred-time-settings>
  </v-flex>
</template>
<script>
import ProfessionSettings from "./Profession";
import PreferredTimeSettings from "./PreferredTime";
export default {
  data: function() {
    return {
    
    };
  },
  components: {
    ProfessionSettings,
    PreferredTimeSettings
  }
};
</script>
