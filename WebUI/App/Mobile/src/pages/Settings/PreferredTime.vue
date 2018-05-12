<template>
  <f7-page>
    <f7-navbar title="Предпочтительное время" back-link="Back"></f7-navbar>
<div class="list block">
  <ul>
    <li class="swipeout">
      <div class="swipeout-content">
                               <f7-label >312321132</f7-label>
    <f7-input type="time"></f7-input>
      </div>
      <div class="swipeout-actions-right">
        <a href="#" class="swipeout-delete">Delete</a>
      </div>
    </li>
    <li class="swipeout">
      <div class="swipeout-content">
        <div class="item-content">
          <div class="item-inner">
            <div class="item-title">Item 2</div>
          </div>
        </div>
      </div>
      <div class="swipeout-actions-right">
        <a href="#" class="swipeout-delete">Delete</a>
      </div>
    </li>
  </ul>
</div>
<f7-list block form inset>
  <f7-list-item  swipeout v-for="(item, index) in items" :key="index" @swipeout:deleted="remove(index)" @swipeout:overswipe="remove(index)">
    <f7-label :attr="{for:index}">312321132</f7-label>
    <f7-input type="time" :attr="{name:index}" :value="item.value"></f7-input>
    <f7-swipeout-actions>
      <f7-swipeout-button delete overswipe>Delete</f7-swipeout-button>
    </f7-swipeout-actions>
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
