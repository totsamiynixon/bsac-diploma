<template>
  <f7-page>
    <f7-navbar title="Предпочтительное время"
               back-link="Back"></f7-navbar>
    <f7-list block
             inset>
      <f7-list-item v-for="(item, index) in currentTimes"
                    :key="index"
                    @swipeout:overswipe="remove(index)">
        <f7-label :attr="{for:index}">{{'Время ' + (index+1)}}</f7-label>
        <f7-input type="time"
                  :attr="{name:index}"
                  :value="currentTimes[index]"
                  style="display:inline-block; width:68%"></f7-input>
        <f7-button round
                   color="red"
                   @click="remove(index)"
                   style="display:inline-block; width:30%">
          <f7-icon f7="delete"></f7-icon>
        </f7-button>
      </f7-list-item>
    </f7-list>
    <f7-fab color="pink"
            @click="add">
      <f7-icon f7="add"></f7-icon>
    </f7-fab>
  </f7-page>
</template>
<script>
export default {
  data() {
    return {
      times: []
    };
  },
  computed: {
    currentTimes() {
      var result = this.$store.getters["settings/preferredTainingTime"];
      if (result != null) {
        this.times = result;
      }
      return this.times;
    }
  },
  methods: {
    add() {
      this.times.push("");
    },
    remove(index) {
      this.times.splice(index, 1);
    },
    save() {
      this.$store
        .dispatch("settings/changePreferredTrainingTime", this.times)
        .then(success => {
          this.$router.navigate("/settings");
        });
    }
  }
};
</script>
