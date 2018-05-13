<template>
  <v-layout>
    <profession-settings :profession.sync="profession"></profession-settings>
    <preferred-time-settings :times.sync="times"></preferred-time-settings>
    <v-layout>
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
