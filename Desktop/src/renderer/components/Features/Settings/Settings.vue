<template>
  <v-flex class="px-2 py-2">
    <profession-settings :value.sync="profession"></profession-settings>
    <preferred-time-settings :times.sync="times"></preferred-time-settings>
  </v-flex>
</template>
<script>
import ProfessionSettings from "./Profession";
import PreferredTimeSettings from "./PreferredTime";
export default {
  data: function() {
    return {
      profession: "Бухгалтер",
      times: [
        {
          value: null,
          dialog: false
        },
        {
          value: null,
          dialog: false
        },
        {
          value: null,
          dialog: false
        }
      ]
    };
  },
  methods: {},
  components: {
    ProfessionSettings,
    PreferredTimeSettings
  }
};
</script>
