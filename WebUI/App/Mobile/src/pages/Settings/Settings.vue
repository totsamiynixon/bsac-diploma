<template>
  <f7-page>
    <f7-navbar>
      <f7-nav-left>
        <f7-link icon-if-ios="f7:menu"
                 icon-if-md="material:menu"
                 panel-open="left"></f7-link>
      </f7-nav-left>
      <f7-nav-title>Настройки</f7-nav-title>
    </f7-navbar>
    <f7-list>
      <f7-list-item link="/settings/profession/"
                    title="Профессия">
        <slot name="after-list">
          {{currentProfession.name}}
        </slot>
      </f7-list-item>
      <f7-list-item link="/settings/preferred-time/"
                    title="Предпочтительное время"></f7-list-item>
    </f7-list>
  </f7-page>
</template>
<script>
import axios from "axios";
export default {
  data() {
    return {
      profession: {
        id: 0,
        name: ""
      }
    };
  },
  created() {
    this.$store.dispatch("settings/getProfession");
  },
  computed: {
    currentProfession() {
      let result = this.$store.getters["settings/profession"];
      return result != null ? result : this.profession;
    }
  }
};
</script>
