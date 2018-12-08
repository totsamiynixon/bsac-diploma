<template>
  <f7-page>
    <f7-navbar>
      <f7-nav-left>
        <f7-link v-show="valid"
                 icon="icon-back"
                 @click="saveAndExit"></f7-link>
      </f7-nav-left>
      <f7-nav-title>Предпочтительное время</f7-nav-title>
    </f7-navbar>
    <f7-list block
             inset>
      <f7-list-item v-for="(item, index) in times"
                    :key="index"
                    @swipeout:overswipe="remove(index)">
        <f7-label :attr="{for:index}">{{'Время ' + (index+1)}}</f7-label>
        <f7-input type="time"
                  :attr="{name:index}"
                  :value="times[index]"
                  @change="times[index] = $event.target.value"
                  :placeholder="times[index]"
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
      times: [],
      isValid: true
    };
  },
  created() {
    this.times = this.$store.getters["settings/preferredTime"] || [];
  },
  computed: {
    valid() {
      return this.times.filter(time => time.length == 0).length == 0;
    }
  },
  methods: {
    add() {
      this.times.push("");
    },
    remove(index) {
      this.times.splice(index, 1);
    },
    saveAndExit() {
      if (!this.valid) {
        return;
      }
      var payload = this.times.map(s => {
        return s;
      });
      this.$store.dispatch("settings/changePreferredTime", payload);
      this.$f7router.back();
    }
  }
};
</script>
