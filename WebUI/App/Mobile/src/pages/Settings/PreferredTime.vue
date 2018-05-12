<template>
  <f7-page>
    <f7-navbar title="Предпочтительное время" back-link="Back"></f7-navbar>
<f7-list block inset>
  <f7-list-item  v-for="(item, index) in items" :key="index" @swipeout:overswipe="remove(index)">
    <f7-label :attr="{for:index}">{{'Время ' + (index+1)}}</f7-label>
    <f7-input type="time" :attr="{name:index}" :value="item.value" style="display:inline-block; width:68%"></f7-input>
    <f7-button round color="red" @click="remove(index)" style="display:inline-block; width:30%"><f7-icon f7="delete"></f7-icon></f7-button>
  </f7-list-item>
</f7-list>
  <f7-fab color="pink" @click="add">
    <f7-icon f7="add"></f7-icon>
  </f7-fab>
  </f7-page>
</template>
<script>
export default {
  data() {
    return {
      items: [
        {
          value: null
        },
        {
          value: null
        },
        {
          value: null
        }
      ]
    };
  },
  computed: {
    currentState() {
      return this.state;
    }
  },
  methods: {
    add() {
      this.items.push({
        value: null
      });
    },
    remove(index) {
      this.items.splice(index, 1);
    }
  }
};
</script>
